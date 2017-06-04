using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend;
using Lottery.Entities.Extend.Interfaces.Validation;
using LotteryService.Common.Enums;
using ValidationResult = Lottery.Entities.Extend.Validation.ValidationResult;

namespace Lottery.Entities
{
    [Table("User",Schema = Constant.ApplicationSchema)]
    public class User : AuditedEntity, ISelfValidation
    {
        public User()
        {
            IsActive = true;
        }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(14)]
        public string Phone { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [MaxLength(100)]
        public string SessionId { get; set; }

        [MaxLength(50)]
        public string QQ { get; set; }

        [MaxLength(50)]
        public string Wechat { get; set; }

        [MaxLength(100)]
        public string WechatOpenId { get; set; }

        [Required]
        public UserRegistType UserRegistType { get; set; }

        public DateTime? LastModificationTime { get; set; }
        
        [MaxLength(50)]
        public string LastModifierUserId { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; }

        public bool IsValid
        {
            get { return true; }

        }
    }
}