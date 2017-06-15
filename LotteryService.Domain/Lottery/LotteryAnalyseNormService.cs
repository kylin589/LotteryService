using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Domain.Lottery
{
    public class LotteryAnalyseNormService : ILotteryAnalyseNormService
    {
        private ILotteryAnalyseNormDapperRepostory _analyseNormDapperRepostory;

        public LotteryAnalyseNormService(ILotteryAnalyseNormDapperRepostory analyseNormDapperRepostory)
        {
            _analyseNormDapperRepostory = analyseNormDapperRepostory;
        }

        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll()
        {
            return _analyseNormDapperRepostory.GetAll();
        }

        public UserBasicNorm GetUserBasicNorm(string userId, LotteryType lotteryType)
        {
            return _analyseNormDapperRepostory.GetUserBasicNorm(userId,lotteryType);
        }

        public bool SetUserBasicNorm(UserBasicNorm userBasicNorm)
        {
            return _analyseNormDapperRepostory.SetUserBasicNorm(userBasicNorm);
        }

        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAllEnable()
        {
            return _analyseNormDapperRepostory.GetAllEnable();
        }
    }
}