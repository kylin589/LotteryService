using Lottery.Entities;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Domain.Interfaces.Repository.Dapper
{
    public interface IUserDapperRepository : IDapperRepository<User>
    {
        
    }
}