using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Data.Repository.Dapper.Log
{
    public class AuditLogDapperRepostory : DapperRepository, IReadOnlyRepository<AuditLog>
    {
        public AuditLog Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuditLog> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuditLog> Find(Expression<Func<AuditLog, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}