using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.ML.Models
{
    internal class ConfusionMatrixResult
    {
 
        public int TruePositive { get; set; }

        public int TrueNegative { get; set; }

        public int FalsePositive { get; set; }

        public int FalseNegative { get; set; }
    }
}