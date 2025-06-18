using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    internal class ActionService : IActionService
    {
        public BaseApiResponse<BaseBodyStub> BadRequestResponse(Dictionary<string, string> errors)
        {
            var response = new BaseApiResponse<BaseBodyStub>
            {
                Errors = errors,
                StatusCode = 400
            };

            return response;
        }

        public BaseApiResponse<T> CreatedResponse<T>(T item)
        {
            var response = new BaseApiResponse<T>
            {
                Body = item,
                StatusCode = 201
            };

            return response;
        }

        public BaseApiResponse<BaseBodyStub> ForbidenrResponse()
        {
            var response = new BaseApiResponse<BaseBodyStub>
            {
                StatusCode = 403
            };
            response.Errors.Add("Forbiden", "Доступ к запрашиваемому ресурсу запрещен");

            return response;
        }

        public BaseApiResponse<BaseBodyStub> NoContentResponse()
        {
            var response = new BaseApiResponse<BaseBodyStub>
            {
                StatusCode = 204
            };

            return response;
        }

        public BaseApiResponse<BaseBodyStub> NotAuthorizeResponse()
        {
            var response = new BaseApiResponse<BaseBodyStub>
            {
                StatusCode = 401
            };
            response.Errors.Add("NotAuthorized", "Пользователь не авторизирован");

            return response;
        }

        public BaseApiResponse<BaseBodyStub> NotFoundResponse(string error)
        {
            var response = new BaseApiResponse<BaseBodyStub>
            {
                StatusCode = 404
            };
            response.Errors.Add("NotFound", error);

            return response;
        }

        public BaseApiResponse<T> OkResponse<T>(T item)
        {
            var response = new BaseApiResponse<T>
            {
                Body = item,
                StatusCode = 200
            };

            return response;
        }

        public BaseApiResponse<ItemCountDto<T>> PartialResponse<T>(IReadOnlyCollection<T> items,int startItemsCount, int endItemCount, int total)
        {
            var response = new BaseApiResponse<ItemCountDto<T>>();
            var itemsCollection = new ItemCountDto<T>();
            itemsCollection.Items = items.ToArray();
            itemsCollection.Count = items.Count;

            response.Body = itemsCollection;
            response.Headers.Add("Content-Range", $"{startItemsCount} - {endItemCount} / {total}");
            response.StatusCode = 206;

            return response;
        }

        public BaseApiResponse<BaseBodyStub> RedirectResponse(string url, int maxRedirects)
        {
            var response = new BaseApiResponse<BaseBodyStub>
            {
                MaxRedirects = maxRedirects,
                RedirectionUrl = url,
                StatusCode = 302
            };

            return response;
        }

        public BaseApiResponse<BaseBodyStub> ServerErrorResponse(Exception error)
        {
            var response = new BaseApiResponse<BaseBodyStub>
            {
                StatusCode = 500
            };
            response.Errors.Add("ServerError", error.ToString());

            return response;
        }
    }
}
