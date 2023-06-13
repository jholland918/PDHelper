﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;

namespace PD_Helper.Library
{
    internal class Arsenal
    {
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

        public PDCard[] Cards { get; } = new PDCard[30];

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

        public string[] ToHexDeck()
        {
            if (Cards.Length != 30)
            {
                throw new AppException($"Invalid card count [{Cards.Length}], cannot convert to valid Hex Deck.");
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
