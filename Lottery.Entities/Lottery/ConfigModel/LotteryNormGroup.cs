using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lottery.Entities
{
    public class NormGroup  
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Cname { get; set; }

        public List<LotteryPlan> Plans { get; set; }
    }
}