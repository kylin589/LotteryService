using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Validations;
using ValidationResult = Lottery.Entities.Extend.Validation.ValidationResult;

namespace Lottery.Entities
{
    [Table("LotteryDatas",Schema = Constant.LotterySchema)]
    public class LotteryData : BaseEntity,ISelfValidation
    {
        public LotteryData()
        {
            InsertTime = DateTime.Now;
        }

        [Required]
        public int Period { get; set; }

        [Required]
        [MaxLength(30)]
        public string LotteryType { get; set; }

        [Required]
        public string Data { get; set; }

        public DateTime InsertTime { get; set; }

        public DateTime LotteryDateTime { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid {

            get
            {
                var fiscal = new LotteryDataIsValidValidation();
                ValidationResult = fiscal.Valid(this);
                return ValidationResult.IsValid;
            }
        }
    }
}