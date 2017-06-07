using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Validations;
using ValidationResult = Lottery.Entities.Extend.Validation.ValidationResult;

namespace Lottery.Entities
{
    [Table("Features")]
    public class Feature : BaseEntity, ISelfValidation
    {
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string LotteryType { get; set; }

        //public LotteryInfo LotteryInfo { get; set; }

        //public Norm Norm { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var fiscal = new FeatureIsValidValidation();
                ValidationResult = fiscal.Valid(this);
                return ValidationResult.IsValid;
            }

        }
    }
}