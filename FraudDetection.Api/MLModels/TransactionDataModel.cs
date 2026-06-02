using Microsoft.ML.Data;

namespace FraudDetection.Api.MLModels
{
    public class TransactionDataModel
    {
  
        public float AccountAgeDays { get; set; }

        public string CreditScoreBand { get; set; }

        public string KycLevel { get; set; }

        public float AvgMonthlySpend { get; set; }

        public float MerchantRiskScore { get; set; }

        public float TransactionAmount { get; set; }

        public string PaymentChannel { get; set; }

        public string DeviceType { get; set; }

        public bool IsInternational { get; set; }

        public float IPRiskScore { get; set; }

        public float TxnCount1h { get; set; }

        public float TxnCount24h { get; set; }

        public float FailedTxnCount24h { get; set; }

        public float GeoDistance { get; set; }

        public float AmountDeviation { get; set; }
        public float PostAuthRiskScore { get; set; }
    }
}