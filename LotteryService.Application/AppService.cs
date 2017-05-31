using System;
using System.Collections.Generic;
using Lottery.Entities.Common;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Validation;
using LotteryService.Application.Interfaces;
using LotteryService.Data.Context.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace LotteryService.Application
{
    public abstract class AppService<TContext> : ITransactionAppService<TContext>
         where TContext : IDbContext, new()
    {
        private IUnitOfWork<TContext> _uow;

        protected AppService()
        {
            ValidationResult = new ValidationResult();
        }

        protected ValidationResult ValidationResult { get; private set; }

        protected virtual void WriteData<TEntity>(Func<IList<TEntity>, ValidationResult> func, params TEntity[] entity)
            where TEntity :BaseEntity,ISelfValidation 
        {
            BeginTransaction();
            ValidationResult.Add(func(entity));
            if (ValidationResult.IsValid)
            {
                Commit();
            }
        }

        protected virtual void WriteData<TEntity>(Func<TEntity, ValidationResult> func,TEntity entity)
        where TEntity : BaseEntity, ISelfValidation
        {
            BeginTransaction();
            ValidationResult.Add(func(entity));
            if (ValidationResult.IsValid)
            {
                Commit();
            }
        }

        public virtual void BeginTransaction()
        {
            _uow = ServiceLocator.Current.GetInstance<IUnitOfWork<TContext>>();
            _uow.BeginTransaction();
            //var ioc = new IoC();
            //_uow = ioc.Container.Resolve<IUnitOfWork<TContext>>();
            //_uow.BeginTransaction();
        }

        public virtual void Commit()
        {
            _uow.SaveChanges();
        }
    }
}