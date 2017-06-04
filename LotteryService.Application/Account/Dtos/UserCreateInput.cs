namespace LotteryService.Application.Account.Dtos
{
    public class UserCreateInput : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 重复密码
        /// </summary>
        public string RepeatPassword { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string IdentifyCode { get; set; }

    }
}