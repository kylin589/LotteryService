using System;

namespace Lottery.Entities
{
    public class TimeRule
    {

        public DateTime StartTime
        {
            get { return Convert.ToDateTime(StartTimeStr); }
        }

  
        public DateTime EndTime
        {
            get { return Convert.ToDateTime(EndTimeStr); }
        }


        public TimeSpan Tick
        {
            get
            {
                return TimeSpan.Parse(TickStr);
            }
        }

        public bool IsEffective
        {
            get { return DateTime.Now > StartTime && DateTime.Now <= EndTime.Add(Tick); }
        }

        public string StartTimeStr { get; set; }
        public string EndTimeStr { get; set; }
        public string TickStr { get; set; }
    }
}