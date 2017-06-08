using Lottery.Entities;
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
    }
}