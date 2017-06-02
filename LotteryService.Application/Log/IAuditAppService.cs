using LotteryService.Application.Interfaces;
using LotteryService.Application.Log.Dtos;
using LotteryService.Common.Dependency;
using LotteryService.Data.Context;

namespace LotteryService.Application.Log
{
    public interface IAuditAppService : ITransientDependency
    {
        string InsertAuditLog(AuditLogInput input);

        void UpdateAuditLog(AuditLogEdit input);
    }
}