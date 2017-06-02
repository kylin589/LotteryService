using System;

namespace LotteryService.Application.Lottery.Dtos
{
    public class LotteryDataOutput
    {
        public int Period { get; set; }

        public string LotteryType { get; set; }

        public string Data { get; set; }

        public DateTime LotteryDateTime { get; set; }

    }
}