using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;
using LotteryService.Domain.Services.Common;

namespace LotteryService.Domain.Lottery
{
    public class LotteryDataService : Service<LotteryData>, ILotteryDataService
    {
        public LotteryDataService(IRepository<LotteryData> repository, ILotteryDataDapperRepository dapperRepository) 
            : base(repository, dapperRepository)
        {
        }

        public bool ExsitData(string lotteryType, int period)
        {
            return ((ILotteryDataDapperRepository) _dapperRepository).ExsitData(lotteryType,period);
        }

        public LotteryData GetLatestLotteryData(string lotteryType)
        {
            return ((ILotteryDataDapperRepository)_dapperRepository).GetLatestLotteryData(lotteryType);
        }

        public LotteryData GetLotteryData(string lotteryType, int? peroiod)
        {
            return ((ILotteryDataDapperRepository)_dapperRepository).GetLotteryData(lotteryType,peroiod);
        }

        public IList<LotteryData> GetLotteryDatas(string lotteryType, int pageIndex, int pageSize, out int totalCount)
        {
            return ((ILotteryDataDapperRepository)_dapperRepository).GetLotteryDatas(lotteryType, pageIndex, pageSize,out totalCount);
        }

        public IDictionary<LotteryType, IList<LotteryData>> GetAnaylesBasicLotteryDatas(int basicHistoryCount)
        {
            return ((ILotteryDataDapperRepository)_dapperRepository).GetAnaylesBasicLotteryDatas(basicHistoryCount);
        }
    }
}