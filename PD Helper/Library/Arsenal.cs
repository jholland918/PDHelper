using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PD_Helper.Library
{
    /// <summary>
    /// Represents a player's arsenal.
    /// </summary>
    /// <remarks>
    /// This could be an arsenal that has been loaded from the Phantom Dust game, the filesystem, or created from scratch within PD Helper.
    /// </remarks>
    public class Arsenal
    {
        private static readonly Dictionary<string, int> _typeSort = new Dictionary<string, int>
        {
            ["Attack"] = 1,
            ["Defense"] = 2,
            ["Erase"] = 3,
            ["Status"] = 4,
            ["Special"] = 5,
            ["Environment"] = 6,
            ["Aura"] = 7,
        };

        /// <summary>
        /// Returns the string showing how many skills vs aura are in the arsenal.
        /// </summary>
        public string SkillsOverAura { get { return $"{Cards.Where(c => c.TYPE != "Aura").Count()}/30"; } }

        /// <summary>
        /// The arsenal index inside the game
        /// </summary>
        /// <remarks>
        /// This is only relevant when reading the arsenal from the game 
        /// or when the user wants to save the arsenal back to the game.
        /// </remarks>
        public int? ArsenalIndex { get; set; }

        /// <summary>
        /// The name of the arsenal
        /// </summary>
        public string ArsenalName { get; set; }

        /// <summary>
        /// The number of schools used in the arsenal.
        /// </summary>
        /// <remarks>
        /// This should be correct when reading the arsenal from the game, 
        /// but this may need to be recalculated when reading from the 
        /// filesystem (*.arsenal file) or after a user edits an arsenal via PD Helper.
        /// </remarks>
        public int SchoolAmount { get; set; }

        public string[] Schools 
        {
            get
            {
                return Cards.Select(c => c.SCHOOL).Distinct().Where(s => s != "Aura").ToArray();
            }
        }

        public PDCard[] Cards { get; private set; } = new PDCard[30];

        /// <summary>
        /// Creates a new instance using an arsenal index loaded from the game
        /// </summary>
        /// <param name="arsenalIndex">The arsenal index from the game</param>
        public Arsenal(int arsenalIndex) 
            : this()
        {
            ArsenalIndex = arsenalIndex;
        }

        public Arsenal()
        {
        }

        /// <summary>
        /// Sets the school amount from a hex string loaded from the game or the filesystem.
        /// </summary>
        /// <param name="schoolAmountHex"></param>
        public void SetSchoolAmount(string schoolAmountHex)
        {
            SchoolAmount = int.Parse(schoolAmountHex.Remove(schoolAmountHex.Length - 3));
        }

        public void SortCards()
        {
            // Phantom Dust's sorting seems pretty arbitrary except for sorting by skill type,
            // so until I can figure it out I'm just going to sort by name after type.
            Cards = Cards.OrderBy(c => _typeSort[c.TYPE]).ThenBy(c => c.NAME).ToArray();
        }
    }
}
