﻿using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;
using LotteryService.Domain.Account.Models;

namespace LotteryService.Domain.Interfaces.Service
{
    public interface ILoginManager : ITransientDependency
    {
        LoginResult Login(string accountName, string password);

        void Logout(string loginUserTokenId);
    }
}