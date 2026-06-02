using Microsoft.ML.Data;

namespace FraudDetection.Api.MLModels
{
    public class FraudPredictionModel
    {

        [ColumnName("PredictedLabel")]
        public bool IsFraud { get; set; }

        [ColumnName("Probability")]
        public float FraudProbability { get; set; }

        [ColumnName("Score")]
        public float Score { get; set; }
    }
}