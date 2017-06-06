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

        public void LoginFail(string userId, string accountName, LoginResultType loginResultType)
        {
            // string sqlStr1 = "UPDATE [App].[User] SET [LastLoginTime] = GETDATE() ,[TokenId] = @TokenId WHERE Id = @UserId";
            string sqlStr = " INSERT INTO[App].[UserLoginAttempts]([Id],[UserId],[AccountName],[ClientIpAddress],[BrowserInfo],[LoginTime],[LoginResult],[IsOnline] )" +
                             " VALUES(@Id,@UserId, @AccountName,@ClientIpAddress,@BrowserInfo, GETDATE(),@LoginResult,@IsOnline)";
            using (var cn = LotteryDbConnection)
            {               
                cn.Execute(sqlStr, new
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    AccountName = accountName,
                    ClientIpAddress = IpHelper.GetClientIP(),
                    BrowserInfo = Utils.GetBrowserInfo(),
                    IsOnline = false,
                    LoginResult = loginResultType,
                });

            }
        }

        public void LoginSuccess(string userId, string accountName, LoginResultType loginResultResultType, string tokenId, DateTime loginTime)
        {
            string sqlStr1 = " UPDATE [App].[User] SET [LastLoginTime] = @DateTime ,[TokenId] = @TokenId WHERE Id = @UserId";
            string sqlStr2 =
                " UPDATE [App].[UserLoginAttempts] SET LogoutTime = GETDATE(),IsOnline = @Outline WHERE UserId = @UserId AND IsOnline = @Online";
            string sqlStr3 = " INSERT INTO[App].[UserLoginAttempts]([Id],[UserId],[TokenId],[AccountName],[ClientIpAddress],[BrowserInfo],[LoginTime],[LoginResult],[IsOnline] )" +
                             " VALUES(@Id,@UserId,@TokenId, @AccountName,@ClientIpAddress,@BrowserInfo,@DateTime,@LoginResult,@IsOnline)";

            using (var cn = LotteryDbConnection)
            {
                cn.Open();
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        cn.Execute(sqlStr1, new
                        {
                            UserId = userId,
                            TokenId = tokenId,
                            DateTime = loginTime

                        },transaction);

                        cn.Execute(sqlStr2, new
                        {
                            UserId = userId,
                            Outline = false,
                            Online = true

                        }, transaction);

                        cn.Execute(sqlStr3, new
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = userId,
                            TokenId = tokenId,
                            AccountName = accountName,
                            ClientIpAddress = IpHelper.GetClientIP(),
                            BrowserInfo = Utils.GetBrowserInfo(),
                            LoginResult = loginResultResultType,
                            DateTime = loginTime,
                            IsOnline = true,

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

        public User GetUserByTokenId(string tokenId)
        {
            string sqlStr = " SELECT * FROM App.[User] WHERE TokenId = @TokenId";
            using (var cn = LotteryDbConnection)
            {
                var loginUser = cn.Query<User>(sqlStr, new
                {
                    TokenId = tokenId
                }).SingleOrDefault();
                return loginUser;
            }
        }

        public void Logout(string tokenId)
        {
            string sqlStr1 = "UPDATE App.[User] SET TokenId = NULL WHERE TokenId = @TokenId";
            string sqlStr2 = "UPDATE [App].[UserLoginAttempts] SET LogoutTime = GETDATE(),IsOnline = @IsOnline WHERE TokenId=@TokenId ";

            using (var cn = LotteryDbConnection)
            {
                cn.Open();
                using (var transaction = cn.BeginTransaction())
                {
                    cn.Execute(sqlStr1, new
                    {
                        TokenId = tokenId,
                    }, transaction);

                    cn.Execute(sqlStr2, new
                    {
                        TokenId = tokenId,
                        IsOnline = false
                    }, transaction);
                    transaction.Commit();
                }
            }
        }
    }
}