using Microsoft.ML;
using FraudDetection.ML.Models;

/*
---------------------------------------------------
Fraud Detection Model Comparison

Models Evaluated:
1. FastTree
2. LightGBM
3. SDCA Logistic Regression

Evaluation Metrics:
- Accuracy
- Precision
- Recall
- F1 Score
- AUC

Selected Production Model:
LightGBM

Reason:
Highest F1 Score among all evaluated models.

Used in ASP.NET API:
fraud_model_lightgbm.zip
---------------------------------------------------
*/

var mlContext = new MLContext(seed: 42);

var results = new List<ModelResult>();

// ================= DATA PATHS =================

string trainPath = Path.Combine(
    Environment.CurrentDirectory,
    "BalancedData",
    "balanced_transactions_train.csv");

string testPath = Path.Combine(
    Environment.CurrentDirectory,
    "Data",
    "transactions_test.csv");

// ================= LOAD DATA =================

var trainData = mlContext.Data.LoadFromTextFile<TransactionData>(
    trainPath,
    hasHeader: true,
    separatorChar: ',');

var testData = mlContext.Data.LoadFromTextFile<TransactionData>(
    testPath,
    hasHeader: true,
    separatorChar: ',');

// ================= FEATURE ENGINEERING =================

var dataProcessPipeline = mlContext.Transforms

    // Convert bool -> float
    .Conversion.ConvertType(
        outputColumnName: "IsInternationalFloat",
        inputColumnName: "IsInternational",
        outputKind: Microsoft.ML.Data.DataKind.Single)

    // One-Hot Encoding
    .Append(mlContext.Transforms.Categorical.OneHotEncoding(
        "CreditScoreEncoded",
        "CreditScoreBand"))

    .Append(mlContext.Transforms.Categorical.OneHotEncoding(
        "KycEncoded",
        "KycLevel"))

    .Append(mlContext.Transforms.Categorical.OneHotEncoding(
        "PaymentEncoded",
        "PaymentChannel"))

    .Append(mlContext.Transforms.Categorical.OneHotEncoding(
        "DeviceEncoded",
        "DeviceType"))

    // Feature Vector
    .Append(mlContext.Transforms.Concatenate(
        "Features",

        "AccountAgeDays",
        "AvgMonthlySpend",
        "MerchantRiskScore",
        "TransactionAmount",
        "IPRiskScore",
        "TxnCount1h",
        "TxnCount24h",
        "FailedTxnCount24h",
        "GeoDistance",
        "AmountDeviation",
        "PostAuthRiskScore",
        "IsInternationalFloat",

        "CreditScoreEncoded",
        "KycEncoded",
        "PaymentEncoded",
        "DeviceEncoded"));

// ================= FASTTREE =================

var fastTreePipeline =
    dataProcessPipeline.Append(
        mlContext.BinaryClassification
                 .Trainers.FastTree(
                     numberOfLeaves: 10,
                     numberOfTrees: 20,
                     minimumExampleCountPerLeaf: 20));

// ================= LIGHTGBM =================

var lightGbmPipeline =
    dataProcessPipeline.Append(
        mlContext.BinaryClassification
                 .Trainers.LightGbm());

// ================= SAVE FINAL PRODUCTION MODEL =================

var lightGbmModel =
    lightGbmPipeline.Fit(trainData);

string outputDir = Path.Combine(
    Environment.CurrentDirectory,
    "Output");

if (!Directory.Exists(outputDir))
{
    Directory.CreateDirectory(outputDir);
}

string lightGbmModelPath = Path.Combine(
    outputDir,
    "fraud_model_lightgbm.zip");

mlContext.Model.Save(
    lightGbmModel,
    trainData.Schema,
    lightGbmModelPath);

Console.WriteLine(
    $"LightGBM model saved at: {lightGbmModelPath}");

// ================= SDCA LOGISTIC REGRESSION =================

var sdcaPipeline =
    dataProcessPipeline.Append(
        mlContext.BinaryClassification
                 .Trainers.SdcaLogisticRegression());

// ================= MODEL COMPARISON =================

results.Add(
    TrainAndEvaluate(
        "FastTree",
        fastTreePipeline,
        trainData,
        testData,
        mlContext));

results.Add(
    TrainAndEvaluate(
        "LightGBM",
        lightGbmPipeline,
        trainData,
        testData,
        mlContext));

results.Add(
    TrainAndEvaluate(
        "SDCA",
        sdcaPipeline,
        trainData,
        testData,
        mlContext));

// ================= PRINT RESULTS =================

Console.WriteLine("\n==============================");
Console.WriteLine("MODEL COMPARISON");
Console.WriteLine("==============================");

foreach (var result in results)
{
    Console.WriteLine($"\nModel: {result.ModelName}");

    Console.WriteLine(
        $"Accuracy : {result.Accuracy:P2}");

    Console.WriteLine(
        $"Precision: {result.Precision:P2}");

    Console.WriteLine(
        $"Recall   : {result.Recall:P2}");

    Console.WriteLine(
        $"F1 Score : {result.F1Score:P2}");

    Console.WriteLine(
        $"AUC      : {result.AUC:P2}");
}

// ================= HELPER METHOD =================

static ModelResult TrainAndEvaluate(
    string modelName,
    IEstimator<ITransformer> pipeline,
    IDataView trainData,
    IDataView testData,
    MLContext mlContext)
{
    Console.WriteLine($"\nTraining {modelName}...");

    var model =
        pipeline.Fit(trainData);

    Console.WriteLine(
        $"{modelName} TRAINING COMPLETE");

    var predictions =
        model.Transform(testData);

    var metrics =
        mlContext.BinaryClassification
                 .Evaluate(predictions);

    Console.WriteLine(
        $"{modelName} Accuracy: {metrics.Accuracy:P2}");

    Console.WriteLine(
        $"{modelName} Precision: {metrics.PositivePrecision:P2}");

    Console.WriteLine(
        $"{modelName} Recall: {metrics.PositiveRecall:P2}");

    Console.WriteLine(
        $"{modelName} F1 Score: {metrics.F1Score:P2}");

    return new ModelResult
    {
        ModelName = modelName,
        Accuracy = metrics.Accuracy,
        Precision = metrics.PositivePrecision,
        Recall = metrics.PositiveRecall,
        F1Score = metrics.F1Score,
        AUC = metrics.AreaUnderRocCurve
    };
}