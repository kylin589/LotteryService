namespace LotteryService.Application.Lottery.Dtos
{
    public class UserBasicNormInput
    {

        /// <summary>
        /// 计划小周期
        /// </summary>
        public int PlanCycle { get; set; }

        /// <summary>
        /// 预测个数
        /// </summary>
        public int ForecastCount { get; set; }

        /// <summary>
        /// 指标历史开奖基数
        /// </summary>
        public int BasicHistoryCount { get; set; }

        /// <summary>
        /// 指标历史开奖单元数
        /// </summary>
        public int UnitHistoryCount { get; set; }

        /// <summary>
        /// 热号权重
        /// </summary>
        public int HotWeight { get; set; }

        /// <summary>
        /// 大小权重
        /// </summary>
        public int SizeWeight { get; set; }

        /// <summary>
        /// 三区间权重
        /// </summary>
        public int ThreeRegionWeight { get; set; }

        /// <summary>
        /// 遗漏值权重
        /// </summary>
        public int MissingValueWeight { get; set; }

        /// <summary>
        /// 奇偶权重
        /// </summary>
        public int OddEvenWeight { get; set; }

        /// <summary>
        /// 彩种 
        /// </summary>
        public string LotteryType { get; set; }

    }
}