using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend.Interfaces.Validation;
using ValidationResult = Lottery.Entities.Extend.Validation.ValidationResult;

namespace Lottery.Entities
{
    public class UserBasicNorm : AuditedEntity, ISelfValidation
    {
        public UserBasicNorm()
        {
            IsDefault = false;
            Enable = true;
            LatestStartPeriod = 0;
        }

        [MaxLength(50)]
        public string UserId { get; set; }

        [Required]
        public int PlanCycle { get; set; }

        public int? LatestStartPeriod { get; set; }

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

        [Required]
        public bool Enable { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; }

        [NotMapped]
        public bool IsValid {
            get { return true; } 
        }
    }
}