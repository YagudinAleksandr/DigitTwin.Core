using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.ActionService
{
    internal class ActionService : IActionService
    {
        public BaseApiResponse<BaseBodyStub> BadRequestResponse(Dictionary<string, string> errors)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<T> CreatedResponse<T>(T item)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<BaseBodyStub> ForbidenrResponse()
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<BaseBodyStub> NoContentResponse()
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<BaseBodyStub> NotAuthorizeResponse()
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<BaseBodyStub> NotFoundResponse(string error)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<T> OkResponse<T>(T item)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<ItemCountDto<T>> PartialResponse<T>(IReadOnlyCollection<T> items)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<BaseBodyStub> RedirectResponse(string url, int maxRedirects)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<BaseBodyStub> ServerErrorResponse(string error)
        {
            throw new NotImplementedException();
        }
    }
}
