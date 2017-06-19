using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

                var lotteryDataRedisKey = string.Format(LsConstant.LotteryDataCacheKey, item.Key);
                if (RedisHelper.KeyExists(lotteryDataRedisKey))
                {
                    RedisHelper.KeyDelete(lotteryDataRedisKey);
                }
                //foreach (var data in item.Value)
                //{

                //    RedisHelper.SetHash(lotteryDataRedisKey, data.Id, data);
                //}
                //Parallel.ForEach(item.Value, new ParallelOptions()
                //{
                //    MaxDegreeOfParallelism = LsConstant.MaxDegreeOfParallelism,
                //}, lotteryData =>
                //{
                //    RedisHelper.SetHash(lotteryDataRedisKey, lotteryData.Id, lotteryData);
                //});

                //RedisHelper.AddList(lotteryDataRedisKey, item.Value);
                //item.Value.AsParallel().ForAll(val =>
                //{
                //    RedisHelper.AddList(lotteryDataRedisKey, val);
                //});

                CacheHelper.SetCache(lotteryDataRedisKey, item.Value);

            }
        }

        /// <summary>
        /// 从缓存中读取历史开奖数据
        /// </summary>
        /// <param name="lotteryType">彩种</param>
        /// <returns></returns>
        public IList<LotteryData> GetHistoryLotteryDatas(LotteryType lotteryType)
        {
            return CacheHelper.GetCache<IList<LotteryData>>(string.Format(LsConstant.LotteryDataCacheKey, lotteryType));

        }

        public IList<LotteryData> GetHistoryLotteryDatas(LotteryType lotteryType, int count)
        {
            return GetHistoryLotteryDatas(lotteryType).Take(count).ToList();
        }

        public LotteryData GetLastLotteryDatas(LotteryType lotteryType)
        {
            var key = AppUtils.GetLotteryRedisKey(lotteryType.ToString(), LsConstant.LastLotteryDataCacheKey);
            return CacheHelper.GetCache<LotteryData>(key);
        }
    }
}