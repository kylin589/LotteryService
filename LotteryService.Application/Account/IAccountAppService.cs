using LotteryService.Application.Account.Dtos;
using LotteryService.Common.Dependency;

namespace LotteryService.Application.Account
{
    public interface IAccountAppService : ITransientDependency
    {
        UserCreateOutput Create(UserCreateInput input);
    }
}