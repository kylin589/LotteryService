using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual string Id { get; set; }

        public BaseEntity()
        {
           Id = Guid.NewGuid().ToString();
        }

    }
}