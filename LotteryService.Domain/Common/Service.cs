﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Validation;
using LotteryService.Common;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Service.Common;

namespace LotteryService.Domain.Services.Common
{
    public class Service<TEntity> : IService<TEntity>
      where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IDapperRepository<TEntity> _dapperRepository;
        protected readonly ValidationResult _validationResult;

        public Service(IRepository<TEntity> repository,
            IDapperRepository<TEntity> dapperRepository)
        {
            _repository = repository;
            _dapperRepository = dapperRepository;
            _validationResult = new ValidationResult();
        }


        #region Read Methods

        public virtual TEntity Get(string id, bool @readonly = false)
        {
            return @readonly
                ? _dapperRepository.Get(id)
                : _repository.Get(id);
        }

        public virtual IEnumerable<TEntity> All(bool @readonly = false)
        {
            return _repository.All(@readonly);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return _repository.Find(predicate, @readonly);
        }

        #endregion

        #region CRUD Methods

        public virtual ValidationResult Add(TEntity entity)
        {
            if (!_validationResult.IsValid)
                return _validationResult;

            var selfValidationEntity = entity as ISelfValidation;
            if (selfValidationEntity != null && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;


            _repository.Add(entity);
            var baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                _validationResult.SetData(LsConstant.IdKey, baseEntity.Id);
            }
            
            return _validationResult;
        }

        public virtual ValidationResult Update(TEntity entity)
        {
            if (!_validationResult.IsValid)
                return _validationResult;

            var selfValidationEntity = entity as ISelfValidation;
            if (selfValidationEntity != null && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            _repository.Update(entity);
            //_validationResult.SetData(LsConstant.EntityKey, entity);
            var baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                _validationResult.SetData(LsConstant.IdKey, baseEntity.Id);
            }
            return _validationResult;
        }

        public virtual ValidationResult Delete(TEntity entity)
        {
            if (!_validationResult.IsValid)
                return _validationResult;

            _repository.Delete(entity);
            var baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                _validationResult.SetData(LsConstant.IdKey, baseEntity.Id);
            }
            return _validationResult;
        }

        #endregion
    }
}