using DigitTwin.Lib.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Фильтр обработки ответа сервера исключения из <see cref="IBaseApiResponse"/> в <see cref="IActionResult"/>
    /// </summary>
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new BaseApiResponse<object>
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            response.Errors.Add(context.Exception.Message);

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
