namespace LotteryService.Application.Lottery.Dtos
{
    /// <summary>
    /// 彩种计划
    /// </summary>
    public class PlanOutput
    {
        /// <summary>
        /// 计划Id
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 该计划是否选中
        /// </summary>
        public bool IsSelected { get; set; }

    }
}