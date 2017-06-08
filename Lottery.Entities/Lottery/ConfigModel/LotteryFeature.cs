using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using Lottery.Entities.Extend;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Validations;
using ValidationResult = Lottery.Entities.Extend.Validation.ValidationResult;

namespace Lottery.Entities
{
    public class LotteryFeature
    {
        public string LotteryType { get; set; }

        public string CName { get; set; }


        public ICollection<NumberInfo> NumberInfos { get; set; }

        public LotteryTimeRule LotteryTimeRule { get; set; }

        public LotteryNorm LotteryNorm { get; set; }

    }
}