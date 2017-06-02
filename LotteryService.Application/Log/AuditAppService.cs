using System;
using Lottery.Entities;
using LotteryService.Application.Log.Dtos;
using LotteryService.Common;
using LotteryService.Data.Context;
using LotteryService.Domain.Interfaces.Service.Common;
using LotteryService.Common.Excetions;
using LotteryService.Data.Context.Interfaces;
using LotteryService.Domain.Interfaces.Service;
using Microsoft.Practices.ServiceLocation;

namespace LotteryService.Application.Log
{
    public class AuditAppService : AppService<LotteryDbContext>, IAuditAppService
    {
        private IService<AuditLog> _auditLogService;

        public AuditAppService(IService<AuditLog> auditLogService)
        {
            _auditLogService = auditLogService;
            ValidationResult.SetData(LsConstant.AuditLogKey,true);
        }

        public string InsertAuditLog(AuditLogInput input)
        {
            var auditLog = new AuditLog()
            {
                UserId = input.UserId,
                BrowserInfo = input.BrowserInfo,
                ClientIpAddress = input.ClientIpAddress,
                ClientName = input.ClientName,              
                MethodName = input.MethodName,
                ApiAddress = input.RequestAddress,           
                ExecutionDuration = 0,
                Parameters = input.Parameters,
                IsExecSuccess = input.IsExecSuccess,
                ActionName = input.ActionName,
                ControllerName = input.ControllerName,
            };

             WriteData(_auditLogService.Add, auditLog);

            return ValidationResult.GetData<string>(LsConstant.IdKey);
      
        }

        public void UpdateAuditLog(AuditLogEdit input)
        {
            var auditLog = _auditLogService.Get(input.Id);
            auditLog.Exception = input.Exception;
            auditLog.ExecutionDuration = input.ExecutionDuration;
            auditLog.IsExecSuccess = input.IsExecSuccess;
            WriteData(_auditLogService.Update,auditLog);
            if (!ValidationResult.IsValid)
            {
                throw new LSException("更新Api执行时间失败");
            }
        }
    }
}