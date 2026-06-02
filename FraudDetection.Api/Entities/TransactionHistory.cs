namespace FraudDetection.Api.Entities
{
    public class TransactionHistory
    {
   
        public int Id { get; set; }
        public float TransactionAmount { get; set; }
        public float MerchantRiskScore { get; set; }
        public float IPRiskScore { get; set; }
        public bool IsInternational { get; set; }
        public float FraudProbability { get; set; }
        public bool IsFraud { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Recommendation { get; set; }
        public string Reason { get; set; }
    }
}