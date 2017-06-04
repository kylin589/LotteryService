namespace LotteryService.Application
{
    public class BaseDto : IDto
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 请求签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
    }
}