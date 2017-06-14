using Lottery.Entities;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Domain.Interfaces.Repository.Dapper
{
    public interface ILotteryDataDapperRepository : IDapperRepository<LotteryData>
    {
        bool ExsitData(string lotteryType, int period);

        LotteryData GetLatestLotteryData(string lotteryType);

        LotteryData GetLotteryData(string lotteryType, int? peroiod);
    }
}