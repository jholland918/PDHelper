using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;

namespace PD_Helper.Library
{
    internal class Arsenal
    {
        public string Name { get; set; }

        public List<PDCard> Cards { get; set; } = new List<PDCard>();
    }
}
