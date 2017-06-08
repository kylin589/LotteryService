using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;

namespace Lottery.Entities
{
    public class LotteryPlan 
    {
        //public int PlanId { get; set; }

        //public string Name { get; set; }

        //public string Type { get; set; }

        //public PlanType PlanType {
        //    get { return Utils.StringConvertEnum<PlanType>(Type); }
        //}

        //public DsType DsType { get; set; }

        //public ICollection<int> KeyNumber { get; set; }

        //public int? GroupId { get; set; }

        public int PlanId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<int> KeyNumber { get; set; }
        public string DsType { get; set; }
        public int? GroupId { get; set; }
        public int? SerialNumber { get; set; }

        public ForecastType ForecastType
        {
            get
            {
                if (KeyNumber.Count == 1)
                {
                    return ForecastType.Single;
                }
                return ForecastType.Multiple;
            }
        }

    }
}