namespace FraudDetection.Api.DTOs
{

    public class TransactionHistoryDto
    {
        public int Id { get; set; }

        public float TransactionAmount { get; set; }

        public float MerchantRiskScore { get; set; }

        public float IPRiskScore { get; set; }

        public bool IsInternational { get; set; }

        public float FraudProbability { get; set; }

        public bool IsFraud { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Recommendation { get; set; }

        public string Reason { get; set; }

    }
}
