using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace LotteryService.WebApi
{
    public class LsAuthenticationFilter :Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            // 1. Look for credentials in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // 2. If there are no credentials, do nothing.
            if (authorization == null)
            {
                return;
            }

            // 3. If there are credentials but the filter does not recognize the 
            //    authentication scheme, do nothing.
            if (authorization.Scheme != "Basic")
            {
                return;

            }

            //// 4. If there are credentials that the filter understands, try to validate them.
            //// 5. If the credentials are bad, set the error result.
            //if (string.IsNullOrEmpty(authorization.Parameter))
            //{
            //    context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
            //    return;
            //}

            //Tuple<string, string> userNameAndPasword = ExtractUserNameAndPassword(authorization.Parameter);
            //if (userNameAndPasword == null)
            //{
            //    context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
            //}

            //string userName = userNameAndPasword.Item1;
            //string password = userNameAndPasword.Item2;

            //IPrincipal principal = await AuthenticateAsync(userName, password, cancellationToken);
            //if (principal == null)
            //{
            //    context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
            //    HttpContext.Current.User = principal;
            //}

            // 6. If the credentials are valid, set principal.
            else
            {
              //  context.Principal = principal;
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}