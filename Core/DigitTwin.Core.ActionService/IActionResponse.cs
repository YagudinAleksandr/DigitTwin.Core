using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    public interface IActionResponse
    {
        BaseApiResponse<TModel> GetOkResponse<TModel>(TModel model);
        BaseApiResponse<TModel> GetBadRequestResponse<TModel>(TModel model, Dictionary<string, string> errors);
        BaseApiResponse<TModel> GetNotFoundResponse<TModel>(TModel model, string error);
        BaseApiResponse<TModel> GetNotAuthorizedResponse<TModel>();
    }
}
