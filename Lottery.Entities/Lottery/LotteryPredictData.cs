using LotteryService.Common.Enums;

namespace Lottery.Entities
{
    public class LotteryPredictData : AuditedEntity
    {

        public string NormId { get; set; }

        public int CurrentPredictPeriod { get; set; }

        public int StartPeriod { get; set; }

        public int EndPeriod { get; set; }

        public int MinorCycle { get; set; }

        public string PredictedNum { get; set; }

        public PredictionResult PredictedResult { get; set; }

    }
}