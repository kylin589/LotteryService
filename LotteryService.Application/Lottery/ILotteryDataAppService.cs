using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common.Dependency;

namespace LotteryService.Application.Lottery
{
    public interface ILotteryDataAppService : ITransientDependency
    {
        IList<LotteryDataOutput> GetLotteryData();

        LotteryData Insert(LotteryData newData);

        bool ExsitData(string lotteryType, int period);

        LotteryData GetLatestLotteryData(string lotteryType);

        bool GetLotteryData(string lotteryType, int? peroiod, out LotteryDataOutput lotteryData);
    }
}