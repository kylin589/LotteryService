using System;
using Lottery.Entities.Extend.Validation;
using LotteryService.Common.Enums;

namespace LotteryService.Application.Account.Dtos
{
    public class UserDto : IDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }


        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLoginTime { get; set; }


        public string TokenId { get; set; }

        public string QQ { get; set; }

        public string Wechat { get; set; }

        public string WechatOpenId { get; set; }


        public UserRegistType UserRegistType { get; set; }

        public DateTime? LastModificationTime { get; set; }


        public string LastModifierUserId { get; set; }

        public string AccountName
        {
            get
            {
                string accountName = String.Empty;
                switch (UserRegistType)
                {
                    case UserRegistType.Email:
                        accountName = Email;
                        break;
                    case UserRegistType.Phone:
                        accountName = Phone;
                        break;
                    case UserRegistType.UserName:
                        accountName = UserName;
                        break;
                }
                return accountName;
            }
        }

    }
}