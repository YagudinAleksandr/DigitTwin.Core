using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigitTwin.Infrastructure.Redis;
using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DigitTwin.Core.ActionService
{
    internal class TokenService : ITokenService
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
        /// Префикс для ключей токенов обновления
        /// </summary>
        private const string TokenKeyRefreshPrefix = "user_token_refresh:";

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

        /// <inheritdoc cref="ITokenService.CreateToken"/>
        public async Task<TokenInfoDto> CreateToken(Guid userId, string email, UserTypeEnum userType, TokenTypeEnum tokenType)
        {
            // Создаем JWT токен
            var token = GenerateJwtToken(userId, email, userType, tokenType);

            // Создаем информацию о токене
            var tokenInfo = new TokenInfoDto
            {
                UserId = userId,
                Email = email,
                UserType = (int)userType,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(tokenType == TokenTypeEnum.Auth ? _jwtConfig.ExpirationToken : _jwtConfig.ExpirationRefresh),
                Token = token
            };

            // Сохраняем токен в Redis
            var expirationTime = tokenType == TokenTypeEnum.Auth ? _jwtConfig.ExpirationToken : _jwtConfig.ExpirationRefresh;
            await SaveToken(userId, token, expirationTime, tokenType);

            // Сохраняем информацию о токене по значению токена для быстрого поиска
            var tokenValueKey = $"{TokenValuePrefix}{token}";
            await _redisService.SetAsync(tokenValueKey, tokenInfo, expirationTime);

            return tokenInfo;
        }

        /// <inheritdoc cref="ITokenService.SaveToken"/>
        public async Task SaveToken(Guid userId, string token, TimeSpan expirationTime, TokenTypeEnum tokenType)
        {
            string tokenKeyType = tokenType == TokenTypeEnum.Auth ? TokenKeyPrefix : TokenKeyRefreshPrefix;
            var tokenKey = $"{tokenKeyType}{userId}";
            var tokenInfo = new TokenInfoDto
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(expirationTime)
            };

            await _redisService.SetAsync(tokenKey, tokenInfo, expirationTime);
        }

        /// <inheritdoc cref="ITokenService.GetToken"/>
        public async Task<string?> GetToken(Guid userId, TokenTypeEnum tokenType)
        {
            var tokenTypeKeyPrefix = tokenType == TokenTypeEnum.Auth ? TokenKeyPrefix : TokenKeyRefreshPrefix;
            var tokenKey = $"{tokenTypeKeyPrefix}{userId}";
            var tokenInfo = await _redisService.GetAsync(tokenKey);

            if (tokenInfo == null || DateTime.UtcNow > tokenInfo.ExpiresAt)
            {
                // Токен не найден или истек
                await RemoveToken(userId, tokenType);
                return null;
            }

            return tokenInfo.Token;
        }

        /// <inheritdoc cref="ITokenService.RemoveToken"/>
        public async Task RemoveToken(Guid userId, TokenTypeEnum tokenType)
        {
            var tokenTypeKeyPrefix = tokenType == TokenTypeEnum.Auth ? TokenKeyPrefix : TokenKeyRefreshPrefix;
            var tokenKey = $"{tokenTypeKeyPrefix}{userId}";

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

        /// <inheritdoc cref="ITokenService.ValidateToken"/>
        public async Task<bool> ValidateToken(string token, TokenTypeEnum tokenType)
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
                    await RemoveToken(tokenInfo.UserId, tokenType);
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc cref="ITokenService.GetTokenInfo"/>
        public async Task<TokenInfoDto?> GetTokenInfo(string token, TokenTypeEnum tokenType)
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
                        await RemoveToken(tokenInfo.UserId, tokenType);
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

        /// <inheritdoc cref="ITokenService.RefreshToken"/>
        public async Task<TokenInfoDto> RefreshToken(Guid userId, string email, UserTypeEnum userType, TokenTypeEnum tokenType)
        {
            // Удаляем старый токен
            await RemoveToken(userId, tokenType);

            // Создаем новый токен
            return await CreateToken(userId, email, userType, tokenType);
        }

        /// <summary>
        /// Генерирует JWT токен
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="email">Email пользователя</param>
        /// <param name="userType">Тип пользователя</param>
        /// <param name="tokenType">Тип токена</param>
        /// <returns>JWT токен</returns>
        private string GenerateJwtToken(Guid userId, string email, UserTypeEnum userType, TokenTypeEnum tokenType)
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
                Expires = DateTime.UtcNow.Add(tokenType == TokenTypeEnum.Auth ? _jwtConfig.ExpirationToken : _jwtConfig.ExpirationRefresh),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        ///// <summary>
        ///// Получить информацию о токене по значению токена
        ///// </summary>
        ///// <param name="token">Токен</param>
        ///// <returns>Информация о токене или null</returns>
        //public async Task<TokenInfoDto?> GetTokenInfoFromRedisAsync(string token)
        //{
        //    if (string.IsNullOrEmpty(token))
        //        return null;

        //    var tokenValueKey = $"{TokenValuePrefix}{token}";
        //    var tokenInfo = await _redisService.GetAsync(tokenValueKey);

        //    if (tokenInfo == null || DateTime.UtcNow > tokenInfo.ExpiresAt)
        //    {
        //        if (tokenInfo != null)
        //        {
        //            await RemoveToken(tokenInfo.UserId);
        //        }
        //        return null;
        //    }

        //    return tokenInfo;
        //}

        /// <summary>
        /// Обновить время жизни токена
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="newExpirationTime">Новое время истечения</param>
        /// <returns>Task</returns>
        //public async Task ExtendTokenAsync(Guid userId, TimeSpan newExpirationTime)
        //{
        //    var tokenKey = $"{TokenKeyPrefix}{userId}";
        //    var tokenInfo = await _redisService.GetAsync(tokenKey);

        //    if (tokenInfo != null)
        //    {
        //        tokenInfo.ExpiresAt = DateTime.UtcNow.Add(newExpirationTime);
        //        await _redisService.SetAsync(tokenKey, tokenInfo, newExpirationTime);

        //        // Обновляем также запись по значению токена
        //        var tokenValueKey = $"{TokenValuePrefix}{tokenInfo.Token}";
        //        await _redisService.SetAsync(tokenValueKey, tokenInfo, newExpirationTime);
        //    }
        //}
    }
}
