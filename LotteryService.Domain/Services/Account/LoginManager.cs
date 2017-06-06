using System;
using System.Security.Claims;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;
using LotteryService.Domain.Logs;
using LotteryService.Domain.Services.Account.Models;
using LotteryService.Domain.Services.Common;

namespace LotteryService.Domain.Services.Account
{
    public class LoginManager : DapperService<User>, ILoginManager
    {
        public LoginManager(IUserDapperRepository dapperRepository) : base(dapperRepository)
        {
        }

        public LoginResult Login(string accountName, string password)
        {
            var userInfo = ((IUserDapperRepository)_dapperRepository).GetUserByAccountName(accountName);

            LoginResult loginResult = null;
            LoginResultType loginResultType;
            // 1. 判断是否存在用户
            if (userInfo == null)
            {
                loginResultType = LoginResultType.InvalidUserNameAccount;
                loginResult = new LoginResult(loginResultType);
                LogDbHelper.LogWarn("登录失败,不存在用户{0}",GetType().FullName + "Login",OperationType.Account);
                return loginResult;
            }

            // 2. 判断密码是否正确
            else if (!AppUtils.VerifyPassword(accountName,password,userInfo.Password,userInfo.UserRegistType))
            {
                loginResultType = LoginResultType.InvalidPassword;
                loginResult = new LoginResult(loginResultType);
                
            }
            // 3. 判断用户状态是否被激活
            else if (!userInfo.IsActive)
            {
                loginResultType = LoginResultType.UserIsNotActive;
                loginResult = new LoginResult(loginResultType);
            }
            // 4.判断用户是否已经登录，如果登录了，提示用户不要重复登录
            //else if (!string.IsNullOrWhiteSpace(userInfo.TokenId))
            //{
            //    loginResultType = LoginResultType.UserAlreadyLogged;
            //    loginResult = new LoginResult(loginResultType);
            //}            
            else
            {

                loginResult = new LoginResult(userInfo, accountName, userInfo.UserRegistType);
            }
            try
            {
                if (loginResult.ResultType != LoginResultType.Success)
                {
                    ((IUserDapperRepository) _dapperRepository).LoginFail(userInfo.Id, accountName, loginResult.ResultType);
                    LogDbHelper.LogWarn("登录失败, 原因:" + loginResult.LoginResultMsg, GetType().FullName + "Login", OperationType.Account);
                }
                else
                {
                    ((IUserDapperRepository)_dapperRepository).LoginSuccess(userInfo.Id, accountName, loginResult.ResultType,
                        loginResult.TokenId,loginResult.LoginTime);
                    // :todo 缓存TokenId
                    LogDbHelper.LogWarn(loginResult.LoginResultMsg, GetType().FullName + "Login", OperationType.Account);
                }
            }
            catch (Exception ex)
            {

                LogDbHelper.LogError("登录失败, 原因:" + ex.ToString(), GetType().FullName + "Login", OperationType.Account);
            }


            return loginResult;
        }

       
    }
}