using System.Collections.Generic;
using LotteryService.Application.Lottery;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer
{
    public class LotteryPlanManager : ILotteryPlanManager
    {
        private readonly ILotteryFeatureLoader _lotteryFeatureLoader;
        private readonly IAnylseNormAppService _anylseNormAppService;

        public LotteryPlanManager(ILotteryFeatureLoader lotteryFeatureLoader, 
            IAnylseNormAppService anylseNormAppService)
        {
            _lotteryFeatureLoader = lotteryFeatureLoader;
            _anylseNormAppService = anylseNormAppService;
        }


        public IList<LotteryPlanGroupDto> GetUserLotteryPlans(string userId, LotteryType lotteryType)
        {
            var lotteryPlanGroupDtos = new List<LotteryPlanGroupDto>();
            var selectedPlanGroup = new LotteryPlanGroupDto()
            {
                LotteryType = lotteryType.ToString(),
                GroupId =  0,
                GroupName = "已选计划",
                IsSelecedGroup = true,
                Plans = new List<PlanOutput>(),
            };

            lotteryPlanGroupDtos.Add(selectedPlanGroup);

            var lotteryFeature = _lotteryFeatureLoader.LoadLotteryFeature(lotteryType);
            var userSelectedPlans = _anylseNormAppService.GetUserSelectedPlans(userId,lotteryType);
            foreach (var normGroup in lotteryFeature.LotteryNorm.NormGroup)
            {
                var planGroupDto = new LotteryPlanGroupDto()
                {
                    LotteryType = lotteryType.ToString(),
                    GroupId = normGroup.GroupId,
                    GroupName = normGroup.Cname,
                    IsSelecedGroup = false,
                    Plans = new List<PlanOutput>(),
                };
                foreach (var plan in normGroup.Plans)
                {
                    var planDto = new PlanOutput()
                    {
                        PlanId = plan.PlanId,
                        PlanName = plan.Name,
                        IsSelected = false
                    };
                    if (userSelectedPlans.Contains(planDto.PlanId))
                    {
                        planDto.IsSelected = true;
                        selectedPlanGroup.Plans.Add(planDto);
                    }
                    planGroupDto.Plans.Add(planDto);
                }
                lotteryPlanGroupDtos.Add(planGroupDto);
            }
           
            return lotteryPlanGroupDtos;
        }

        public bool UpdateUserLotteryPlan(string userId, LotteryType lotteryType, IList<int> planIds)
        {
            return _anylseNormAppService.UpdateUserLotteryPlan(userId, lotteryType, planIds);
        }
    }
}