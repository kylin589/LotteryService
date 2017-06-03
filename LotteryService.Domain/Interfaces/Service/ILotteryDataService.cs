using Lottery.Entities;
using LotteryService.Domain.Interfaces.Service.Common;

namespace LotteryService.Domain.Interfaces.Service
{
    public interface ILotteryDataService : IService<LotteryData>
    {
        bool ExsitData(string lotteryType, int period);

        LotteryData GetLatestLotteryData(string lotteryType);
    }
}