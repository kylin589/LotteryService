using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lottery.Entities;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Data.Context;
using LotteryService.Domain.Interfaces.Service;
using LotteryService.Domain.Interfaces.Service.Common;
using LotteryService.Domain.Logs;

namespace LotteryService.Application.Lottery
{
    public class LotteryDataAppService : AppService<LotteryDbContext>, ILotteryDataAppService
    {
        private ILotteryDataService _lotteryService;

        private IDapperService<LotteryData> _lotteryDapperService;

        public LotteryDataAppService(ILotteryDataService lotteryService, 
            IDapperService<LotteryData> lotteryDapperService)
        {
            _lotteryService = lotteryService;
            _lotteryDapperService = lotteryDapperService;
        }

        public IList<LotteryDataOutput> GetLotteryData()
        {
            var lotteryDatas = _lotteryDapperService.All();
            return Mapper.Map(lotteryDatas, new List<LotteryDataOutput>());

        }

 
        public LotteryData Insert(LotteryData newData)
        {
            var lotteryDataId = _lotteryDapperService.Add(newData);
            return _lotteryDapperService.Get(lotteryDataId.GetData<string>(LsConstant.IdKey));
        }

        public bool ExsitData(string lotteryType, int period)
        {
            return _lotteryService.ExsitData(lotteryType, period);
        }

        public LotteryData GetLatestLotteryData(string lotteryType)
        {
            return _lotteryService.GetLatestLotteryData(lotteryType);
        }

        public bool GetLotteryData(string lotteryType, int? peroiod, out LotteryDataOutput lotteryDataOutput)
        {
            var lotteryData = _lotteryService.GetLotteryData(lotteryType, peroiod);
            if (lotteryData == null)
            {
                lotteryDataOutput = null;
                return false;
            }
            lotteryDataOutput = Mapper.Map(lotteryData, new LotteryDataOutput());
            return true;
        }
    }
}