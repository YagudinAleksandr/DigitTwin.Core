using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Сервис обработки базовых ответов от API
    /// </summary>
    public interface IActionService
    {
        /// <summary>
        /// Ответ от сервера 200 (Ok)
        /// </summary>
        /// <typeparam name="T">Тип тела ответа</typeparam>
        /// <param name="item">Модель</param>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<T> OkResponse<T>(T item);

        /// <summary>
        /// Ответ от сервера 201 (Created)
        /// </summary>
        /// <typeparam name="T">Тип тела ответа</typeparam>
        /// <param name="item">Модель</param>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<T> CreatedResponse<T>(T item);

        /// <summary>
        /// Ответ от сервера 204 (NoContent)
        /// </summary>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> NoContentResponse();

        /// <summary>
        /// Ответ от сервера 206 (PartialContent)
        /// </summary>
        /// <typeparam name="T">Тип тела ответа</typeparam>
        /// <param name="items">Коллекция элементов</param>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<ItemCountDto<T>> PartialResponse<T>(IReadOnlyCollection<T> items);

        /// <summary>
        /// Ответ от сервера 302 (Redirect)
        /// </summary>
        /// <param name="maxRedirects">Количество редиректов</param>
        /// <param name="url">URL ресурса</param>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> RedirectResponse(string url, int maxRedirects);

        /// <summary>
        /// Ответ от сервера 400 (BadRequest)
        /// </summary>
        /// <param name="errors">Ошибки</param>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> BadRequestResponse(Dictionary<string, string> errors);

        /// <summary>
        /// Ответ от сервера 404 (NotFound)
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> NotFoundResponse(string error);

        /// <summary>
        /// Ответ от сервера 401 (NotAuthorize)
        /// </summary>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> NotAuthorizeResponse();

        /// <summary>
        /// Ответ от сервера 403 (Forbiden)
        /// </summary>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> ForbidenrResponse();

        /// <summary>
        /// Ответ от сервера 500 (Server error)
        /// </summary>
        /// <param name="error">Исключение</param>
        /// <returns><inheritdoc cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> ServerErrorResponse(Exception error);
    }
}
