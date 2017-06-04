using System;
using System.Linq;
using Lottery.Entities;
using Lottery.Entities.Extend.Validation;
using LotteryService.Application.Account.Dtos;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Data.Context;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Application.Account
{
    public class AccountAppService : AppService<LotteryDbContext>, IAccountAppService
    {
        private readonly IAccountService _accountService;

        public AccountAppService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public UserCreateOutput Create(UserCreateInput input)
        {
            #region 输入确认

            if (string.IsNullOrEmpty(input.AccountName))
            {
                return new UserCreateOutput()
                {
                    IsSuccess = false,
                    Msg = "账号不能为空",
                };
            }
            if (string.IsNullOrEmpty(input.Password))
            {
                return new UserCreateOutput()
                {
                    IsSuccess = false,
                    Msg = "密码不能为空"
                };
            }
            if (!input.Password.Equals(input.RepeatPassword))
            {
                return new UserCreateOutput()
                {
                    IsSuccess = false,
                    Msg = "密码不一致，请确认您的密码"
                };
            }

            #endregion

            var regiserType = AppUtils.UserAccountType(input.AccountName);

            User user = null;
            switch (regiserType)
            {
                case UserRegistType.UserName:
                    user = new User()
                    {
                        UserName = input.AccountName,
                        UserRegistType = regiserType,
                    };
                    break;
                case UserRegistType.Phone:
                    user = new User()
                    {
                        Phone = input.AccountName,
                        UserRegistType = regiserType,
                    };
                    break;
               case UserRegistType.Email:
                   user = new User()
                   {
                       Email = input.AccountName,
                       UserRegistType = regiserType
                   }; 
                    break;
                default:
                    return new UserCreateOutput()
                    {
                        IsSuccess = false,
                        Msg = "您输入的账号不合法，请输入正确的用户名(以'字母'或'_'开头,3~16位字母与数字组合)|手机|Email",
                    };
                                    
            }
            user.Password = AppUtils.EncryptPassword(input.AccountName,input.Password,regiserType);
            try
            {
                var result = _accountService.Add(user);
                if (result.IsValid)
                {
                    return new UserCreateOutput()
                    {
                        IsSuccess = false,
                        Msg = "注册成功",
                        AccountName = input.AccountName
                    };
                }
                return new UserCreateOutput()
                {
                    IsSuccess = true,
                    Msg = "注册失败，原因:" + result.Errors.Select(p=>p.Message),
                    AccountName = input.AccountName
                };
            }
            catch (Exception ex)
            {
                return new UserCreateOutput()
                {
                    IsSuccess = false,
                    Msg = "注册失败,原因:" + ex.Message + ",请稍后重试",
                };
            }
        }
    }
}