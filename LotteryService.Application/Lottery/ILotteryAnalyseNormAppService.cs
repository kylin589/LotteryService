using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace LotteryService.Application.Lottery
{
    public interface ILotteryAnalyseNormAppService : ITransientDependency
    {
        IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll();

        UserBasicNormDto GetUserBasicNorm(string userId, LotteryType lotteryType);

        ResultMessage<UserBasicNormDto> SetUserBasicNorm(string userId, UserBasicNormInput input);
    }
}