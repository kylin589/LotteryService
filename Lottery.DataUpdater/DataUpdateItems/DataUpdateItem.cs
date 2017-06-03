using System;
using System.Collections.Generic;
using System.Text;
using Lottery.DataUpdater.Models;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Logs;
using Newtonsoft.Json.Linq;

namespace Lottery.DataUpdater.DataUpdateItems
{
    public abstract class DataUpdateItem
    {
        protected readonly DataSite _dataSite;
        protected readonly long _latestDataId;
        protected readonly string _lotteryName;
        protected bool _iscrawlingData = false;

        protected DataUpdateItem(DataSite dataSite, long latestDataId, string lotteryName)
        {
            _dataSite = dataSite;
            _latestDataId = latestDataId;
            _lotteryName = lotteryName;
        }

        public IList<LotteryData> RequestNewDatas()
        {
            if (_iscrawlingData)
            {
                return null;
            }
            _iscrawlingData = true;

            try
            {
                LogDbHelper.LogDebug("正在从" + _dataSite.Name + "爬取数据",GetType().FullName + "=>RequestNewDatas",OperationType.Job);
                var dataList = CrawNewDatas();
                if (dataList.Count <= 0)
                {
                    LogDbHelper.LogDebug("从" + _dataSite.Name + "爬取数据失败,没有获取到最新的数据", GetType().FullName + "=>RequestNewDatas", OperationType.Job);
                }
                else
                {
                    LogDbHelper.LogDebug("从" + _dataSite.Name + "爬取数据成功,共爬取到" + dataList.Count + "条数据",GetType().FullName + "=>RequestNewDatas", OperationType.Job);
                }

                return dataList;
            }
            catch (Exception e)
            {
                LogDbHelper.LogError("从" + _dataSite.Name + "爬取数据失败，原因" + e.Message, GetType().FullName + "=>RequestNewDatas", OperationType.Job);
                return null;
            }
            finally
            {
                _iscrawlingData = false;
            }

        }

        protected abstract List<LotteryData> CrawNewDatas();

        protected virtual string GetCpDataByCpJObject(string prefix, int itemNum, JObject itemData)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= itemNum; i++)
            {
                sb.Append(i < itemNum
                    ? string.Format("{0},", itemData[prefix + i])
                    : string.Format("{0}", itemData[prefix + i]));
            }

            return sb.ToString();
        }
    }
}