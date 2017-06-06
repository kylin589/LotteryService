using LotteryService.Application.Account.Dtos;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace LotteryService.Application.Account
{
    public interface IAccountAppService : ITransientDependency
    {
        UserCreateOutput Create(UserCreateInput input);

       
    }
}