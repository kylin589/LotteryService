using System;
using LotteryService.Common.Enums;

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
            string firstEncryptStr = inputAccountName + inputPassword;
            string secondEncryptStr = Utils.EncryptSHA256(firstEncryptStr) + firstEncryptStr + regiserType;
            return Utils.EncryptSHA256(secondEncryptStr);
        }
    }
}