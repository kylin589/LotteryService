using System;

namespace Lottery.DataUpdater.Models
{
    public class TimeRule
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan Tick { get; set; }

        public bool IsEffective
        {
            get { return DateTime.Now > StartTime && DateTime.Now <= EndTime.Add(Tick); }
        }
    }
}