using System;
using System.Collections.Generic;
using System.Web.Http;
using Lottery.DataAnalyzer;
using LotteryService.Application;
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
        private readonly ILotteryPlanManager _lotteryPlanManager;
        private readonly ILotteryAnalyseNormAppService _analyseNormAppService;

        public LotteryController(ILotteryDataAppService lotteryDataAppService, 
            ILotteryPlanManager lotteryPlanManager,
            ILotteryAnalyseNormAppService analyseNormAppService)
        {
            _lotteryDataAppService = lotteryDataAppService;
            _lotteryPlanManager = lotteryPlanManager;
            _analyseNormAppService = analyseNormAppService;
        }

        /// <summary>
        /// 获取指定彩种的历史开奖数据
        /// </summary>
        /// <param name="lotteryType">彩种</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [Route("lotterydatas")]
        [HttpGet]
        public ResultMessage<IPageList<LotteryDataOutput>> GetLotteryDatas(string lotteryType, int pageIndex = 1,int pageSize = 20)
        {            
            var data = _lotteryDataAppService.GetLotteryDatas(lotteryType, pageIndex, pageSize);
            return  ResponseUtils.DataResult(data);
        }

        /// <summary>
        /// 获取指定彩种指定期数的开奖结果
        /// </summary>
        /// <param name="lotteryType">彩种</param>
        /// <param name="peroiod">开奖期数</param>
        /// <returns></returns>
        [Route("lotterydata")]
        [HttpGet]
        public ResultMessage<LotteryDataOutput> GetLotteryData(string lotteryType, int? peroiod)
        {
            LotteryDataOutput lotteryData = null;
            var result = _lotteryDataAppService.GetLotteryData(lotteryType, peroiod, out lotteryData);
            if (result)
            {
                return ResponseUtils.DataResult(lotteryData);
            }
            string peroidStr = peroiod?.ToString() ?? "最后一期";
            return ResponseUtils.ErrorResult<LotteryDataOutput>($"获取{lotteryType}彩种第{peroidStr}期开奖数据失败,可能正在开奖...");
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
        /// 更新用户计划列表
        /// </summary>
        /// <param name="lotteryType">彩种</param>
        /// <param name="planIds">用户选择的彩种计划Ids</param>
        /// <returns></returns>
        [Route("userplan")]
        [HttpPut]
        public ResultMessage<IList<LotteryPlanGroupDto>> UpdateUserLotteryPlan(string lotteryType,IList<int> planIds)
        {
            _lotteryPlanManager.UpdateUserLotteryPlan(LoginUser.Id, Utils.StringConvertEnum<LotteryType>(lotteryType), planIds);
            return GetUserLotteryPlan(lotteryType);
        }

        /// <summary>
        /// 获取用户计划基础指标
        /// </summary>
        /// <returns></returns>
        [Route("userbasicplannorm")]
        [HttpGet]
        public ResultMessage<UserBasicNormDto> GetUserBasicNorm(string lotteryType)
        {
            // : todo 鉴权
            try
            {
                var userBasicNorm = _analyseNormAppService.GetUserBasicNorm(LoginUser.Id,
                    Utils.StringConvertEnum<LotteryType>(lotteryType));
                return ResponseUtils.DataResult(userBasicNorm);
            }
            catch (Exception ex)
            {

                return ResponseUtils.ErrorResult<UserBasicNormDto>(ex.Message);
            }
        }

        /// <summary>
        /// 新增/更新用户计划基础指标
        /// </summary>
        /// <returns></returns>
        [Route("userbasicplannorm")]
        [HttpPost]
        //[HttpPut]  屏蔽 Put方法
        public ResultMessage<UserBasicNormDto> UpdateBasicNorm(UserBasicNormInput input)
        {
            try
            {
                var userBasicNorm = _analyseNormAppService.SetUserBasicNorm(LoginUser.Id, input);
                return userBasicNorm;
            }
            catch (Exception ex)
            {

                return ResponseUtils.ErrorResult<UserBasicNormDto>(ex.Message);
            }
        }

    }
}
