using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class LotteryAnalyseNormDapperRepostory : DapperRepository, ILotteryAnalyseNormDapperRepostory
    {
        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll()
        {
            IDictionary<LotteryType,IList<LotteryAnalyseNorm>> _dictionary = new Dictionary<LotteryType, IList<LotteryAnalyseNorm>>();
            using (var cn = LotteryDbConnection)
            {
                string sqlStr1 = "SELECT LotteryType FROM dbo.LotteryAnalyseNorms GROUP BY LotteryType";
                string sqlStr2 = "SELECT * FROM dbo.LotteryAnalyseNorms WHERE LotteryType = @LotteryType";
                var lotteryTypes = cn.Query<string>(sqlStr1);

                foreach (var lotteryType in lotteryTypes)
                {
                    var lotteryAnalyseNorms = cn.Query<LotteryAnalyseNorm>(sqlStr2, new
                    {
                        LotteryType = lotteryType
                    }).ToList();
                    _dictionary.Add(Utils.StringConvertEnum<LotteryType>(lotteryType),lotteryAnalyseNorms);
                }
            }
            return _dictionary;
        }
    }
}