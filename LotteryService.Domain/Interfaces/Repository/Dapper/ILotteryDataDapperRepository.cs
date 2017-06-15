using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Domain.Interfaces.Repository.Dapper
{
    public interface ILotteryDataDapperRepository : IDapperRepository<LotteryData>
    {
        bool ExsitData(string lotteryType, int period);

        LotteryData GetLatestLotteryData(string lotteryType);

        LotteryData GetLotteryData(string lotteryType, int? peroiod);

        IList<LotteryData> GetLotteryDatas(string lotteryType, int pageIndex, int pageSize,out int totalCount);

        IDictionary<LotteryType, IList<LotteryData>> GetAnaylesBasicLotteryDatas(int basicHistoryCount);
    }
}