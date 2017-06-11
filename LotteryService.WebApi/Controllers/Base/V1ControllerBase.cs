using System.Web.Http;
using Lottery.Entities;
using LotteryService.Application.Account.Dtos;

namespace LotteryService.WebApi.Controllers.Base
{
    public class V1ControllerBase : ApiController
    {
        protected const string TAG = "Lottery.Api.V1";

        /// <summary>
        /// 登录的用户
        /// </summary>
        protected UserDto LoginUser => Request.GetLoginUser();
    }
}