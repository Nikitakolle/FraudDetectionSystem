namespace FraudDetection.Api.DTOs
{
    public class FraudPredictionResponseDto
    {
        public bool IsFraud { get; set; }

        public float FraudProbability { get; set; }

        public string Message { get; set; }

        public string Recommendation { get; set; }

        public string Reason { get; set; }
    }
}