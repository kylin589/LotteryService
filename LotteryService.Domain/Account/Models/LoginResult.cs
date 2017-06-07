using System;
using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Excetions;
using LotteryService.Common.Tools;

namespace LotteryService.Domain.Services.Account.Models
{
    public class LoginResult
    {
        private readonly LoginResultType _loginResult;
        private readonly User _user;
        // private readonly ClaimsIdentity _claimsIdentity;

        private string _loginResultMsg;

        private string _tokenId;

        private string _token;

        private IDictionary<string, object> _payload;

        private UserRegistType _userRegistType;

        private string _accountName;

        private DateTime _loginTime;
    

        public string LoginResultMsg
        {
            get { return _loginResultMsg; }
        }

        public string TokenId
        {
            get
            {
                if (_loginResult != LoginResultType.Success)
                {
                    throw new LSException("登录失败,无法获取TokenId");
                }
                return _tokenId;
            }
        }

        public DateTime LoginTime {
            get { return _loginTime; }
        }

        public string Token
        {
            get
            {
                if (_loginResult != LoginResultType.Success)
                {
                    throw new LSException("登录失败,无法获取Token");
                }
                return _token;
            }
        }

        public LoginResult(LoginResultType resultType, User user = null)
        {
            _loginResult = resultType;
            _user = user;
            SetLoginResultMsg();
        }



        public LoginResult(User user, string accountName, UserRegistType userRegistType)
        {
            _loginResult = LoginResultType.Success;
            _accountName = accountName;
            _user = user;
            _userRegistType = userRegistType;
            SetLoginResultMsg();
            GenerateToken();
        }

        public LoginResult(Exception ex)
        {
            _loginResult = LoginResultType.ServerError;
            _loginResultMsg = ex.Message;
        }

        private void SetLoginResultMsg()
        {
            switch (_loginResult)
            {
                case LoginResultType.Success:
                    _loginResultMsg = "登录成功";
                    break;
                case LoginResultType.InvalidPassword:
                    _loginResultMsg = "密码错误,请核对您的密码";
                    break;
                case LoginResultType.InvalidUserNameAccount:
                    _loginResultMsg = "账号错误,请核对您的账号";
                    break;
                case LoginResultType.UserIsNotActive:
                    _loginResultMsg = "用户被冻结,请与客服联系";
                    break;
                case LoginResultType.UserAlreadyLogged:
                    _loginResultMsg = "用户已经登录,请不要重复登录";
                    break;
                default:
                    _loginResultMsg = "登录失败，原因:" + _userRegistType;
                    break;
            }
        }

        private void GenerateToken()
        {
            var secret = Utils.GetConfigValuesByKey(LsConstant.JwtSecret);
            _tokenId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            _payload = new Dictionary<string, object>();
            _payload[LsConstant.TokenId] = _tokenId;
            _loginTime = DateTime.Now;
            _payload[LsConstant.LoginTime] = _loginTime;
            _payload[LsConstant.AccountName] = _accountName;
            _token = AppUtils.GenerateToken(_payload, secret);

        }


        public LoginResultType ResultType
        {
            get { return _loginResult; }
        }

        public User User
        {
            get { return _user; }
        }


    }
}