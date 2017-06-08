using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Validation;

namespace Lottery.Entities
{
    public class NumberInfo 
    {
        public int LocationNumber { get; set; }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public string LotteryType { get; set; }

        public string FeatureId { get; set; }


    }
}