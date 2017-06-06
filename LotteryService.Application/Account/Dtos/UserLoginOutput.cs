namespace LotteryService.Application.Account.Dtos
{
    /// <summary>
    /// 用户登录API返回消息
    /// </summary>
    public class UserLoginOutput : IDto
    {
        /// <summary>
        /// 用户登录成功后返回的Token,登录成功后，每次请求API都需要在Header中添加请求头  LOTTERY_SERVICE_TICKET 
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 登录返回消息
        /// </summary>
        public string LoginResultMsg { get; set; }
    }
}