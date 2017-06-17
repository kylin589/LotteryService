namespace LotteryService.Common.Enums
{
    /// <summary>
    /// 三区间
    /// </summary>
    public enum ThreeRegion
    {
        /// <summary>
        /// 第一区间
        /// </summary>
        FirstRegion = 1,

        /// <summary>
        /// 第二区间
        /// </summary>
        SecondRegion = 2,

        /// <summary>
        /// 第三区间
        /// </summary>
        ThirdRegion = 3,
    }

    /// <summary>
    /// 大小形态
    /// </summary>
    public enum SizeShape
    {
        /// <summary>
        /// 小
        /// </summary>
        Small = 0,

        /// <summary>
        /// 大
        /// </summary>
        Big = 1,

    }

    /// <summary>
    /// 奇偶形态
    /// </summary>
    public enum OddEvenShape
    {
        /// <summary>
        /// 奇数形态
        /// </summary>
        Odd = 1,

        /// <summary>
        /// 偶数形态
        /// </summary>
        Even = 2,
    }

    /// <summary>
    /// 数据冷热形态
    /// </summary>
    public enum TemperShape
    {
        /// <summary>
        /// 热号
        /// </summary>
        Hot = 1,

        /// <summary>
        /// 温号
        /// </summary>
        Mild = 2,

        /// <summary>
        /// 冷号
        /// </summary>
        Cold = 3,
    }
}