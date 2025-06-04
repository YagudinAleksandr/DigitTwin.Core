namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Список статус кодов
    /// </summary>
    internal enum StatusCodesEnum
    {
        Success = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        NotFound = 404,
        NotAuthorized = 401,
        Forbidden = 403,
        ServerError = 500
    }
}
