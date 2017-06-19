using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Lottery.DataUpdater.DataUpdateItems;
using Lottery.DataUpdater.Jobs;
using Lottery.DataUpdater.Models;
using Lottery.Engine;
using Lottery.Entities;
using LotteryService.Application.Lottery;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Excetions;
using LotteryService.Common.Tools;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Logs;
using Microsoft.Practices.ServiceLocation;

namespace Lottery.DataUpdater
{
    public class DataUpdateContainer
    {
        private readonly LotteryUpdateConfig _lotteryUpdateConfig;
        private readonly ILotteryDataAppService _lotteryDataAppService;
        private LotteryDataJob _lotteryDataJob;
        private int _lastPeriod;
        private static object _lockObject = new object();

        public DataUpdateContainer(
            LotteryUpdateConfig lotteryUpdateConfig,
            ILotteryDataAppService lotteryDataAppService,
            LotteryDataJob lotteryDataJob)
        {
            _lotteryUpdateConfig = lotteryUpdateConfig;
            _lotteryDataAppService = lotteryDataAppService;
            _lotteryDataJob = lotteryDataJob;

        }

        protected int LastPeriod
        {
            get
            {
                LoadLatestData();
                return _lastPeriod;
            }

        }

        private void LoadLatestData()
        {
            var latestData = _lotteryDataAppService.GetLatestLotteryData(_lotteryDataJob.LotteryType.ToString());
            if (latestData == null)
            {
                _lastPeriod = -1;
            }
            else
            {
                _lastPeriod = latestData.Period;
            }
        }

        public void Execute()
        {
            RequestNewDataAsync();

        }

        private void RequestNewDataAsync()
        {
            foreach (var dataSite in _lotteryUpdateConfig.DataSites)
            {
                if (!dataSite.Enable)
                    continue;
                Action<DataSite> requestDataAction = RequestData;
                requestDataAction.BeginInvoke(dataSite, null, null);

            }
        }

        private void RequestData(DataSite dataSite)
        {

            var dataUpdateItem = ObtainDataUpdateItemByReflect(dataSite);
            if (dataUpdateItem == null)
            {
                throw new LSException("获取彩票开始数据数据爬取器失败.=>" + dataSite.Name + dataSite.Id);
            }

            var newDataList = dataUpdateItem.RequestNewDatas();
            lock (_lockObject)
            {
                if (newDataList != null && newDataList.Count > 0)
                {
                    //必须排序
                    newDataList = newDataList.OrderBy(p => p.Period).ToList();
                    foreach (var newData in newDataList)
                    {
                        if (!_lotteryDataAppService.ExsitData(_lotteryDataJob.LotteryType.ToString(), newData.Period))
                        {
                            var lastLotteryData = _lotteryDataAppService.Insert(newData);
                            _lastPeriod = lastLotteryData.Period;
                            string key = AppUtils.GetLotteryRedisKey(lastLotteryData.LotteryType, LsConstant.LastLotteryDataCacheKey);
                            CacheHelper.SetCache(key, lastLotteryData);
                            // todo:更新彩票分析数据   
                            var lotteryEngine =
                                LotteryEngine.GetLotteryEngine(
                                    Utils.StringConvertEnum<LotteryType>(lastLotteryData.LotteryType));
                            lotteryEngine.CalculateNextLotteryData();
                        }
                    }
                    LogDbHelper.LogDebug(string.Format("更新彩票{0}开奖数据数据成功,共{1}条数据", _lotteryDataJob.LotteryType, newDataList.Count),
                        GetType().FullName + "=>RequestData", OperationType.Job);
                    _lotteryDataJob.NextLotteryTime = _lotteryUpdateConfig.NextLotteryTime;

                }
            }
        }

        private DataUpdateItem ObtainDataUpdateItemByReflect(DataSite dataSite)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string updaterFullName = "Lottery.DataUpdater.DataUpdateItems."
                                     + Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(_lotteryUpdateConfig.Name) + "." +
                                     Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(_lotteryUpdateConfig.Name) +
                                     "DataUpdateItem" + dataSite.Id;

            DataUpdateItem updateItem = assembly.CreateInstance(updaterFullName, false, BindingFlags.CreateInstance, null, new object[] { dataSite, LastPeriod, _lotteryUpdateConfig.Name }, null, null) as DataUpdateItem;
            return updateItem;
        }
    }
}