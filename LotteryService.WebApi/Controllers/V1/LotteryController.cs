using System.Collections.Generic;
using System.Web.Http;
using Lottery.DataAnalyzer;
using Lottery.Entities;
using LotteryService.Application.Log;
using LotteryService.Application.Lottery;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Enums;
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
        private readonly ILotteryPlanManager _lotteryPlanManager;

        public LotteryController(ILotteryDataAppService lotteryDataAppService, 
            IAuditAppService auditAppService, 
            ILotteryPlanManager lotteryPlanManager)
        {
            _lotteryDataAppService = lotteryDataAppService;
            _auditAppService = auditAppService;
            _lotteryPlanManager = lotteryPlanManager;
        }

        [Route("data")]
        [HttpGet]
        public ResultMessage<IList<LotteryDataOutput>> GetLotteryData(int period)
        {            
            var data = _lotteryDataAppService.GetLotteryData();
            return  ResponseUtils.DataResult(data);
        }

        /// <summary>
        /// 获取用户计划列表
        /// </summary>
        /// <remarks>获取用户设置的计划信息,如果该用户没有设置过彩种计划，则返回计划中默认的计划</remarks>
        /// <param name="lotteryType">彩种</param>
        /// <returns></returns>
        [Route("userplan")]
        [HttpGet]
        public ResultMessage<IList<LotteryPlanGroupDto>> GetUserLotteryPlan(string lotteryType)
        {           
            var userPlans = _lotteryPlanManager.GetUserLotteryPlans(LoginUser.Id,Utils.StringConvertEnum<LotteryType>(lotteryType));
            return ResponseUtils.DataResult(userPlans);
        }

        /// <summary>
        /// 更新用户列表
        /// </summary>
        /// <returns></returns>
        [Route("userplan")]
        [HttpPut]
        public ResultMessage<IList<LotteryPlanGroupDto>> UpdateUserLotteryPlan()
        {
            // : todo 鉴权
            return null;
        }

    }
}
