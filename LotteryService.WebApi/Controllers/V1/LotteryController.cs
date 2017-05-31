using System.Collections.Generic;
using System.Web.Http;
using LotteryService.Application.Lottery;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.WebApi.Controllers.Base;

namespace LotteryService.WebApi.Controllers.V1
{
    [RoutePrefix("lottery/v1")]
    public class LotteryController : V1ControllerBase
    {
        private readonly ILotteryDataAppService _lotteryDataAppService;

        public LotteryController(ILotteryDataAppService lotteryDataAppService)
        {
            _lotteryDataAppService = lotteryDataAppService;
        }

        [Route("data")]
        public ResultMessage<IList<LotteryDataOutput>> GetLotteryData()
        {
            
            var data = _lotteryDataAppService.GetLotteryData();
            return  new ResultMessage<IList<LotteryDataOutput>>(data);
        }
    }
}
