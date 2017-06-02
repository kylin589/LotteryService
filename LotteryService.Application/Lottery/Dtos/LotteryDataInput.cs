using System;

namespace LotteryService.Application.Lottery.Dtos
{
    public class LotteryDataInput
    {
        public string LotteryType { get; set; }

        public string Data { get; set; }

        public int Period { get; set; }

        public DateTime LotteryDateTime { get; set; }
    }
}