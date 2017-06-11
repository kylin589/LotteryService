using System;
using System.Collections.Generic;
using AutoMapper;
using Lottery.Entities;
using Lottery.Entities.Extend.Validation;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Service;
using LotteryService.Application;
using LotteryService.Common;
using LotteryService.Common.Extensions;
using LotteryService.Common.Tools;

namespace LotteryService.Application.Lottery
{
    public class LotteryAnalyseNormAppService : ILotteryAnalyseNormAppService
    {
        private ILotteryAnalyseNormService _analyseNormService;

        public LotteryAnalyseNormAppService(ILotteryAnalyseNormService analyseNormService)
        {
            _analyseNormService = analyseNormService;
        }

        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll()
        {
            return _analyseNormService.GetAll();
        }

        public UserBasicNormDto GetUserBasicNorm(string userId, LotteryType lotteryType)
        {
            var userBasicNorm = _analyseNormService.GetUserBasicNorm(userId, lotteryType);
            return Mapper.Map(userBasicNorm, new UserBasicNormDto());
        }

        public ResultMessage<UserBasicNormDto> SetUserBasicNorm(string userId, UserBasicNormInput input)
        {
            ResultMessage<UserBasicNormDto> result = new ResultMessage<UserBasicNormDto>();

           var userBasicNorm = Mapper.Map(input, new UserBasicNorm());
            userBasicNorm.UserId = userId;
            userBasicNorm.Modulus = AppUtils.GenerateModules(input.ForecastCount);
            // Todo:判断用户输入的用户指标是否正确
            if (userBasicNorm.IsValid)
            {
                try
                {
                    if (_analyseNormService.SetUserBasicNorm(userBasicNorm))
                    {
                        result.Code = ResultCode.Success;
                        result.Msg = "设置用户基础指标成功";
                    }
                    else
                    {
                        result.Code = ResultCode.Fail;
                        result.Msg = "设置用户基础指标失败,稍后重试";
                    }
                }
                catch (Exception ex)
                {

                    result.Code = ResultCode.ServiceError;
                    result.Msg = "设置用户基础指标失败,原因:" + ex.Message;
                }
                
            }
            else
            {
                result.Code = ResultCode.VerifyInputError;
                result.Msg = "设置用户基础指标失败,基础指标参数有误:" + userBasicNorm.ValidationResult.Errors.ToSplitString();
            }
            result.Data = GetUserBasicNorm(userId, Utils.StringConvertEnum<LotteryType>(userBasicNorm.LotteryType));

            return result;
        }
    }
}