using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD_Helper.Library.ArsenalGeneration
{
    public class GeneratorOptions
    {
        public List<int> CaseSizes { get; set; }

        public List<string> Schools { get; set; }

        public List<string> AttackRanges { get; set; }

        public Dictionary<string, int> TypeMinimums { get; set; }

        public Dictionary<string, int> TypeMaximums { get; set; }
    }
}
