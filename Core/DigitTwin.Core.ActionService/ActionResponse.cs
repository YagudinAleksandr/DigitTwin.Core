using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    /// <inheritdoc cref="IActionResponse"/>
    internal class ActionResponse : IActionResponse
    {
        public BaseApiResponse<BaseBodyStub> BadRequestResponse(Dictionary<string, string> errors)
        {
            var response = new BaseApiResponse<BaseBodyStub>();
            response.StatusCode = (int)StatusCodesEnum.BadRequest;

            foreach (var error in errors)
            {
                response.Errors.Add(error.Key, error.Value);
            }

            return response;
        }

        public BaseApiResponse<TModelDto> CreatedResponse<TModelDto>(TModelDto dto)
        {
            var response = new BaseApiResponse<TModelDto>();
            response.Body = dto;
            response.StatusCode = (int)StatusCodesEnum.Success;

            return response;
        }

        public BaseApiResponse<BaseBodyStub> ForbiddenResponse()
        {
            var response = new BaseApiResponse<BaseBodyStub>();
            response.StatusCode = (int)StatusCodesEnum.Forbidden;

            return response;
        }

        public BaseApiResponse<BaseBodyStub> NoContentResponse()
        {
            var response = new BaseApiResponse<BaseBodyStub>();
            response.StatusCode = (int)StatusCodesEnum.NoContent;

            return response;
        }

        public BaseApiResponse<BaseBodyStub> NotAuthorizedResponse()
        {
            var response = new BaseApiResponse<BaseBodyStub>();
            response.StatusCode = (int)StatusCodesEnum.NotAuthorized;

            return response;
        }

        public BaseApiResponse<BaseBodyStub> NotFoundResponse(string error)
        {
            var response = new BaseApiResponse<BaseBodyStub>();
            response.StatusCode = (int)StatusCodesEnum.NotFound;
            response.Errors.Add("ResourceNotFound", error);

            return response;
        }

        public BaseApiResponse<TModelDto> OkResponse<TModelDto>(TModelDto dto)
        {
            var response = new BaseApiResponse<TModelDto>();
            response.StatusCode = (int)StatusCodesEnum.Success;
            response.Body = dto;

            return response;
        }

        public BaseApiResponse<ItemCountDto<TModelDto>> PartialContentResponse<TModelDto>(IReadOnlyCollection<TModelDto> dto, int itemStart, int itemEnd, int totalCount)
        {
            var response = new BaseApiResponse<ItemCountDto<TModelDto>>();
            var arrayOfItems = dto.ToArray();
            var headerValues = string.Format("{0}-{1}/{2}", itemStart, itemEnd, totalCount);

            response.StatusCode = (int)StatusCodesEnum.PartialContent;
            response.Body = new ItemCountDto<TModelDto>() { Count = dto.Count, Items = arrayOfItems };
            response.Headers.Add("Content-Range", headerValues);

            return response;
        }

        public BaseApiResponse<BaseBodyStub> RedirectTemporaryResponse(string redirectionUrl)
        {
            var response = new BaseApiResponse<BaseBodyStub>();
            response.StatusCode = (int)StatusCodesEnum.Redirect;
            response.RedirectionUrl = redirectionUrl;
            response.MaxRedirects = 2;

            return response;
        }

        public BaseApiResponse<(string exception, string innerException)> ServerErrorResponse(string exception, string? innerException)
        {
            var response = new BaseApiResponse<(string exception, string innerException)>();
            response.StatusCode = (int)StatusCodesEnum.ServerError;

            string innerPart = string.IsNullOrEmpty(innerException) ? string.Empty : innerException;

            response.Body = (exception, innerPart);

            return response;
        }
    }
}
