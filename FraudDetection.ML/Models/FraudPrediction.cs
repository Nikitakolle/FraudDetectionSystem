using Microsoft.ML.Data;

namespace FraudDetection.ML.Models
{
    public class FraudPrediction
    {
        
        [ColumnName("Label")]
        public bool ActualLabel { get; set; }

        [ColumnName("PredictedLabel")]
        public bool PredictedLabel { get; set; }
        
        [ColumnName("Probability")]
        public float FraudProbability { get; set; }

        [ColumnName("Score")]
        public float Score { get; set; }
    }
}