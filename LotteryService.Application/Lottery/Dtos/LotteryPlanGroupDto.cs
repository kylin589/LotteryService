using System.Collections.Generic;
using Lottery.Entities;

namespace LotteryService.Application.Lottery.Dtos
{
    /// <summary>
    /// 计划分组
    /// </summary>
    public class LotteryPlanGroupDto : IDto
    {
        /// <summary>
        /// 彩种
        /// </summary>
        public string LotteryType { get; set; }

        /// <summary>
        /// 计划组Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 计划组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 是否是已选的计划
        /// </summary>
        public bool IsSelecedGroup { get; set; }

        /// <summary>
        /// 所含计划
        /// </summary>
        public IList<PlanOutput> Plans { get; set; }
  
    }
}