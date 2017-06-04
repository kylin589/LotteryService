using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dapper;
using Lottery.Entities;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Data.Repository.EntityFramework.Common;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Account
{
    public class UserDapperRepository : DapperRepository,IUserDapperRepository
    {
        public bool Add(User entity)
        {
            using (var cn = LotteryDbConnection)
            {
                string sqlStr = "INSERT INTO [Application].[User]" +
                                " ([Id],[Password],[Email],[Phone],[IsActive],[UserName],[UserRegistType],[CreatTime])" + 
		                        " VALUES(@Id, @Password, @Email, @Phone, @IsActive, @UserName, @UserRegistType, GETDATE())";
                return cn.Execute(sqlStr, new
                       {
                           entity.Id,
                           entity.UserName,
                           entity.Email,
                           entity.Phone,
                           entity.Password,
                           entity.UserRegistType,
                           entity.IsActive
                       }) > 0;
            }
        }

        public IEnumerable<User> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, object[] fields)
        {
            throw new NotImplementedException();
        }

        User IDapperRepository<User>.Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}