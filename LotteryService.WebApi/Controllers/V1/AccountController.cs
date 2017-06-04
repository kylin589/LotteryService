using System.Web.Http;
using LotteryService.Application.Account;
using LotteryService.Application.Account.Dtos;
using LotteryService.Common;
using LotteryService.Common.Tools;
using LotteryService.WebApi.Controllers.Base;

namespace LotteryService.WebApi.Controllers.V1
{
    [RoutePrefix("account/v1")]
    public class AccountController : V1ControllerBase
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [Route("user")]
        [HttpPost]
        public ResultMessage<UserCreateOutput> Create(UserCreateInput input)
        {
            var result = _accountAppService.Create(input);
            if (result.IsSuccess)
            {
                return ResponseUtils.DataResult(result);
            }
            return ResponseUtils.ErrorResult<UserCreateOutput>(result.Msg);
        }
    }
}