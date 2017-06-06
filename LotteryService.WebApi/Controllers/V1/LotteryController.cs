using System.Collections.Generic;
using System.Web.Http;
using LotteryService.Application.Log;
using LotteryService.Application.Lottery;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Tools;
using LotteryService.WebApi.Controllers.Base;

namespace LotteryService.WebApi.Controllers.V1
{
    [RoutePrefix("lottery/v1")]
    [LsAuthenticationFilter]
    public class LotteryController : V1ControllerBase
    {
        private readonly ILotteryDataAppService _lotteryDataAppService;
        private readonly IAuditAppService _auditAppService;

        public LotteryController(ILotteryDataAppService lotteryDataAppService, 
            IAuditAppService auditAppService)
        {
            _lotteryDataAppService = lotteryDataAppService;
            _auditAppService = auditAppService;
        }

        [Route("data")]
        [HttpGet]
        public ResultMessage<IList<LotteryDataOutput>> GetLotteryData(int proid)
        {            
            var data = _lotteryDataAppService.GetLotteryData();
            return  ResponseUtils.DataResult(data);
        }
    
    }
}
