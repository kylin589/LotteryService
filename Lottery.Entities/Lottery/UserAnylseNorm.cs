using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lottery.Entities
{
    public class UserAnylseNorm : AuditedEntity
    {
        
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserInfo { get; set; }

        public string LotteryAnalyseNormId { get; set; }

        [ForeignKey("LotteryAnalyseNormId")]
        public LotteryAnalyseNorm LotteryAnalyseNorm { get; set; }

        [Required]
        public int PlanId { get; set; }

        public string LotteryType { get; set; }
    }
}