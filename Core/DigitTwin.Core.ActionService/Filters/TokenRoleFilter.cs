using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Linq;
using System.Reflection;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Фильтр для проверки роли пользователя
    /// </summary>
    public class TokenRoleFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Получаем атрибут с метода
            var methodInfo = context.ActionDescriptor.GetType().GetProperty("MethodInfo")?.GetValue(context.ActionDescriptor) as MethodInfo;
            var allowedRolesAttr = methodInfo?.GetCustomAttributes(typeof(AllowedRolesAttribute), true).FirstOrDefault() as AllowedRolesAttribute;

            // Если не найдено на методе — ищем на контроллере
            if (allowedRolesAttr == null)
            {
                var controllerType = context.Controller.GetType();
                allowedRolesAttr = controllerType.GetCustomAttributes(typeof(AllowedRolesAttribute), true).FirstOrDefault() as AllowedRolesAttribute;
            }

            var allowedRoles = allowedRolesAttr?.Roles;

            if (allowedRoles == null || allowedRoles.Length == 0)
            {
                await next();
                return;
            }

            var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.StartsWith("Bearer ") ? authHeader.Substring(7) : authHeader;

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                if (roleClaim == null || !allowedRoles.Contains(roleClaim, StringComparer.OrdinalIgnoreCase))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
} 