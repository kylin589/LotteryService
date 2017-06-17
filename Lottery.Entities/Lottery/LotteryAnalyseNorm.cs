using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class LotteryAnalyseNorm : AuditedEntity
    {
        public LotteryAnalyseNorm()
        {
            IsDefault = false;
            Enable = true;
        }

        [Required]
        public int PlanId { get; set; }

        [Required]
        public int PlanCycle { get; set; }

        [Required]
        public int LastStartPeriod { get; set; }

        [Required]
        public int ForecastCount { get; set; }

        [Required]
        public int BasicHistoryCount { get; set; }

        [Required]
        public int UnitHistoryCount { get; set; }

        [Required]
        public int HotWeight { get; set; }

        [Required]
        public int SizeWeight { get; set; }

        [Required]
        public int ThreeRegionWeight { get; set; }

        [Required]
        public int MissingValueWeight { get; set; }

        [Required]
        public int OddEvenWeight { get; set; }

        [MaxLength(50)]
        public string Modulus { get; set; }

        [MaxLength(20)]
        [Required]
        public string LotteryType { get; set; }

        public double? CurrentAccuracy { get; set; }

        [Required]
        public bool Enable { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [MaxLength(50)]
        public string CreateUserId { get; set; }
    }
}