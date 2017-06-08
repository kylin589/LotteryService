using System;
using System.Web.Http;
using Lottery.Entities;
using LotteryService.Application.Account;
using LotteryService.Application.Account.Dtos;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Excetions;
using LotteryService.Common.Tools;
using LotteryService.Domain.Account.Models;
using LotteryService.Domain.Interfaces.Service;
using LotteryService.WebApi.Controllers.Base;


namespace LotteryService.WebApi.Controllers.V1
{
    [RoutePrefix("account/v1")]
    public class AccountController : V1ControllerBase
    {

        private readonly IAccountAppService _accountAppService;
        private readonly ILoginManager _loginManager;

        public AccountController(IAccountAppService accountAppService,
            ILoginManager loginManager)
        {
            _accountAppService = accountAppService;
            _loginManager = loginManager;
           
        }

        /// <summary>
        /// 注册用户(创建账号)
        /// </summary>
        /// <param name="input">入参</param>
        /// <returns></returns>
        [Route("user")]
        [HttpPost]
        [EncryptAuditLogParams]
        [AllowAnonymous]
        public ResultMessage<UserCreateOutput> Create(UserCreateInput input)
        {
            var result = _accountAppService.Create(input);
            if (result.IsSuccess)
            {
                return ResponseUtils.DataResult(result);
            }
            return ResponseUtils.ErrorResult<UserCreateOutput>(result.Msg);
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        [EncryptAuditLogParams]
        [AllowAnonymous]
        public ResultMessage<UserLoginOutput> Login(UserLoginInput loginInput)
        {
            try
            {
                var loginResult = GetLoginResult(
                    loginInput.AccountName,
                    loginInput.Password
                );
                return ResponseUtils.DataResult(new UserLoginOutput()
                {
                    LoginResultMsg = loginResult.LoginResultMsg,
                    Ticket = loginResult.Token
                });
            }
            catch (Exception ex)
            {

                return ResponseUtils.ErrorResult<UserLoginOutput>(ex.Message);
            }

        }

        [Route("logout")]
        [HttpPost]
        public ResultMessage<string> Logout()
        {
            var loginUser = Request.GetLoginUser();
            _loginManager.Logout(loginUser.TokenId);
            return ResponseUtils.DataResult("登出成功");
        }

        private LoginResult GetLoginResult(string accountName, string password)
        {
            var loginResult = _loginManager.Login(accountName, password);
            switch (loginResult.ResultType)
            {
                case LoginResultType.Success:
                    return loginResult;
                default:
                    throw new LSException("登录不成功,原因:" + loginResult.LoginResultMsg );

            }
        }
    }
}