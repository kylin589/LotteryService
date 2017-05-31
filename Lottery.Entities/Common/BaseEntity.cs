using System.ComponentModel.DataAnnotations;
using LotteryService.Common.Dependency;

namespace Lottery.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public virtual long Id { get; set; }
    }
}