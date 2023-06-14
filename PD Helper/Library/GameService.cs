using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static PD_Helper.Form1;

namespace PD_Helper.Library
{
    /// <summary>
    /// Reads and writes data to the Phantom Dust game itself
    /// </summary>
    internal class GameService
    {
        private readonly string[] arsenalNameOffsets = { "8", "6C", "D0", "134", "198", "1FC", "260", "2C4", "328", "38C", "3F0", "454", "4B8", "51C", "580", "5E4" };
        private readonly string[] arsenalSkillOffsets = { "18", "7C", "E0", "144", "1A8", "20C", "270", "2D4", "338", "39C", "400", "464", "4C8", "52C", "590", "5F4" };
        private readonly Dictionary<string, PDCard> cardDef = JsonConvert.DeserializeObject<Dictionary<string, PDCard>>(File.ReadAllText("SkillDB.json"));

        /// <summary>
        /// Initializes the the <see cref="GameProfile"/> Instance.
        /// </summary>
        /// <remarks>This should not be called outside of the <see cref="GameProfile"/> constructor.</remarks>
        public void LoadGameProfile(GameProfile gameProfile)
        {
            var process = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "PDUWP");
            if (process == null)
            {
                throw new AppException("Could not locate Phantom Dust game process. Make sure Phantom Dust is running and try again.");
            }

            // Get Processes and check for Phantom Dust, attach and enable Arsenal Loading
            gameProfile.ProcessId = process.Id.ToString();

            bool success = gameProfile.Mem.OpenProcess(process.Id, out string error);
            if (!success)
            {
                throw new AppException($"Could not open Phantom Dust game process: Error: [{error}]");
            }

            // Read all names of Arsenals
            for (int o = 0; o < arsenalNameOffsets.Length; o++)
            {
                string setup = "base+003ED6B8," + arsenalNameOffsets[o];
                string currentName = gameProfile.Mem.ReadString(setup, "", 16, true);

                if (currentName.Length > 0)
                {
                    gameProfile.Arsenals.Add(currentName);
                }
            }

            if (gameProfile.Arsenals.Count == 0)
            {
                throw new AppException($"Could not find Phantom Dust game arsenals. This could mean you haven't loaded a player profile in the game yet or are not at a game screen that's supported. Please load a player profile, go to a multiplayer lobby or other supported screen, and try again.");
            }
        }

        /// <summary>
        /// Reads an arsenal from the Phantom Dust game by index
        /// </summary>
        public Arsenal ReadArsenal(int arsenalIndex)
        {
            var arsenal = new Arsenal(arsenalIndex);

            // Read name of Arsenal
            string setup = "base+003ED6B8," + arsenalNameOffsets[arsenalIndex];
            arsenal.ArsenalName = GameProfile.Instance.Mem.ReadString(setup, "", 16, true);

            // Read arsenal skills from game
            byte[] loadDeck = GameProfile.Instance.Mem.ReadBytes("base+003ED6B8," + arsenalSkillOffsets[arsenalIndex], 62);

            int o = 0;
            for (int i = 0; i <= 30; i++)
            {
                byte[] currentByte = { loadDeck[o], loadDeck[o + 1] };
                string currentHexString = BitConverter.ToString(currentByte).Replace('-', ' ');

                if (i < 30)
                {
                    // The cards are the first thirty hex values.
                    arsenal.Cards[i] = cardDef[currentHexString];
                }
                else
                {
                    // The final hex value is the school amount.
                    arsenal.SetSchoolAmount(currentHexString);
                }

                o += 2;
            }

            return arsenal;
        }

        /// <summary>
        /// Writes the given arsenal to the Phantom Dust game
        /// </summary>
        /// <param name="arsenal"></param>
        public void WriteArsenal(Arsenal arsenal)
        {
            ValidateArsenal(arsenal);

            // Write arsenal name
            byte[] deckNameToWrite = Encoding.ASCII.GetBytes(arsenal.ArsenalName);
            Array.Resize(ref deckNameToWrite, 15);
            GameProfile.Instance.Mem.WriteBytes("base+003ED6B8," + arsenalNameOffsets[arsenal.ArsenalIndex.Value], deckNameToWrite);

            // Write arsenal skills
            byte[] dataToWrite = { };
            Array.Resize(ref dataToWrite, 62);
            int o = 0;
            for (int i = 0; i <= 30; i++)
            {
                // The cards are the first thirty hex values and the final hex value is the school amount.
                var hex = i < 30 ? arsenal.Cards[i].HEX : $"0{arsenal.SchoolAmount} 00";
                dataToWrite[o] = Convert.ToByte(hex.Remove(2), 16);
                dataToWrite[o + 1] = Convert.ToByte(hex.Remove(0, 3), 16);
                o += 2;
            }

            GameProfile.Instance.Mem.WriteBytes("base+003ED6B8," + arsenalSkillOffsets[arsenal.ArsenalIndex.Value], dataToWrite);
        }

        /// <summary>
        /// Validates an arsenal before writing it to Phantom Dust
        /// </summary>
        private void ValidateArsenal(Arsenal arsenal)
        {
            if (arsenal.ArsenalIndex == null || arsenal.ArsenalIndex < 0 || arsenal.ArsenalIndex > 15)
            {
                throw new AppException($"Invalid arsenal index [{arsenal.ArsenalIndex}]");
            }

            //school limit checking
            int psy = 0;
            int opt = 0;
            int nat = 0;
            int ki = 0;
            int fai = 0;
            int schoolAmount = 0;

            Dictionary<string, int> skillDupes = new Dictionary<string, int>();
            foreach (var card in arsenal.Cards)
            {
                switch (card.SCHOOL)
                {
                    case "Psycho":
                        psy++;
                        break;
                    case "Optical":
                        opt++;
                        break;
                    case "Nature":
                        nat++;
                        break;
                    case "Ki":
                        ki++;
                        break;
                    case "Faith":
                        fai++;
                        break;
                    case "Aura":
                        break;

                }

                if (skillDupes.ContainsKey(card.NAME))
                {
                    skillDupes[card.NAME]++;
                }
                else
                {
                    skillDupes.Add(card.NAME, 1);
                }
            }

            int maxAllowedSchools = arsenal.SchoolAmount;
            if (psy > 0) { schoolAmount++; }
            if (opt > 0) { schoolAmount++; }
            if (nat > 0) { schoolAmount++; }
            if (ki > 0) { schoolAmount++; }
            if (fai > 0) { schoolAmount++; }

            // Dupe limit checking
            bool isOverDupeLimit = false;
            
            foreach (var item in skillDupes)
            {
                if (item.Value > 3 && item.Key != "Aura Particle")
                {
                    isOverDupeLimit = true;
                }
            }

            if (schoolAmount > maxAllowedSchools)
            {
                throw new AppException("ERROR05: This Arsenal has skills from too many schools. You are limited to: " + maxAllowedSchools.ToString() + " School(s)");
            }

            if (isOverDupeLimit)
            {
                throw new AppException("ERROR06: You cannot have more than 3 of the same skill in an Arsenal");
            }
        }
    }
}
