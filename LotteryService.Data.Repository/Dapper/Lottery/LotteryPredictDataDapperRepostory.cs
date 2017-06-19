using Dapper;
using Lottery.Entities;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class LotteryPredictDataDapperRepostory : DapperRepository, ILotteryPredictDataDapperRepostory
    {
        public LotteryPredictData GetCurrentLotteryPredictData(string normId, int currentPredictPeriod)
        {
            string sqlStr = "SELECT * FROM [dbo].[LotteryPredictDatas] WHERE [NormId]=@NormId AND [CurrentPredictPeriod]=@CurrentPredictPeriod";
            using (var cn = LotteryDbConnection)
            {
                return cn.QuerySingleOrDefault<LotteryPredictData>(sqlStr, new
                {
                    NormId = normId,
                    CurrentPredictPeriod = currentPredictPeriod
                });
            }
        }

        public bool Update(LotteryPredictData lotteryPredictData)
        {
            string sqlStr =
                "UPDATE [dbo].[LotteryPredictDatas] SET " +
                "[CurrentPredictPeriod] = @CurrentPredictPeriod,[StartPeriod] = @StartPeriod,[EndPeriod] = @EndPeriod,[MinorCycle] = @MinorCycle,[PredictedResult] = @PredictedResult ,[ModifyTime] = GETDATE() " +
                "WHERE Id=@Id";
            using (var cn = LotteryDbConnection)
            {
                return cn.Execute(sqlStr, lotteryPredictData) > 0;
            }
        }

        public bool Insert(LotteryPredictData lotteryPredictData)
        {
            // todo: 计算正确率
            string sqlStr = "INSERT INTO [dbo].[LotteryPredictDatas] " +
"([Id],[NormId],[CurrentPredictPeriod],[StartPeriod],[EndPeriod],[MinorCycle],[PredictedNum],[PredictedResult],[CreatTime]) " +
"VALUES(@Id, @NormId, @CurrentPredictPeriod, @StartPeriod, @EndPeriod , @MinorCycle, @PredictedNum, @PredictedResult, GETDATE())";

            using (var cn = LotteryDbConnection)
            {
                return cn.Execute(sqlStr, lotteryPredictData) > 0;
            }
        }
    }
}