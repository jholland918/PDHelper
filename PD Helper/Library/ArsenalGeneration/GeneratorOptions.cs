namespace PD_Helper.Library.ArsenalGeneration
{
    public class GeneratorOptions
    {
        public List<int> CaseSizes { get; set; } = new List<int>();

        public List<string> Schools { get; set; } = new List<string>();

        public List<string> AttackRanges { get; set; } = new List<string>();

        public Dictionary<string, int> TypeMinimums { get; set; } = new Dictionary<string, int>();

        public Dictionary<string, int> TypeMaximums { get; set; } = new Dictionary<string, int>();
    }
}
