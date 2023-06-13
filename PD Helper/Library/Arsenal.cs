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

        public int SchoolAmount { get; set; }

        public List<PDCard> Cards { get; set; } = new List<PDCard>();

        public string[] ToHexDeck()
        {
            if (Cards.Count != 30)
            {
                throw new AppException($"Invalid card count [{Cards.Count}], cannot convert to valid Hex Deck.");
            }

            if (SchoolAmount < 1 || SchoolAmount > 3)
            {
                throw new AppException($"Invalid school count [{SchoolAmount}], cannot convert to valid Hex Deck.");
            }

            var output = Cards.Select(c => c.HEX).ToList();
            output.Add($"0{SchoolAmount} 00");

            return output.ToArray();
        }
    }
}
