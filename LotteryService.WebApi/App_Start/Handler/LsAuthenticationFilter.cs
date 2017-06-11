using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Lottery.Entities;
using LotteryService.Application.Account.Dtos;
using LotteryService.Common;

namespace LotteryService.WebApi
{
    public class LsAuthenticationFilter :Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            if (Enumerable.Any(context.ActionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()) ||
                Enumerable.Any(context.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()))
            {
                return;
            }

            HttpRequestMessage request = context.Request;

            UserDto loginUser = null;

            try
            {
                loginUser = request.GetLoginUser();

                // 2. If there are no credentials, do nothing.
                if (loginUser == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                    return;
                }
            }
            catch (Exception ex)
            {
                if (context.ActionContext.ActionDescriptor.ActionName.ToLower() == LsConstant.Logout)
                {
                    context.ErrorResult = new AuthenticationFailureResult("用户未登录,非法操作", request);
                    return;
                }
                context.ErrorResult = new AuthenticationFailureResult(ex.Message, request);
                return;
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}