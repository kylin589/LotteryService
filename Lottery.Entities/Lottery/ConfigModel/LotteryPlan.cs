using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LotteryService.Common.Enums;

namespace Lottery.Entities
{
    public class LotteryPlan 
    {
        public int PlanId { get; set; }

        public string GroupId { get; set; }

        public string LotteryType { get; set; }

        public string Name { get; set; }

        public PlanType PlanType { get; set; }

        public DsType DsType { get; set; }

        public string LocationNumbers { get; set; }

        public int[] LocationNumberInts {
            get { return LocationNumbers.Split(',').Select(p => Convert.ToInt32(p)).ToArray(); }
        }

        public ForecastType ForecastType
        {
            get
            {
                if (LocationNumberInts.Length == 1)
                {
                    return ForecastType.Single;
                }
                return ForecastType.Multiple;
            }
        }

    }
}