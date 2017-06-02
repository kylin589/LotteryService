
using LitJson;

namespace LotteryService.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return JsonMapper.ToJson(obj);           
        }
    }
}
