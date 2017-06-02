using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Entities;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Data.Context;
using LotteryService.Domain.Interfaces.Service.Common;

namespace LotteryService.Application.Lottery
{
    public class LotteryDataAppService : AppService<LotteryDbContext>, ILotteryDataAppService
    {
        private IService<LotteryData> _lotteryService;

        private IReadOnlyService<LotteryData> _lotteryReadOnlyService;

        public LotteryDataAppService(IService<LotteryData> lotteryService, 
            IReadOnlyService<LotteryData> lotteryReadOnlyService)
        {
            _lotteryService = lotteryService;
            _lotteryReadOnlyService = lotteryReadOnlyService;
        }


        public IList<LotteryDataOutput> GetLotteryData()
        {
            var lotteryDatas = _lotteryService.All();
            return lotteryDatas.Select(p =>
            {
                return new LotteryDataOutput()
                {
                    Data = p.Data,
                    LotteryType = p.LotteryType
                };
            }).ToList();
        }

        public void Add(LotteryDataInput input)
        {
            WriteData(_lotteryService.Add, new LotteryData
            {
                LotteryType = input.LotteryType,
                Data = input.Data,
                LotteryDateTime = input.LotteryDateTime,
                Period = input.Period,
            });
        }
    }
}