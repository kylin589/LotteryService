using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Lottery.DataUpdater.Models;
using Lottery.Entities;
using Newtonsoft.Json.Linq;

namespace Lottery.DataUpdater.DataUpdateItems.Bjpks
{
    public class BjpksDataUpdateItem3 : DataUpdateItem
    {
        public BjpksDataUpdateItem3(DataSite dataSite, long latestDataId, string lotteryName) : base(dataSite, latestDataId, lotteryName)
        {
        }

        protected override List<LotteryData> CrawNewDatas()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string jsonString = client.DownloadString(_dataSite.Url);

            var jObject = JObject.Parse(jsonString);
            var dataList = new List<LotteryData>();
            if (Convert.ToBoolean(jObject["success"]))
            {
                var dataItems = (JArray)jObject["rows"];
                foreach (var row in dataItems)
                {
                    
                    var itemId = Convert.ToInt32(row["termNum"]);
                    if (itemId <= _latestDataId)
                        continue;
                    var lotteryData = new LotteryData();
                    lotteryData.Period = itemId;
                    lotteryData.Data = GetCpDataByCpJObject("n", 10, (JObject)row);
                    lotteryData.LotteryType = _lotteryName;
                    lotteryData.LotteryDateTime = Convert.ToDateTime(row["lotteryTime"]);
                    dataList.Add(lotteryData);
                }
            }
            return dataList;
        }
    }
}
