using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Validation;
using LotteryService.Common;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;
using LotteryService.Domain.Interfaces.Service.Common;
using LotteryService.Domain.Services.Common;

namespace LotteryService.Domain.Services.Account
{
    public class AccountService :DapperService<User>, IAccountService
    {


        public AccountService(IUserDapperRepository dapperRepository) : base(dapperRepository)
        {
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(User entity)
        {         
            return base.Add(entity);
        }

        public ValidationResult Update(string id, params object[] fields)
        {
            throw new NotImplementedException();
        }

        public User GetUserByTokenId(string tokenId)
        {
            return ((IUserDapperRepository) _dapperRepository).GetUserByTokenId(tokenId);
        }

        public User GetUserByAccountName(string accountName)
        {
            return ((IUserDapperRepository)_dapperRepository).GetUserByAccountName(accountName);
        }

        public void Logout(string tokenId)
        {
            ((IUserDapperRepository)_dapperRepository).Logout(tokenId);
        }
    }
}