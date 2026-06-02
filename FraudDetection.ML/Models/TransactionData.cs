using Microsoft.ML.Data;

namespace FraudDetection.ML.Models
{
    public class TransactionData
    {
        // Numeric
        [LoadColumn(4)] public float AccountAgeDays { get; set; }
        [LoadColumn(7)] public float AvgMonthlySpend { get; set; }
        [LoadColumn(8)] public float MerchantRiskScore { get; set; }
        [LoadColumn(9)] public float TransactionAmount { get; set; }
        [LoadColumn(13)] public float IPRiskScore { get; set; }
        [LoadColumn(14)] public float TxnCount1h { get; set; }
        [LoadColumn(15)] public float TxnCount24h { get; set; }
        [LoadColumn(16)] public float FailedTxnCount24h { get; set; }
        [LoadColumn(17)] public float GeoDistance { get; set; }
        [LoadColumn(18)] public float AmountDeviation { get; set; }
        [LoadColumn(20)] public float PostAuthRiskScore { get; set; }

        // Categorical
        [LoadColumn(5)] public string CreditScoreBand { get; set; }
        [LoadColumn(6)] public string KycLevel { get; set; }
        [LoadColumn(10)] public string PaymentChannel { get; set; }
        [LoadColumn(11)] public string DeviceType { get; set; }

        // Boolean
        [LoadColumn(12)] public bool IsInternational { get; set; }

        // Label
        [LoadColumn(19)]
        public bool Label { get; set; }
    }
}