using System;
using System.Collections.Generic;
using LitJson;
using Lottery.Entities;
using Lottery.Entities.Extend.Validation;
using LotteryService.Application.Interfaces;
using LotteryService.Common;
using LotteryService.Common.Excetions;
using LotteryService.Common.Extensions;
using LotteryService.Data.Context;
using LotteryService.Data.Context.Interfaces;
using LotteryService.Domain.Logs;
using Microsoft.Practices.ServiceLocation;

namespace LotteryService.Application
{
    public class AppService<TContext> : ITransactionAppService<TContext>
         where TContext : IDbContext, new()
    {
        protected IUnitOfWork<TContext> _uow;

        protected AppService()
        {
            ValidationResult = new ValidationResult();
        }

        protected ValidationResult ValidationResult { get; private set; }

        protected virtual void WriteData<TEntity>(Func<IList<TEntity>, ValidationResult> func, params TEntity[] entity)
            where TEntity :BaseEntity 
        {
            BeginTransaction();
            ValidationResult.Add(func(entity));
            if (ValidationResult.IsValid)
            {
                if (!Commit())
                {
                    throw new LSException("数据保存失败.Data:" + entity.ToJsonString() + ",Action:" + GetType().FullName + "=>" + func.Method.Name);
                }
                if (!ValidationResult.IsSet(LsConstant.AuditLogKey) || !ValidationResult.GetData<bool>(LsConstant.AuditLogKey))
                {
                    LogDbHelper.LogInfo("数据保存成功.Data:" + entity.ToJsonString(), GetType().FullName + "=>" + func.Method.Name);
                }
            }
            else
            {
                LogDbHelper.LogInfo("数据保存失败,原因:" + ValidationResult.Errors.ToJsonString(), GetType().FullName + "=>" + func.Method.Name);
            }

        }

        protected virtual void WriteData<TEntity>(Func<TEntity, ValidationResult> func,TEntity entity)
        where TEntity : BaseEntity
        { 
            BeginTransaction();
            ValidationResult.Add(func(entity));
            if (ValidationResult.IsValid)
            {
                if (!Commit())
                {
                   throw new LSException("数据保存失败,Data："+ entity.ToJsonString() + ",Action:" + GetType().FullName + "=>" + func.Method.Name);
                }
                if (!ValidationResult.IsSet(LsConstant.AuditLogKey) || !ValidationResult.GetData<bool>(LsConstant.AuditLogKey))
                {
                    LogDbHelper.LogInfo("数据保存成功.Data:" + entity.ToJsonString(), GetType().FullName + "=>" + func.Method.Name);
                }
            }
            else
            {
                LogDbHelper.LogInfo("数据保存失败,原因:" + ValidationResult.Errors.ToJsonString(), GetType().FullName + "=>" + func.Method.Name);
            }
        }

        public virtual void BeginTransaction()
        {
             _uow = ServiceLocator.Current.GetInstance<IUnitOfWork<TContext>>();                     
            _uow.BeginTransaction();
        }

        public virtual bool Commit()
        {
            return _uow.SaveChanges();
        }
    }
}