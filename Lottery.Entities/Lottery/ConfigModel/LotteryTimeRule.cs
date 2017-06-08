using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Validation;

namespace Lottery.Entities
{
    public class LotteryTimeRule 
    {
        public string Weekdays { get; set; }
        public List<TimeRule> TimeRules { get; set; }

        //public int[] WeekdayArray
        //{
        //    get { return Weekdays.Split(',').Select(p => Convert.ToInt32(p)).ToArray(); }
        //}

    }
}