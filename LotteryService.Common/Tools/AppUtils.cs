using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using LotteryService.Common.Enums;
using Newtonsoft.Json.Linq;

namespace LotteryService.Common.Tools
{
    public class AppUtils
    {
        public static UserRegistType UserAccountType(string accountName)
        {
            if (Utils.IsMobilePhone(accountName))
            {
                return UserRegistType.Phone;
            }
            if (Utils.IsEmail(accountName))
            {
                return UserRegistType.Email;
            }
            if (Utils.IsLegalUserName(accountName))
            {
                return UserRegistType.UserName;
            }
            return UserRegistType.Illegal;
        }

        public static string EncryptPassword(string inputAccountName, string inputPassword, UserRegistType regiserType)
        {
            string firstEncryptStr = inputAccountName.ToLower() + inputPassword;
            string secondEncryptStr = Utils.EncryptSHA256(firstEncryptStr) + inputAccountName.ToLower() + regiserType.ToString().ToLower();
            return Utils.EncryptSHA256(secondEncryptStr);
        }

        public static bool VerifyPassword(string accountName, string inputPassword, string userInfoPassword,UserRegistType userRegistType)
        {
#if DEBUG
              return userInfoPassword.Equals(AppUtils.EncryptPassword(accountName, inputPassword, userRegistType));
#else
            // 生产环境中前端需要进行一次 SHA256(inputAccountName.ToLower() + inputPassword)
            return Utils.EncryptSHA256(inputPassword + accountName.ToLower() + userRegistType.ToString().ToLower())
                    .Equals(userInfoPassword);           
#endif
        }

        public static string GenerateToken(IDictionary<string,object> playload,string secret)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(playload, secret);
        }


        public static JObject GetPayloadFromToken(string token,string secret)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var jsonstr = decoder.Decode(token, secret, verify: true);

            return JObject.Parse(jsonstr);
        }


        public static string GenerateModules(int inputForecastCount)
        {
            // :todo 生成用户指标Module
            return "20,30,40,50";
        }

        public static string GetLotteryRedisKey(string lotteryType, string lotteryDataRedisKey)
        {
            return string.Format(lotteryDataRedisKey,lotteryType);
        }
    }
}