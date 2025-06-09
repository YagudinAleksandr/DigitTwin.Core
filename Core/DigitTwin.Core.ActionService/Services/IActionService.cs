using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Сервис обработки базовых ответов от API
    /// </summary>
    public interface IActionService
    {
        BaseApiResponse<T> OkResponse<T>(T item);
        BaseApiResponse<T> CreatedResponse<T>(T item);
        BaseApiResponse<BaseBodyStub> NoContentResponse();
        BaseApiResponse<ItemCountDto<T>> PartialResponse<T>(IReadOnlyCollection<T> items);

        BaseApiResponse<BaseBodyStub> RedirectResponse(string url, int maxRedirects);

        BaseApiResponse<BaseBodyStub> BadRequestResponse(Dictionary<string, string> errors);
        BaseApiResponse<BaseBodyStub> NotFoundResponse(string error);
        BaseApiResponse<BaseBodyStub> NotAuthorizeResponse();
        BaseApiResponse<BaseBodyStub> ForbidenrResponse();

        BaseApiResponse<BaseBodyStub> ServerErrorResponse(string error);
    }
}
