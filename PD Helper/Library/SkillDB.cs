using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;

namespace PD_Helper.Library
{
    internal static class SkillDB
    {
        public static readonly Dictionary<string, PDCard> Skills = JsonConvert.DeserializeObject<Dictionary<string, PDCard>>(File.ReadAllText("SkillDB.json"));
    }
}
