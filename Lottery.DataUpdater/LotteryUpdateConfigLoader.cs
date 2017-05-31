using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using Lottery.DataUpdater.Models;
using LotteryService.Common.Dependency;

namespace Lottery.DataUpdater
{
    public class LotteryUpdateConfigLoader : ILotteryUpdateConfigLoader
    {
        private IList<LotteryUpdateConfig> _lotteryUpdateConfigs;

        public LotteryUpdateConfigLoader()
        {
            _lotteryUpdateConfigs = new List<LotteryUpdateConfig>();
            LoaderLotteryUpdateConfig();
        }

        private void LoaderLotteryUpdateConfig()
        {
            var xmlDoc = new XmlDocument();
            var xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LotteryData.config");
            xmlDoc.Load(xmlFilePath);
            var lotteries = xmlDoc.SelectSingleNode("/lotterys")?.ChildNodes;
            foreach (XmlNode node in lotteries)
            {
                Debug.Assert(node.Attributes != null, "node.Attributes != null");
                var lottery = new LotteryUpdateConfig()
                {
                    Name = node.Attributes["name"]?.InnerText,
                    Cname = node.Attributes["cname"]?.InnerText,
                    Interval = Convert.ToInt32(node.Attributes["interval"]?.InnerText),
                    TimeRules = LoaderTimeRules(node),
                    DataSites = LoaderDataSites(node),
                    Weekdays = LoaderWeekdays(node),
                };
                _lotteryUpdateConfigs.Add(lottery);
            }
        }

        private int[] LoaderWeekdays(XmlNode node)
        {
            var weekDaysStr = node.SelectSingleNode("./timeRules")?.Attributes?["weekdays"].InnerText;
            var weekDays = weekDaysStr.Split(',').Select(p => Convert.ToInt32(p)).ToArray();
            return weekDays;
        }

        private IList<DataSite> LoaderDataSites(XmlNode node)
        {
            var datasites = new List<DataSite>();

            foreach (XmlNode datasiteNode in node.SelectSingleNode("./datasites").ChildNodes)
            {
                var dataSite = new DataSite()
                {
                    Id = Convert.ToInt32(datasiteNode.Attributes?["id"].InnerText),
                    Count = Convert.ToInt32(datasiteNode.Attributes?["count"].InnerText),
                    Name = datasiteNode.Attributes?["name"].InnerText,
                    Enable = Convert.ToBoolean(datasiteNode.Attributes?["enable"].InnerText),
                    Url = datasiteNode.Attributes?["url"].InnerText
                };

                datasites.Add(dataSite);
            }
            return datasites;
        }

        private IList<TimeRule> LoaderTimeRules(XmlNode node)
        {
            var timeRules = new List<TimeRule>();

            foreach (XmlNode timeRuleNode in node.SelectSingleNode("./timeRules")?.ChildNodes)
            {

                var timeRule = new TimeRule()
                {
                    StartTime = Convert.ToDateTime(timeRuleNode.Attributes["startTime"]?.InnerText),
                    EndTime = Convert.ToDateTime(timeRuleNode.Attributes["endTime"]?.InnerText),
                    Tick = TimeSpan.Parse(timeRuleNode.Attributes["tick"]?.InnerText)
                };

                timeRules.Add(timeRule);
            }


            return timeRules;
        }

        public IList<LotteryUpdateConfig> GetLotteryUpdateConfigs()
        {
            if (_lotteryUpdateConfigs.Count <= 0)
            {
                throw new Exception("loader配置文件失败");
            }
            return _lotteryUpdateConfigs;
        }
    }
}