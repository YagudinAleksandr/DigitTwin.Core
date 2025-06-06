using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Сервис формирования ответа от сервера
    /// </summary>
    public interface IActionResponse
    {
        #region 2.. Statuses Codes
        /// <summary>
        /// Ответ сервера 201 (Сущность создана)
        /// </summary>
        /// <typeparam name="TModelDto">Тип модели</typeparam>
        /// <param name="dto">Объект</param>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<TModelDto> CreatedResponse<TModelDto>(TModelDto dto);

        /// <summary>
        /// Ответ сервера 204 (Без данных)
        /// </summary>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> NoContentResponse();

        /// <summary>
        /// Ответ сервера 200 (Успешный запрос)
        /// </summary>
        /// <typeparam name="TModelDto">Тип модели</typeparam>
        /// <param name="dto">Объект</param>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<TModelDto> OkResponse<TModelDto>(TModelDto dto);

        /// <summary>
        /// Ответ сервера 206 (Частичный контент)
        /// </summary>
        /// <typeparam name="TModelDto">Тип модели</typeparam>
        /// <param name="dto">Объект</param>
        /// <param name="itemStart">Стартовый элемент</param>
        /// <param name="itemEnd">Конечный элемент</param>
        /// <param name="totalCount">Всего элементов</param>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<ItemCountDto<TModelDto>> PartialContentResponse<TModelDto>(IReadOnlyCollection<TModelDto> dto, int itemStart, int itemEnd, int totalCount);
        #endregion

        #region 3.. Statuses Codes
        
        /// <summary>
        /// Ответ сервера 302 (Перенаправление)
        /// </summary>
        /// <param name="redirectionUrl">Ссылка перенаправления</param>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> RedirectTemporaryResponse(string redirectionUrl);
        #endregion

        #region 4.. Statuses Codes

        /// <summary>
        /// Ответ сервера 400 (Неверный запрос)
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> BadRequestResponse(Dictionary<string, string> errors);

        /// <summary>
        /// Ответ от сервера 404 (Ресурс не найден)
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> NotFoundResponse(string error);

        /// <summary>
        /// Отвтет от сервера 401 (Не авторизирован)
        /// </summary>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> NotAuthorizedResponse();

        /// <summary>
        /// Ответ от сервера 403 (Нет доступа к ресурсу)
        /// </summary>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<BaseBodyStub> ForbiddenResponse();
        #endregion

        #region 5.. Statuses Codes

        /// <summary>
        /// Ответ от сервера 500 (Ошибка сервера)
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="innerException">Описание исключения</param>
        /// <returns><see cref="BaseApiResponse{TBody}"/></returns>
        BaseApiResponse<(string exception, string innerException)> ServerErrorResponse(string exception, string? innerException);
        #endregion
    }
}
