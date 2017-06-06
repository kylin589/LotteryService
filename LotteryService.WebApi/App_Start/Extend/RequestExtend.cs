using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using LotteryService.Common;
using LotteryService.Common.Excetions;
using LotteryService.Common.Tools;
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

      
    }
}