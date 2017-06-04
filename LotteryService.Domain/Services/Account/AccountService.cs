using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Validation;
using LotteryService.Common;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;
using LotteryService.Domain.Interfaces.Service.Common;
using LotteryService.Domain.Services.Common;

namespace LotteryService.Domain.Services.Account
{
    public class AccountService :DapperService<User>, IAccountService
    {


        public AccountService(IUserDapperRepository dapperRepository) : base(dapperRepository)
        {
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(User entity)
        {
            //if (!_validationResult.IsValid)
            //    return _validationResult;

            //var selfValidationEntity = entity as ISelfValidation;
            //if (selfValidationEntity != null && !selfValidationEntity.IsValid)
            //    return selfValidationEntity.ValidationResult;

            //if (!((IUserDapperRepository)_dapperRepository).Add(entity))
            //{
            //    _validationResult.Add(string.Format("添加实体对象{0}失败", entity.GetType().FullName));
            //}
            //var baseEntity = entity as BaseEntity;
            //if (baseEntity != null)
            //{
            //    _validationResult.SetData(LsConstant.IdKey, baseEntity.Id);
            //}

            //return _validationResult;
            return base.Add(entity);
        }

        public ValidationResult Update(string id, params object[] fields)
        {
            throw new NotImplementedException();
        }


    }
}