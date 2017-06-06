using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Domain.Interfaces.Service.Common;

namespace LotteryService.Domain.Interfaces.Service
{
    public interface IAccountService : IDapperService<User>, ITransientDependency
    {
        User GetUserByTokenId(string tokenId);

        User GetUserByAccountName(string accountName);
    }
}