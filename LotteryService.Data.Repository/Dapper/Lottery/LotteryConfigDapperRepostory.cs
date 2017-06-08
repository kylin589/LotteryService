using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Lottery.Entities;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class LotteryConfigDapperRepostory : DapperRepository, ILotteryConfigDapperRepostory
    {

        public string GetLotteryConfig(string lotteryType)
        {
            using (var cn = LotteryDbConnection)
            {
                string sqlStr = "SELECT ConfigData FROM [dbo].[LotteryConfigs] WHERE LotteryType = @LotteryType";

                var lotteryConfigData = cn.QuerySingleOrDefault<string>(sqlStr, new { LotteryType = lotteryType });
                return lotteryConfigData;
            }
        }

        public IDictionary<string, string> GetLotteryConfigs()
        {
            using (var cn = LotteryDbConnection)
            {
                string sqlStr = "SELECT * FROM [dbo].[LotteryConfigs]";

                var lotteryConfigDatas = cn.Query<LotteryConfig>(sqlStr);
                var lotteryConfigDic = new Dictionary<string, string>();
                foreach (var item in lotteryConfigDatas)
                {
                    lotteryConfigDic.Add(item.LotteryType,item.ConfigData);
                }
                return lotteryConfigDic;
            }
        }
    }
}