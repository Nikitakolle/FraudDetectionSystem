using FraudDetection.Api.DTOs;
using FraudDetection.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FraudDetection.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FraudDetectionController : ControllerBase
    {
        private readonly IFraudPredictionService _predictionService;

        public FraudDetectionController(
            IFraudPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        

        [HttpPost("predict")]
        public async Task<IActionResult> Predict(
    [FromBody] TransactionRequestDto request)
        {
            var userId = int.Parse(
                User.FindFirst(
                         ClaimTypes.NameIdentifier)!
                .Value
            );
            var result = await _predictionService.Predict(request, userId);

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var userId = int.Parse(

              User.FindFirst(
               ClaimTypes.NameIdentifier)!
              .Value
            );
            var history = await _predictionService.GetHistory(userId);

            return Ok(history);
        }

        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            var userId = int.Parse(

       User.FindFirst(
           ClaimTypes.NameIdentifier)!
       .Value
   );
            var result =
                await _predictionService
                    .ProcessCsvFile(file, userId);

            return Ok(result);
        }
    }
}