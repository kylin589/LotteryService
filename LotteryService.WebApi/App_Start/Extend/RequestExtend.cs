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
                    value = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
                    break;
                case LsConstant.RequestClientAddress:
                    value = IpHelper.GetClientIP();
                    break;
                case LsConstant.RequestClientName:
                    value = SystemCheck(HttpContext.Current.Request);
                    break;
                case LsConstant.RequestParameters:
                    value ="{}";
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

        public static string SystemCheck(HttpRequest request)
        {
            string agent = request.ServerVariables["HTTP_USER_AGENT"];
            if (agent.IndexOf("NT 4.0", StringComparison.Ordinal) > 0)
                return "Windows NT ";
            if (agent.IndexOf("NT 5.0", StringComparison.Ordinal) > 0)
                return "Windows 2000";
            if (agent.IndexOf("NT 5.1", StringComparison.Ordinal) > 0)
                return "Windows XP";
            if (agent.IndexOf("NT 5.2", StringComparison.Ordinal) > 0)
                return "Windows 2003";
            if (agent.IndexOf("NT 6.0", StringComparison.Ordinal) > 0)
                return "Windows Vista";
            if (agent.IndexOf("NT 7.0", StringComparison.Ordinal) > 0)
                return "Windows 7";
            if (agent.IndexOf("NT 8.0", StringComparison.Ordinal) > 0)
                return "Windows 8";
            if (agent.IndexOf("NT 10.0", StringComparison.Ordinal) > 0)
                return "Windows 10";
            if (agent.IndexOf("WindowsCE", StringComparison.Ordinal) > 0)
                return "Windows CE";
            if (agent.IndexOf("NT", StringComparison.Ordinal) > 0)
                return "Windows NT ";
            if (agent.IndexOf("9x", StringComparison.Ordinal) > 0)
                return "Windows ME";
            if (agent.IndexOf("98", StringComparison.Ordinal) > 0)
                return "Windows 98";
            if (agent.IndexOf("95", StringComparison.Ordinal) > 0)
                return "Windows 95";
            if (agent.IndexOf("Win32", StringComparison.Ordinal) > 0)
                return "Win32";
            if (agent.IndexOf("Linux", StringComparison.Ordinal) > 0)
                return "Linux";
            if (agent.IndexOf("SunOS", StringComparison.Ordinal) > 0)
                return "SunOS";
            if (agent.IndexOf("Mac", StringComparison.Ordinal) > 0)
                return "Mac";
            if (agent.IndexOf("Linux", StringComparison.Ordinal) > 0)
                return "Linux";
             if (agent.IndexOf("Windows", StringComparison.Ordinal) > 0)
                return "Windows";
            return "未知类型";
        }
    }
}