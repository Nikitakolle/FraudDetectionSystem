using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.ML.BalancedData
{
    public static class BalanceDataset
    {
        public static void Run()
        {
            string inputPath = Path.Combine(
                Environment.CurrentDirectory,
                "Data",
                "transactions_train.csv");

            string outputDir = Path.Combine(
                Environment.CurrentDirectory,
                "BalancedData");

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            string outputPath = Path.Combine(
                outputDir,
                "balanced_transactions_train.csv");

            var lines = File.ReadAllLines(inputPath);

            var header = lines[0];

            var fraudRows = new List<string>();
            var nonFraudRows = new List<string>();

            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',');

               
                string label = columns[19];

                if (i < 5)
                {
                    Console.WriteLine($"Row {i}: Label = {label}");
                }
                if (label == "1")
                {
                    fraudRows.Add(lines[i]);
                }
                else
                {
                    nonFraudRows.Add(lines[i]);
                }
            }

            Console.WriteLine($"Fraud Rows: {fraudRows.Count}");
            Console.WriteLine($"Non-Fraud Rows: {nonFraudRows.Count}");

            var balancedRows = new List<string>();

            balancedRows.AddRange(nonFraudRows);

            int multiplier = nonFraudRows.Count / fraudRows.Count;

            for (int i = 0; i < multiplier; i++)
            {
                balancedRows.AddRange(fraudRows);
            }
            

            Console.WriteLine($"Balanced Rows: {balancedRows.Count}");

            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(header);

                foreach (var row in balancedRows)
                {
                    writer.WriteLine(row);
                }
            }

            Console.WriteLine($"\nBalanced dataset created:");
            Console.WriteLine(outputPath);
        }
    }
}