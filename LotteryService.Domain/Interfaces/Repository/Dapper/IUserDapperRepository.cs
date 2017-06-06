using System;
using System.Security.Cryptography.X509Certificates;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Domain.Interfaces.Repository.Dapper
{
    public interface IUserDapperRepository : IDapperRepository<User>
    {
        User GetUserByAccountName(string accountName);

        void LoginFail(string userId, string accountName, LoginResultType loginResultResultType);

        void LoginSuccess(string userId, string accountName, LoginResultType loginResultResultType, string tokenId,DateTime dateTime);

        User GetUserByTokenId(string tokenId);
        void Logout(string tokenId);
    }
}