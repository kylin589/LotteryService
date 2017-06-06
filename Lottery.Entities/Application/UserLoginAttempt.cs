using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend;
using LotteryService.Common.Enums;

namespace Lottery.Entities
{
    [Table("UserLoginAttempts",Schema = Constant.ApplicationSchema)]
    public class UserLoginAttempt : BaseEntity
    {
        public UserLoginAttempt()
        {
            LoginTime = DateTime.Now;
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClientIpAddress { get; set; }

        [MaxLength(50)]
        public string BrowserInfo { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public LoginResultType LoginResult { get; set; }

        

    }
}