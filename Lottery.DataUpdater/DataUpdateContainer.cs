using System;
using Lottery.DataUpdater.Models;
using Lottery.Entities;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace Lottery.DataUpdater
{
    public class DataUpdateContainer
    {
        private readonly LotteryUpdateConfig _lotteryUpdateConfig;
        private readonly IRepository<LotteryData> _lotteryDataRepository;
        private long _lastPeriod;
        private bool _isFirstStartService;
        private object _lockObject = new object();
        private DateTime _nextLotteryTime;

        public DataUpdateContainer(LotteryUpdateConfig lotteryUpdateConfig, IRepository<LotteryData> lotteryDataRepository)
        {
            _lotteryUpdateConfig = lotteryUpdateConfig;
            _lotteryDataRepository = lotteryDataRepository;
        }
    }
}