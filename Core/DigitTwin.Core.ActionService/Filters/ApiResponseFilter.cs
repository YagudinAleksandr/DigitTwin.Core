using DigitTwin.Lib.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Фильтр обработки ответа сервера из <see cref="IBaseApiResponse"/> в <see cref="IActionResult"/>
    /// </summary>
    public class ApiResponseFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Result is ObjectResult objectResult &&
                objectResult.Value is IBaseApiResponse baseResponse)
            {
                executedContext.Result = new ObjectResult(baseResponse)
                {
                    StatusCode = baseResponse.StatusCode
                };
            }
        }
    }
}
