using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;

namespace PD_Helper.Library
{
    internal class GameArsenal
    {
        public GameArsenal(GameProfile profile, int arsenalIndex)
        {
            Profile = profile;
            ArsenalIndex = arsenalIndex;
        }

        public GameProfile Profile { get; }

        public int ArsenalIndex { get; }

        public string ArsenalName { get; set; }

        public string LoadSchoolAmount { get; set; }

        public List<PDCard> Cards { get; set; } = new List<PDCard>();

        public string[] Deck { get { return loadedDeck; } }

        private string[] loadedDeck = new string[] { "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "FF FF", "00 00" };
    }
}
