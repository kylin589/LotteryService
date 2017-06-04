namespace LotteryService.Application.Account.Dtos
{
    public class UserCreateOutput : IDto
    {
        public bool IsSuccess { get; set; }

        public string Msg { get; set; }

        public string AccountName  { get; set; }
    }
}