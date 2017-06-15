using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Entities;
using LotteryService.Application.Lottery;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using Microsoft.Practices.ServiceLocation;
using LotteryService.Domain.Logs;

namespace Lottery.DataAnalyzer
{
    public class LotteryDataManager : ILotteryDataManager
    {

        private static ILotteryDataAppService _lotteryDataAppService;

        private static IDictionary<LotteryType, IList<LotteryData>> _historyLotteryDataDictionary;

        /// <summary>
        /// Redis 缓存历史开奖数据
        /// </summary>
        static LotteryDataManager()
        {           
            _lotteryDataAppService = ServiceLocator.Current.GetInstance<ILotteryDataAppService>();
            _historyLotteryDataDictionary = _lotteryDataAppService.GetAnaylesBasicLotteryDatas(LsConstant.LOAD_HISTORY_LOTTERYDATA);
            foreach (var item in _historyLotteryDataDictionary)
            {

                var lotteryDataRedisKey = string.Format(LsConstant.LotteryDataRedisKey, item.Key);
                if (RedisHelper.KeyExists(lotteryDataRedisKey))
                {
                    RedisHelper.KeyDelete(lotteryDataRedisKey);
                }
                foreach (var data in item.Value)
                {

                    RedisHelper.SetHash(lotteryDataRedisKey, data.Id, data);
                }
            }
        }

        /// <summary>
        /// 从缓存中读取历史开奖数据
        /// </summary>
        /// <param name="lotteryType">彩种</param>
        /// <returns></returns>
        public IList<LotteryData> GetHistoryLotteryDatas(LotteryType lotteryType)
        {
            return RedisHelper.GetAll<LotteryData>(string.Format(LsConstant.LotteryDataRedisKey, lotteryType));
        }

 
    }
}