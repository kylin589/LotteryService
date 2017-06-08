using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Validation;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;

namespace Lottery.Entities
{
    public class NumberInfo 
    {
        //public int LocationNumber { get; set; }

        //public int MaxValue { get; set; }

        //public int MinValue { get; set; }

        //public string LotteryType { get; set; }

        //public LotteryType LotteryTypeE
        //{
        //    get { return Utils.StringConvertEnum<LotteryType>(LotteryType); }
        //}

        //public string FeatureId { get; set; }

        public string LotteryType { get; set; }
        public int KeyNumber { get; set; }
        public string Name { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }


    }
}