
using System;
using LitJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LotteryService.Common.Extensions
{
    public static class JsonExtensions
    {

        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            //options.Converters.Insert(0, new LsDateTimeConverter());

            return JsonConvert.SerializeObject(obj, options);
        }

        public static T ToObject<T>(this string jsonStr, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.DeserializeObject<T>(jsonStr, options);
        }

    }
}
