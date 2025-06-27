using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Фильтр для проверки токена авторизации
    /// </summary>
    public class TokenAuthFilter : IAsyncActionFilter
    {
        private readonly ITokenService _tokenService;

        public TokenAuthFilter(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.StartsWith("Bearer ") ? authHeader.Substring(7) : authHeader;

            if (string.IsNullOrEmpty(token) || !await _tokenService.ValidateToken(token, TokenTypeEnum.Auth))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next();
        }
    }
} 