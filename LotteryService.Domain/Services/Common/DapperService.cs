using System;
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
    public class DapperService<TEntity> : IDapperService<TEntity>
        where TEntity : class
    {
        #region Constructor

        private readonly IDapperRepository<TEntity> _dapperRepository;
        protected readonly ValidationResult _validationResult;

        public DapperService(
            IDapperRepository<TEntity> dapperRepository)
        {
            _dapperRepository = dapperRepository;
            _validationResult = new ValidationResult();
        }

        #endregion

        #region Properties

        protected IDapperRepository<TEntity> DapperRepository
        {
            get { return _dapperRepository; }
        }

        #endregion

        public virtual TEntity Get(string id)
        {
            return _dapperRepository.Get(id);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return _dapperRepository.All();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dapperRepository.Find(predicate);
        }

        public ValidationResult Add(TEntity entity)
        {
            if (!_validationResult.IsValid)
                return _validationResult;

            var selfValidationEntity = entity as ISelfValidation;
            if (selfValidationEntity != null && !selfValidationEntity.IsValid)
                return selfValidationEntity.ValidationResult;

            if (!_dapperRepository.Add(entity))
            {
                _validationResult.Add(string.Format("添加实体对象{0}失败",entity.GetType().FullName));
            }
            var baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                _validationResult.SetData(LsConstant.IdKey, baseEntity.Id);
            }

            return _validationResult;
        }

        public ValidationResult Update(string id, params object [] fields)
        {
            if (!_validationResult.IsValid)
                return _validationResult;

            if (!_dapperRepository.Update(id,fields))
            {
                _validationResult.Add(string.Format("更新实体对象{0}失败,主键Id为{1}", typeof(TEntity).FullName,id));
            }
            _validationResult.SetData(LsConstant.IdKey, id);

            return _validationResult;
        }
    }
}