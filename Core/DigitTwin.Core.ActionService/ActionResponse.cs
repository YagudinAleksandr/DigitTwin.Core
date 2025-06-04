using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    internal class ActionResponse : IActionResponse
    {
        public BaseApiResponse<TModel> GetBadRequestResponse<TModel>(TModel model, Dictionary<string, string> errors)
        {
            var response = new BaseApiResponse<TModel>();
            response.StatusCode = (int)StatusCodesEnum.BadRequest;

            return response;
        }

        public BaseApiResponse<TModel> GetNotAuthorizedResponse<TModel>()
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<TModel> GetNotFoundResponse<TModel>(TModel model, string error)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<TModel> GetOkResponse<TModel>(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
