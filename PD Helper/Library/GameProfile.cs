using Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD_Helper.Library
{
    internal class GameProfile
    {
        public Mem Mem { get; } = new Mem();

        public string ProcessId { get; set; }

        public List<string> Arsenals { get; set; } = new List<string>();
    }
}
