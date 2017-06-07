using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Extensions;

namespace LotteryService.WebApi
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public ResultMessage<string> AuthFailureResult { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            AuthFailureResult = new ResultMessage<string>(ResultCode.NotAllowed, reasonPhrase);
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, AuthFailureResult);
        }
    }
}