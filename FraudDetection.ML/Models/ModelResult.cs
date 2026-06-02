using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.ML.Models
{
   
    public class ModelResult
    {
        public string ModelName { get; set; }

        public double Accuracy { get; set; }

        public double Precision { get; set; }

        public double Recall { get; set; }

        public double F1Score { get; set; }

        public double AUC { get; set; }
    }
}