using System;

namespace Lottery.DataUpdater.Models
{
    public class LotteryDatetime
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int Interval { get; set; }
    }
}