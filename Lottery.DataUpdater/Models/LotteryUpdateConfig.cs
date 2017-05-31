using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.DataUpdater.Models
{
    public class LotteryUpdateConfig
    {
        private TimeRule _currentEffectTimeRule = null;

        public string Name { get; set; }

        public string Cname { get; set; }

        public int Interval { get; set; }

        public int[] Weekdays { get; set; }

        public IList<TimeRule> TimeRules { get; set; }

        public IList<DataSite> DataSites { get; set; }

        public TimeRule CurrentEffectTimeRule
        {
            get
            {
                if (IsNeedUpdateItem)
                {
                    return _currentEffectTimeRule;
                }
                throw new Exception("当前时间节点不需要更新数据");
            }
        }

        public DateTime NextLotteryTime
        {
            get
            {
                if (IsNeedUpdateItem)
                {
                    var totalCurrentLotteryCount = (int)Math.Ceiling((decimal)(DateTime.Now - CurrentEffectTimeRule.StartTime).Ticks / CurrentEffectTimeRule.Tick.Ticks);
                    return CurrentEffectTimeRule.StartTime.Add(new TimeSpan(CurrentEffectTimeRule.Tick.Ticks * totalCurrentLotteryCount));
                }
                //LogHelper.Logger.Error("当前时间节点不需要更新彩票开奖数据");
                var nextLotteryTimeRule = TimeRules.OrderBy(p => p.StartTime).First();
                return nextLotteryTimeRule.StartTime;
            }


        }

        public bool IsNeedUpdateItem
        {
            get
            {
                if (TimeRules == null || TimeRules.Count <= 0)
                {
                    //LogHelper.Logger.Debug("没有设置彩票数据更新时间规则");
                    return false;
                }
                var currentWeek = Convert.ToInt32(DateTime.Now.DayOfWeek);
                if (!Weekdays.Contains(currentWeek))
                {
                    //LogHelper.Logger.Debug("根据彩票配置规范，该天不需要更新数据");
                    return false;
                }
                _currentEffectTimeRule = TimeRules.FirstOrDefault(p => p.IsEffective);
                if (_currentEffectTimeRule == null)
                {

                    return false;
                }
                return true;
            }
        }
    }
}