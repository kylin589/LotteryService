namespace LotteryService.Common.Enums
{
    public enum UserRegistType
    {

        UserName = 1,
        Phone = 2,
        Email = 3,

        /// <summary>
        /// 不合法的用户名
        /// </summary>
        Illegal = -1,
    }
}