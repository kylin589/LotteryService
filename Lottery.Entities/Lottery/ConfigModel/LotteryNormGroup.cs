using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lottery.Entities
{
    public class NormGroup  
    {
        public string NormId { get; set; }

        public LotteryNorm LotteryNorm { get; set; }

        public string Name { get; set; }

        public string Cname { get; set; }

        public ICollection<LotteryPlan> Plans { get; set; }
    }
}