using System.ComponentModel.DataAnnotations;

namespace FraudDetection.Api.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
      
    }
}