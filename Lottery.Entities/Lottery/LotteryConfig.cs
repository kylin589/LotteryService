using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lottery.Entities
{
    [Table("LotteryConfigs")]
    public class LotteryConfig
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string LotteryType { get; set; }

        [Required]
        public string ConfigData { get; set; }

        public DateTime CreateTime { get; set; }
    }
}