using System.Collections.Generic;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common.Dependency;

namespace LotteryService.Application.Lottery
{
    public interface ILotteryDataAppService : ITransientDependency
    {
        IList<LotteryDataOutput> GetLotteryData();

        void Add(LotteryDataInput lotteryDataInput);
    }
}