using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Lottery.DataUpdater.Models;
using Lottery.Entities;
using LotteryService.Common.Tools;
using Newtonsoft.Json.Linq;

namespace Lottery.DataUpdater.DataUpdateItems.Bjpks
{
    public class BjpksDataUpdateItem1 : DataUpdateItem
    {
        public BjpksDataUpdateItem1(DataSite dataSite, long latestDataId,string lotteryName) : base(dataSite, latestDataId, lotteryName)
        {
        }

        protected override List<LotteryData> CrawNewDatas()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string jsonString = client.DownloadString((string) _dataSite.Url);

            var dataItems = JArray.Parse(jsonString);
            var dataList = new List<LotteryData>();
            foreach (var jToken in dataItems)
            {
                var dataItem = (JObject)jToken;
            
                var itemId = Convert.ToInt32(dataItem["pissue"]);
              
                if (itemId <= _latestDataId)
                    continue;
                var item = new LotteryData();
                item.Period = itemId;
                item.Data = GetCpDataByCpJObject("pn", 10, dataItem);
                item.LotteryType = _lotteryName;
                item.LotteryDateTime = Utils.UnixTimestampToDateTime(Convert.ToInt32(dataItem["pintime"].ToString()));
                dataList.Add(item);
            }
            return dataList;
        }
    }
}