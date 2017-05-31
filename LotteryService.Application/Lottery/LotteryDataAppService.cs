using System;
using System.Collections.Generic;
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
            var list = _lotteryService.Find(p => p.LotteryType == "bjpks",true);
            var data = new LotteryData()
            {
                Data = "2,3,1,8,4,5,9",
                LotteryDateTime = DateTime.Now,
                LotteryType = "test",
                Period = 1
            };
            WriteData(_lotteryService.Add, data);
           

            var lotteryDatas = new List<LotteryDataOutput>()
            {
                new LotteryDataOutput()
                {
                    Data = "1,2,4,5,6",
                    LotteryType = "Bjpks"
                },
                new LotteryDataOutput()
                {
                    Data = "8,2,4,5,6",
                    LotteryType = "Bjpks"
                }
            };
            return lotteryDatas;
        }
    }
}