using FraudDetection.Api.DTOs;

namespace FraudDetection.Api.Services
{

    public interface IFraudPredictionService
    {
        Task<FraudPredictionResponseDto> Predict(TransactionRequestDto request, int userId);

        Task<List<TransactionHistoryDto>> GetHistory(int userId);
        Task<object> ProcessCsvFile(IFormFile file, int userId);
    }
}