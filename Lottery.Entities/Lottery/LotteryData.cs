using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Common;
using Lottery.Entities.Extend;
using Lottery.Entities.Extend.Interfaces.Validation;
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


        // [Unique]
        [Required]
        public long Period { get; set; }

        [Required]
        [MaxLength(30)]
        public string LotteryType { get; set; }

        [Required]
        public string Data { get; set; }

        public DateTime InsertTime { get; set; }

        public DateTime LotteryDateTime { get; set; }

        public ValidationResult ValidationResult { get; }

        public bool IsValid {
            get { return true; }
        }
    }
}