using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdAIL_GenAlgorithmTSP
{
    public class Settings
    {
        public const string CsvPath = "european.csv";
        public const int CitiesCount = 100;
        public const int PopulationSize = 20;
        public const int MaxPopulation = 5000;
        public const float MutationRatio = 1f;
        public const int ErrorThreshold = 2;
    }
}
