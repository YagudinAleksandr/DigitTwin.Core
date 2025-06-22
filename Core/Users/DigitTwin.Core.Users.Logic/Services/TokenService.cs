using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DigitTwin.Core.Users.Logic.Configs;
using DigitTwin.Infrastructure.Redis;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.Users
{
    /// <inheritdoc cref="ITokenService"/>
    public class TokenService : ITokenService
    {
        #region CTOR
        /// <inheritdoc cref="IRedisService{T}"/>
        private readonly IRedisService<TokenInfoDto> _redisService;

        /// <inheritdoc cref="JwtConfiguration"/>
        private readonly JwtConfiguration _jwtConfig;

        /// <summary>
        /// Префикс для ключей токенов в Redis
        /// </summary>
        private const string TokenKeyPrefix = "user_token:";

        /// <summary>
        /// Префикс для ключей токенов по значению токена
        /// </summary>
        private const string TokenValuePrefix = "token_value:";

        public TokenService(IRedisService<TokenInfoDto> redisService, IOptions<JwtConfiguration> jwtConfig)
        {
            _redisService = redisService;
            _jwtConfig = jwtConfig.Value;
        }
        #endregion

        /// <inheritdoc cref="ITokenService.CreateTokenAsync"/>
        public async Task<TokenInfoDto> CreateTokenAsync(Guid userId, string email, int userType)
        {
            // Создаем JWT токен
            var token = GenerateJwtToken(userId, email, userType);
            
            // Создаем информацию о токене
            var tokenInfo = new TokenInfoDto
            {
                UserId = userId,
                Email = email,
                UserType = userType,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes),
                Token = token
            };

            // Сохраняем токен в Redis
            var expirationTime = TimeSpan.FromMinutes(_jwtConfig.ExpirationMinutes);
            await SaveTokenAsync(userId, token, expirationTime);
            
            // Сохраняем информацию о токене по значению токена для быстрого поиска
            var tokenValueKey = $"{TokenValuePrefix}{token}";
            await _redisService.SetAsync(tokenValueKey, tokenInfo, expirationTime);

            return tokenInfo;
        }

        /// <inheritdoc cref="ITokenService.SaveTokenAsync"/>
        public async Task SaveTokenAsync(Guid userId, string token, TimeSpan expirationTime)
        {
            var tokenKey = $"{TokenKeyPrefix}{userId}";
            var tokenInfo = new TokenInfoDto
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(expirationTime)
            };

            await _redisService.SetAsync(tokenKey, tokenInfo, expirationTime);
        }

        /// <inheritdoc cref="ITokenService.GetTokenAsync"/>
        public async Task<string?> GetTokenAsync(Guid userId)
        {
            var tokenKey = $"{TokenKeyPrefix}{userId}";
            var tokenInfo = await _redisService.GetAsync(tokenKey);
            
            if (tokenInfo == null || DateTime.UtcNow > tokenInfo.ExpiresAt)
            {
                // Токен не найден или истек
                await RemoveTokenAsync(userId);
                return null;
            }

            return tokenInfo.Token;
        }

        /// <inheritdoc cref="ITokenService.RemoveTokenAsync"/>
        public async Task RemoveTokenAsync(Guid userId)
        {
            var tokenKey = $"{TokenKeyPrefix}{userId}";
            
            // Получаем информацию о токене перед удалением
            var tokenInfo = await _redisService.GetAsync(tokenKey);
            
            // Удаляем токен по ID пользователя
            await _redisService.RemoveAsync(tokenKey);
            
            // Удаляем токен по значению токена
            if (tokenInfo != null && !string.IsNullOrEmpty(tokenInfo.Token))
            {
                var tokenValueKey = $"{TokenValuePrefix}{tokenInfo.Token}";
                await _redisService.RemoveAsync(tokenValueKey);
            }
        }

        /// <inheritdoc cref="ITokenService.ValidateTokenAsync"/>
        public async Task<bool> ValidateTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                // Проверяем JWT токен
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = !string.IsNullOrEmpty(_jwtConfig.Issuer),
                    ValidIssuer = _jwtConfig.Issuer,
                    ValidateAudience = !string.IsNullOrEmpty(_jwtConfig.Audience),
                    ValidAudience = _jwtConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Проверяем, есть ли токен в Redis
                var tokenValueKey = $"{TokenValuePrefix}{token}";
                var tokenInfo = await _redisService.GetAsync(tokenValueKey);
                
                if (tokenInfo == null)
                    return false;

                // Проверяем, не истек ли токен
                if (DateTime.UtcNow > tokenInfo.ExpiresAt)
                {
                    // Токен истек, удаляем его
                    await RemoveTokenAsync(tokenInfo.UserId);
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc cref="ITokenService.GetTokenInfoAsync"/>
        public async Task<TokenInfoDto?> GetTokenInfoAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            try
            {
                // Декодируем JWT токен
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Получаем информацию из claims
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var emailClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                var userTypeClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                    return null;

                // Проверяем, есть ли токен в Redis
                var tokenValueKey = $"{TokenValuePrefix}{token}";
                var tokenInfo = await _redisService.GetAsync(tokenValueKey);
                
                if (tokenInfo == null || DateTime.UtcNow > tokenInfo.ExpiresAt)
                {
                    if (tokenInfo != null)
                    {
                        await RemoveTokenAsync(tokenInfo.UserId);
                    }
                    return null;
                }

                return tokenInfo;
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc cref="ITokenService.RefreshTokenAsync"/>
        public async Task<TokenInfoDto> RefreshTokenAsync(Guid userId, string email, int userType)
        {
            // Удаляем старый токен
            await RemoveTokenAsync(userId);
            
            // Создаем новый токен
            return await CreateTokenAsync(userId, email, userType);
        }

        /// <summary>
        /// Генерирует JWT токен
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="email">Email пользователя</param>
        /// <param name="userType">Тип пользователя</param>
        /// <returns>JWT токен</returns>
        private string GenerateJwtToken(Guid userId, string email, int userType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, userType.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Получить информацию о токене по значению токена
        /// </summary>
        /// <param name="token">Токен</param>
        /// <returns>Информация о токене или null</returns>
        public async Task<TokenInfoDto?> GetTokenInfoFromRedisAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenValueKey = $"{TokenValuePrefix}{token}";
            var tokenInfo = await _redisService.GetAsync(tokenValueKey);
            
            if (tokenInfo == null || DateTime.UtcNow > tokenInfo.ExpiresAt)
            {
                if (tokenInfo != null)
                {
                    await RemoveTokenAsync(tokenInfo.UserId);
                }
                return null;
            }

            return tokenInfo;
        }

        /// <summary>
        /// Обновить время жизни токена
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="newExpirationTime">Новое время истечения</param>
        /// <returns>Task</returns>
        public async Task ExtendTokenAsync(Guid userId, TimeSpan newExpirationTime)
        {
            var tokenKey = $"{TokenKeyPrefix}{userId}";
            var tokenInfo = await _redisService.GetAsync(tokenKey);
            
            if (tokenInfo != null)
            {
                tokenInfo.ExpiresAt = DateTime.UtcNow.Add(newExpirationTime);
                await _redisService.SetAsync(tokenKey, tokenInfo, newExpirationTime);
                
                // Обновляем также запись по значению токена
                var tokenValueKey = $"{TokenValuePrefix}{tokenInfo.Token}";
                await _redisService.SetAsync(tokenValueKey, tokenInfo, newExpirationTime);
            }
        }
    }
}
