using System.ComponentModel.DataAnnotations;
namespace FraudDetection.Api.DTOs
{
    public class TransactionRequestDto
    {
       
            [Required]

            [Range(1, 100000)]
            public float TransactionAmount { get; set; }

            [Required]

            [Range(0, 1)]
            public float MerchantRiskScore { get; set; }

            [Required]

            [Range(0, 1)]
            public float IPRiskScore { get; set; }

            [Required]
            public bool IsInternational { get; set; }

            [Required]
            public string CreditScoreBand { get; set; }

            [Required]
            public string KycLevel { get; set; }

            [Required]
            public string PaymentChannel { get; set; }

            [Required]
            public string DeviceType { get; set; }

            [Required]

            [Range(0, 1)]
            public float PostAuthRiskScore { get; set; }
        
    }
}