using FraudDetection.Api.DTOs;
using FraudDetection.Api.MLModels;
using Microsoft.ML;
using FraudDetection.Api.Data;
using FraudDetection.Api.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CsvHelper;


namespace FraudDetection.Api.Services
{
    public class FraudPredictionService : IFraudPredictionService
    {
        private readonly PredictionEngine<TransactionDataModel, FraudPredictionModel> _predictionEngine;
        private readonly ApplicationDbContext _context;

        public FraudPredictionService(ApplicationDbContext context)
        {
            _context = context;
            var mlContext = new MLContext();

            string modelPath = Path.Combine(
            Environment.CurrentDirectory,
            "MLModels",
            "fraud_model_lightgbm.zip");

            IDataView emptyDataView = mlContext.Data.LoadFromEnumerable(
                new List<TransactionDataModel>());

            ITransformer trainedModel = mlContext.Model.Load(
                modelPath,
                out var modelInputSchema);

            _predictionEngine = mlContext.Model.CreatePredictionEngine
                <TransactionDataModel, FraudPredictionModel>(trainedModel);
        }

        public async Task<FraudPredictionResponseDto> Predict(TransactionRequestDto request, int userId)
        {
            var transactionAmount = request.TransactionAmount;

            var geoDistance =
                request.IsInternational
                    ? Random.Shared.Next(200, 2000)
                    : Random.Shared.Next(1, 100);

            var amountDeviation =
                transactionAmount > 5000
                    ? Random.Shared.Next(5, 15)
                    : Random.Shared.Next(1, 5);

            var failedTxnCount24h =
                request.PostAuthRiskScore > 0.7
                    ? Random.Shared.Next(2, 6)
                    : Random.Shared.Next(0, 2);

            var txnCount1h =
                transactionAmount > 8000
                    ? Random.Shared.Next(5, 15)
                    : Random.Shared.Next(1, 5);

            var txnCount24h =
                txnCount1h + Random.Shared.Next(5, 20);

            bool isHighValueCustomer =
            request.CreditScoreBand == "Excellent";

            int accountAgeDays =
                isHighValueCustomer
                    ? Random.Shared.Next(1000, 3000)
                    : Random.Shared.Next(30, 1000);

            float avgMonthlySpend =
                isHighValueCustomer
                    ? Random.Shared.Next(5000, 20000)
                    : Random.Shared.Next(500, 5000);

            var input = new TransactionDataModel
            {
                AccountAgeDays = accountAgeDays,
                CreditScoreBand = request.CreditScoreBand,
                KycLevel = request.KycLevel,
                AvgMonthlySpend = avgMonthlySpend,
                MerchantRiskScore = request.MerchantRiskScore,
                TransactionAmount = request.TransactionAmount,
                PaymentChannel = request.PaymentChannel,
                DeviceType = request.DeviceType,
                IsInternational = request.IsInternational,
                IPRiskScore = request.IPRiskScore,
                TxnCount1h = txnCount1h,
                TxnCount24h = txnCount24h,
                FailedTxnCount24h = failedTxnCount24h,
                GeoDistance = geoDistance,
                AmountDeviation = amountDeviation,
                PostAuthRiskScore = request.PostAuthRiskScore
            };

            var prediction = _predictionEngine.Predict(input);

           

            string recommendation;

            if (prediction.FraudProbability >= 0.90)
            {
                recommendation = "Block Transaction";
            }
            else if (prediction.FraudProbability >= 0.70)
            {
                recommendation = "Manual Review";
            }
            else
            {
                recommendation = "Approve Transaction";
            }

            var reasons = new List<string>();
            if (request.TransactionAmount > 5000)
            {
                reasons.Add("High transaction amount");
            }

            if (request.IsInternational)
            {
                reasons.Add("International transaction");
            }

            if (request.MerchantRiskScore > 0.7)
            {
                reasons.Add("High merchant risk score");
            }

            if (request.IPRiskScore > 0.7)
            {
                reasons.Add("High IP risk score");
            }
            var reason = reasons.Any()
        ? string.Join(", ", reasons)
        : "No significant risk factors detected";

            var transactionHistory = new TransactionHistory
            {
                TransactionAmount = request.TransactionAmount,
                MerchantRiskScore = request.MerchantRiskScore,
                IPRiskScore = request.IPRiskScore,
                IsInternational = request.IsInternational,
                FraudProbability = prediction.FraudProbability,
                IsFraud = prediction.IsFraud,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                Recommendation = recommendation,
                Reason = reason
            };

            _context.TransactionHistories.Add(transactionHistory);

            await _context.SaveChangesAsync();

            return new FraudPredictionResponseDto
            {
                IsFraud = prediction.IsFraud,

                FraudProbability = prediction.FraudProbability,

                Message = prediction.IsFraud ? "High fraud risk detected" : "Transaction appears safe",

                Recommendation = recommendation,

                Reason = reason
            };
        }

        public async Task<List<TransactionHistoryDto>> GetHistory(int userId)
        {
            var history = await _context.TransactionHistories

              .Where(x => x.UserId == userId)

                .OrderByDescending(x => x.CreatedAt)

                .Select(x => new TransactionHistoryDto
                {
                    Id = x.Id,

                    TransactionAmount = x.TransactionAmount,

                    MerchantRiskScore = x.MerchantRiskScore,

                    IPRiskScore = x.IPRiskScore,

                    IsInternational = x.IsInternational,

                    FraudProbability = x.FraudProbability,

                    IsFraud = x.IsFraud,

                    CreatedAt = x.CreatedAt,

                    Recommendation = x.Recommendation,

                    Reason = x.Reason
                    
                })

                .ToListAsync();

            return history;
        }

        public async Task<object> ProcessCsvFile(IFormFile file, int userId)
        {
            using var reader =
                new StreamReader(file.OpenReadStream());

           

            using var csv =
                new CsvReader(reader);

            var records =
                csv.GetRecords<CsvTransactionDto>()
                    .ToList();

            int fraudCount = 0;

            int safeCount = 0;

            foreach (var record in records)
            {
                var request =
                    new TransactionRequestDto
                    {
                        TransactionAmount =
                            record.TransactionAmount,

                        MerchantRiskScore =
                            record.MerchantRiskScore,

                        IPRiskScore =
                            record.IPRiskScore,

                        IsInternational =
                            record.IsInternational,

                        CreditScoreBand =
                            record.CreditScoreBand,

                        KycLevel =
                            record.KycLevel,

                        PaymentChannel =
                            record.PaymentChannel,

                        DeviceType =
                            record.DeviceType,

                        PostAuthRiskScore =
                            record.PostAuthRiskScore
                    };

                var prediction =
                    await Predict(request, userId);

                if (prediction.IsFraud)
                {
                    fraudCount++;
                }
                else
                {
                    safeCount++;
                }
            }

            return new
            {
                TotalRecords = records.Count,

                FraudCount = fraudCount,

                SafeCount = safeCount,

                Message =
                    "CSV processed successfully"
            };
        }
    }
}