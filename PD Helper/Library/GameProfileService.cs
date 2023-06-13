using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static PD_Helper.Form1;

namespace PD_Helper.Library
{
    internal class GameProfileService
    {
        private static GameProfile _gameProfile = null; // TODO: Probably don't use a static variable for this.
        private readonly string[] arsenalNameOffsets = { "8", "6C", "D0", "134", "198", "1FC", "260", "2C4", "328", "38C", "3F0", "454", "4B8", "51C", "580", "5E4" };
        private readonly string[] arsenalSkillOffsets = { "18", "7C", "E0", "144", "1A8", "20C", "270", "2D4", "338", "39C", "400", "464", "4C8", "52C", "590", "5F4" };
        private readonly Dictionary<string, PDCard> cardDef = JsonConvert.DeserializeObject<Dictionary<string, PDCard>>(File.ReadAllText("SkillDB.json"));

        public GameProfile LoadGameProfile()
        {
            if (_gameProfile != null)
            {
                return _gameProfile;
            }

            var process = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "PDUWP");
            if (process == null)
            {
                return null;
            }

            var profile = new GameProfile();

            // Get Processes and check for Phantom Dust, attach and enable Arsenal Loading
            profile.ProcessId = process.Id.ToString();

            bool success = profile.Mem.OpenProcess(process.Id, out string error);

            // Read all names of Arsenals
            for (int o = 0; o < arsenalNameOffsets.Length; o++)
            {
                string setup = "base+003ED6B8," + arsenalNameOffsets[o];
                string currentName = profile.Mem.ReadString(setup, "", 16, true);

                if (currentName.Length > 0)
                {
                    profile.Arsenals.Add(profile.Mem.ReadString(setup, "", 16, true));
                }
            }

            _gameProfile = profile;

            return _gameProfile;
        }

        public GameArsenal ReadArsenal(GameProfile profile, int arsenalIndex)
        {
            var arsenal = new GameArsenal(profile, arsenalIndex);

            // Read name of Arsenal
            string setup = "base+003ED6B8," + arsenalNameOffsets[arsenalIndex];
            arsenal.ArsenalName = profile.Mem.ReadString(setup, "", 16, true);

            // Read arsenal skills from game
            byte[] loadDeck = profile.Mem.ReadBytes("base+003ED6B8," + arsenalSkillOffsets[arsenalIndex], 62);

            int o = 0;
            for (int i = 0; i < 30; i++)
            {
                byte[] currentByte = { loadDeck[o], loadDeck[o + 1] };
                string currentHexString = BitConverter.ToString(currentByte).Replace('-', ' ');
                arsenal.Cards.Add(cardDef[currentHexString]);
                arsenal.Deck[i] = currentHexString;
                o += 2;
            }

            // Manual write school amount
            byte[] currentByteFix = { loadDeck[60], loadDeck[61] };
            string currentHexStringFix = BitConverter.ToString(currentByteFix).Replace('-', ' ');
            arsenal.Deck[30] = currentHexStringFix;
            arsenal.LoadSchoolAmount = currentHexStringFix.Remove(currentHexStringFix.Length - 3);

            return arsenal;
        }

        public void WritePdhArsenalToGameArsenal(GameProfile profile, Arsenal pdhArsenal, int gameArsenalIndex)
        {
            // Write arsenal name
            byte[] deckNameToWrite = Encoding.ASCII.GetBytes(pdhArsenal.Name);
            Array.Resize(ref deckNameToWrite, 15);
            profile.Mem.WriteBytes("base+003ED6B8," + arsenalNameOffsets[gameArsenalIndex], deckNameToWrite);

            // Write arsenal skills
            var hexDeck = pdhArsenal.ToHexDeck();
            byte[] dataToWrite = { };
            Array.Resize(ref dataToWrite, 62);
            int o = 0;
            for (int i = 0; i < hexDeck.Length; i++)
            {
                dataToWrite[o] = Convert.ToByte(hexDeck[i].Remove(2), 16);
                dataToWrite[o + 1] = Convert.ToByte(hexDeck[i].Remove(0, 3), 16);
                o += 2;
            }

            profile.Mem.WriteBytes("base+003ED6B8," + arsenalSkillOffsets[gameArsenalIndex], dataToWrite);
        }

        public void WriteArsenal(GameArsenal arsenal)
        {
            ValidateArsenal(arsenal);

            // Write arsenal name
            byte[] deckNameToWrite = Encoding.ASCII.GetBytes(arsenal.ArsenalName);
            Array.Resize(ref deckNameToWrite, 15);
            arsenal.Profile.Mem.WriteBytes("base+003ED6B8," + arsenalNameOffsets[arsenal.ArsenalIndex], deckNameToWrite);

            // Write arsenal skills
            byte[] dataToWrite = { };
            Array.Resize(ref dataToWrite, 62);
            int o = 0;
            for (int i = 0; i < arsenal.Deck.Length; i++)
            {
                dataToWrite[o] = Convert.ToByte(arsenal.Deck[i].Remove(2), 16);
                dataToWrite[o + 1] = Convert.ToByte(arsenal.Deck[i].Remove(0, 3), 16);
                o += 2;
            }

            arsenal.Profile.Mem.WriteBytes("base+003ED6B8," + arsenalSkillOffsets[arsenal.ArsenalIndex], dataToWrite);
        }

        private void ValidateArsenal(GameArsenal arsenal)
        {
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

            int maxAllowedSchools = Convert.ToInt32(arsenal.Deck[30].Remove(2));
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
