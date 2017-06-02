using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend.Interfaces.Validation;
using ValidationResult = Lottery.Entities.Extend.Validation.ValidationResult;

namespace Lottery.Entities
{
    public class BaseEntity
    {
        [Key]
        public virtual string Id { get; set; }

        public BaseEntity()
        {
           Id = Guid.NewGuid().ToString();
        }

    }
}