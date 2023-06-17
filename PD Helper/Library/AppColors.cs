using System.Diagnostics;

namespace PD_Helper.Library
{
    /// <summary>
    /// Contains some common colors for the application.
    /// </summary>
    internal class AppColors
    {
        public static readonly Color Attack = Color.FromArgb(220, 107, 69);
        public static readonly Color Defense = Color.FromArgb(75, 97, 220);
        public static readonly Color Environment = Color.FromArgb(75, 220, 218);
        public static readonly Color Special = Color.FromArgb(220, 218, 75);
        public static readonly Color Status = Color.FromArgb(114, 220, 81);
        public static readonly Color Erase = Color.FromArgb(170, 75, 220);
        public static readonly Color Aura = Color.FromArgb(127, 177, 150);

        public static readonly Color BackgroundColor = Color.FromArgb(33, 53, 47);
        public static readonly Color BackgroundColorHover = Color.FromArgb(67, 11, 15);
        public static readonly Color ForegroundColor = Color.FromArgb(92, 172, 149);
        public static readonly Color ForegroundColorHover = Color.FromArgb(216, 185, 24);

        private static readonly Dictionary<string, Color> SkillTypes = new Dictionary<string, Color>
        {
            ["Attack"] = Attack,
            ["Defense"] = Defense,
            ["Environment"] = Environment,
            ["Special"] = Special,
            ["Status"] = Status,
            ["Erase"] = Erase,
            ["Aura"] = Aura,
        };

        public static Color GetSkillColor(string skillType)
        {
            try
            {
                return SkillTypes[skillType];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Color.Orange; // These skills aren't orange so this should stand out :)
            }
        }
    }
}
