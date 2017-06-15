using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace LotteryService.Domain.Interfaces.Service
{
    public interface ILotteryAnalyseNormService : ITransientDependency
    {
        IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll();

        UserBasicNorm GetUserBasicNorm(string userId, LotteryType lotteryType);

        bool SetUserBasicNorm(UserBasicNorm userBasicNorm);

        IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAllEnable();
    }
}