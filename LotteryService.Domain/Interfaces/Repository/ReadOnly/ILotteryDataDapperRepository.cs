using Lottery.Entities;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Domain.Interfaces.Repository.ReadOnly
{
    public interface ILotteryDataDapperRepository : IDapperRepository<LotteryData>
    {
        bool ExsitData(string lotteryType, int period);

        LotteryData GetLatestLotteryData(string lotteryType);
    }
}