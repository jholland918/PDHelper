using System.Diagnostics;

namespace PD_Helper.Library
{
    internal static class AppImages
    {
        public static readonly Image Bug = Image.FromFile(@"Assets\bug.png");
        public static readonly Image Aura = Image.FromFile(@"Assets\aura_26.png");
        public static readonly Image Faith = Image.FromFile(@"Assets\faith_26.png");
        public static readonly Image Ki = Image.FromFile(@"Assets\ki_26.png");
        public static readonly Image Nature = Image.FromFile(@"Assets\nature_26.png");
        public static readonly Image Optical = Image.FromFile(@"Assets\optical_26.png");
        public static readonly Image Psycho = Image.FromFile(@"Assets\psycho_26.png");
        public static readonly Image ArsenalCase1 = Image.FromFile(@"Assets\arsenal_case_1_140.png");
        public static readonly Image ArsenalCase2 = Image.FromFile(@"Assets\arsenal_case_2_140.png");
        public static readonly Image ArsenalCase3 = Image.FromFile(@"Assets\arsenal_case_3_140.png");

        private static readonly Dictionary<string, Image> Schools = new Dictionary<string, Image>
        {
            ["Aura"] = Aura,
            ["Faith"] = Faith,
            ["Ki"] = Ki,
            ["Nature"] = Nature,
            ["Optical"] = Optical,
            ["Psycho"] = Psycho,
        };

        private static readonly Dictionary<int, Image> ArsenalCases = new Dictionary<int, Image>
        {
            [1] = ArsenalCase1,
            [2] = ArsenalCase2,
            [3] = ArsenalCase3,
        };

        public static Image GetArsenalCase(int schoolCount)
        {
            try
            {
                return ArsenalCases[schoolCount];
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Bug;
            }
        }

        public static Image GetSchool(string schoolName)
        {
            try
            {
                return Schools[schoolName];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Bug;
            }
        }
    }
}
