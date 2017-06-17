using System.Collections.Concurrent;
using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using Microsoft.Practices.ServiceLocation;

namespace Lottery.DataAnalyzer.Analyzer
{
    public class LotteryDataAnlyer : ILotteryDataAnlyer
    {
        private int _lotteryDataCount;
        private LotteryType _lotteryType;

        private readonly ILotteryDataManager _lotteryDataManager;

        private IList<LotteryData> _lotteryDataPackage;
        private LotteryType lotteryType;
        private IList<NumberInfo> _numberInfos;

        private IDictionary<int, LotteryNumberAnalyzer> _lotteryNumberAnalyzers;

        public LotteryDataAnlyer(LotteryType lotteryType, IList<NumberInfo> numberInfos, int lotteryDataCount)
        {
            _lotteryType = lotteryType;
            _lotteryDataCount = lotteryDataCount;
            _numberInfos = numberInfos;
            _lotteryNumberAnalyzers = new ConcurrentDictionary<int, LotteryNumberAnalyzer>();

            _lotteryDataManager = ServiceLocator.Current.GetInstance<ILotteryDataManager>();
            _lotteryDataPackage = _lotteryDataManager.GetHistoryLotteryDatas(lotteryType, lotteryDataCount);

            InitNumberInfoAnalyzers();
        }

        private void InitNumberInfoAnalyzers()
        {
            foreach (var numberInfo in _numberInfos)
            {
                var numberAnalyzer = new LotteryNumberAnalyzer(_lotteryDataPackage, numberInfo);
                _lotteryNumberAnalyzers[numberInfo.KeyNumber] = numberAnalyzer;
            }
        }
    }
}