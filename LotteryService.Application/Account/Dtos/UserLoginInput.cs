namespace LotteryService.Application.Account.Dtos
{
    public class UserLoginInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 密码,前端需要进行一次 SHA256(inputAccountName.ToLower() + inputPassword)
        /// </summary>
        public string Password { get; set; }
    }
}