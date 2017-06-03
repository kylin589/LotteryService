using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Lottery.DataUpdater.Models;
using Lottery.Entities;
using LotteryService.Common.Extensions;

namespace Lottery.DataUpdater.DataUpdateItems.Bjpks
{
    public class BjpksDataUpdateItem2 : DataUpdateItem
    {
        public BjpksDataUpdateItem2(DataSite dataSite, long latestDataId, string lotteryName) : base(dataSite, latestDataId, lotteryName)
        {
        }

        protected override List<LotteryData> CrawNewDatas()
        {
            WebClient client = new WebClient();

            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36");
            client.Encoding = Encoding.GetEncoding("UTF-8");
            var dataUrl = string.Format(_dataSite.Url, DateTime.Now.ToString("yyyy-MM-dd"));
            string documentStr = client.DownloadString(dataUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(documentStr);
            ////*[@id="content"]/div[1]/div[1]/div[4]/div/div[2]/table/tbody/tr[5]
            var cpNodeListWrap = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"drawing_tableDetail\"]//*/tr[*]").Skip(1);
            var dataList = new List<LotteryData>();
            foreach (var cp in cpNodeListWrap)
            {
                var item = new LotteryData();
                item.Period = Convert.ToInt32(cp.SelectSingleNode("./td[1]").InnerText);
                if (item.Period <= _latestDataId)
                    continue;               
                item.Data = cp.SelectNodes("./td[2]/div/ul/li").Select(p => int.Parse(p.InnerText)).ToList().ToSplitString(',');
                item.LotteryType = _lotteryName;
                item.LotteryDateTime = DateTime.Now;
                dataList.Add(item);
            }
            return dataList;
        }
    }
}