using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Account
{
    public class UserDapperRepository : DapperRepository, IUserDapperRepository
    {
        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public bool Add(User entity)
        {
            using (var cn = LotteryDbConnection)
            {
                string accountName = entity.UserName;
                if (string.IsNullOrEmpty(accountName))
                {
                    accountName = entity.Email;
                }
                if (string.IsNullOrEmpty(accountName))
                {
                    accountName = entity.Phone;
                }

                string querySqlStr = "SELECT * FROM [App].[User] WITH(NOLOCK) WHERE UserName = @AccountName OR Email = @AccountName OR Phone = @AccountName";

                var existUser = cn.Query<User>(querySqlStr, new
                {
                    AccountName = accountName
                }).SingleOrDefault();

                if (existUser != null)
                {
                    throw new Exception(string.Format("已经存在{0}的用户", accountName));
                }

                string insertSqlStr = "INSERT INTO [App].[User]" +
                                " ([Id],[Password],[Email],[Phone],[IsActive],[UserName],[UserRegistType],[CreatTime])" +
                                " VALUES(@Id, @Password, @Email, @Phone, @IsActive, @UserName, @UserRegistType, GETDATE())";
                return cn.Execute(insertSqlStr, new
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

        public User GetUserByAccountName(string accountName)
        {

            string querySqlStr = "SELECT * FROM [App].[User]  WHERE UserName = @AccountName OR Email = @AccountName OR Phone = @AccountName";

            using (var cn = LotteryDbConnection)
            {
                var existUser = cn.Query<User>(querySqlStr, new
                {
                    AccountName = accountName
                }).SingleOrDefault();
                return existUser;
            }
        }

        public void LoginFail(string userId, string accountName, LoginResultType loginResultResultType)
        {
            using (var cn = LotteryDbConnection)
            {
                // string sqlStr1 = "UPDATE [App].[User] SET [LastLoginTime] = GETDATE() ,[TokenId] = @TokenId WHERE Id = @UserId";
                string sqlStr = " INSERT INTO[App].[UserLoginAttempts]([Id],[UserId],[AccountName],[ClientIpAddress],[BrowserInfo],[LoginTime] )" +
                                 " VALUES(@Id,@UserId, @AccountName,@[ClientIpAddress],@[BrowserInfo], GETDATE())";
                cn.Execute(sqlStr, new
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    AccountName = accountName,
                    ClientIpAddress = IpHelper.GetClientIP(),
                    BrowserInfo = Utils.GetBrowserInfo(),

                });

            }
        }

        public void LoginSuccess(string userId, string accountName, LoginResultType loginResultResultType, string tokenId, DateTime loginTime)
        {
            using (var cn = LotteryDbConnection)
            {
                cn.Open();
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        string sqlStr1 = "UPDATE [App].[User] SET [LastLoginTime] = @DateTime ,[TokenId] = @TokenId WHERE Id = @UserId";
                        string sqlStr2 = " INSERT INTO[App].[UserLoginAttempts]([Id],[UserId],[AccountName],[ClientIpAddress],[BrowserInfo],[LoginTime],[LoginResult] )" +
                                         " VALUES(@Id,@UserId, @AccountName,@ClientIpAddress,@BrowserInfo,@DateTime,@LoginResult)";
                        cn.Execute(sqlStr1, new
                        {
                            UserId = userId,
                            TokenId = tokenId,
                            DateTime = loginTime

                        },transaction);

                        cn.Execute(sqlStr2, new
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = userId,
                            AccountName = accountName,
                            ClientIpAddress = IpHelper.GetClientIP(),
                            BrowserInfo = Utils.GetBrowserInfo(),
                            LoginResult = loginResultResultType,
                            DateTime = loginTime

                        }, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}