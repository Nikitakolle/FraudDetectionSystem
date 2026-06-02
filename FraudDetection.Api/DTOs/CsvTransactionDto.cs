namespace FraudDetection.Api.DTOs
{
    public class CsvTransactionDto
    {
        public float TransactionAmount { get; set; }

        public float MerchantRiskScore { get; set; }

        public float IPRiskScore { get; set; }

        public bool IsInternational { get; set; }

        public string CreditScoreBand { get; set; }

        public string KycLevel { get; set; }

        public string PaymentChannel { get; set; }

        public string DeviceType { get; set; }

        public float PostAuthRiskScore { get; set; }
    }
}