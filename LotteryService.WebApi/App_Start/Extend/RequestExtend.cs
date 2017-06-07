using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lottery.Entities;
using LotteryService.Application.Account;
using LotteryService.Common;
using LotteryService.Common.Excetions;
using LotteryService.Common.Tools;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LotteryService.WebApi
{
    public static class RequestExtend
    {
        public static string RequestValue(string key)
        {
            string value = string.Empty;
            switch (key)
            {
                case LsConstant.RequestBrowser:
                    value = Utils.GetBrowserInfo();
                    break;
                case LsConstant.RequestClientAddress:
                    value = IpHelper.GetClientIP();
                    break;
                case LsConstant.RequestClientName:
                    value = Utils.GetOsInfo();
                    break;               
                default:
                    throw new LSException(string.Format("无法获取key为{0}请求的请求信息",key));

            }
            return value;
        }


        public static async Task<Dictionary<string, string>> GetRequestParams(this HttpRequestMessage request)
        {
            var requestParams = new Dictionary<string, string>();

            if (request.Method == HttpMethod.Get)
            {
                var qString = request.RequestUri.ParseQueryString();
                //Array.Sort(qString.AllKeys);
                foreach (var q in qString.AllKeys)
                {
                    requestParams.Add(q.Split('.')[0].ToLower(), qString[q]);
                }
            }
            else
            {
                //actionContext.Request.Content
                // :todo 判断请求的数据是xml还是json
                string body = await request.Content.ReadAsStringAsync();
                //string body = content.Result;
                var data = (JObject)JsonConvert.DeserializeObject(body);
                foreach (var item in data)
                {
                    requestParams.Add(item.Key.ToLower(), item.Value.ToString());
                }
            }

            return requestParams;
        }

        public static User GetLoginUser(this HttpRequestMessage request)
        {

            var ticket = request.GetHeader(LsConstant.LOTTERY_SERVICE_TICKET);

            if (string.IsNullOrEmpty(ticket))
            {
                throw new LSException("请先登录系统.");
            }
            JObject playLoad = null;
            try
            {
                playLoad = AppUtils.GetPayloadFromToken(ticket, Utils.GetConfigValuesByKey(LsConstant.JwtSecret));
            }
            catch (Exception ex)
            {
                
                throw new LSException("票据无效，请通过合法的途径请求API");
            }

            // :todo 判断用户是否登录超时，如果登录超时，则直接登出

            // ：todo 重构， 缓存，需要从缓存中获取用户,不是直接从数据库中获取用户信息
            var appAccountService = ServiceLocator.Current.GetInstance<AccountAppService>();

            var user = appAccountService.GetUserByTokenId(playLoad[LsConstant.TokenId].ToString());

            if (user == null)
            {
                if (appAccountService.IsOnline(playLoad[LsConstant.AccountName].ToString()))
                {
                    throw new LSException("票据无效,请通过合法途径访问API");
                }
                // :todo 登出操作
                throw new LSException("用户已登出");
            }
            return user;
        }

        public static string GetHeader(this HttpRequestMessage request, string key)
        {
            IEnumerable<string> keys = null;
            if (!request.Headers.TryGetValues(key, out keys))
                return null;

            return keys.First();
        }


        public static HttpResponseMessage CreateErrorResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode,T value)
        {
            return request.CreateResponse(statusCode, value);
        }
    }
}