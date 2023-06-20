using System.Text.RegularExpressions;

namespace PD_Helper.Library
{
    /// <summary>
    /// Operations for filesystem arsenals - filesystem arsenals are arsenals not within the game, but saved on the user's computer.
    /// </summary>
    internal class ArsenalService
    {
        /// <summary>
        /// Gets arsenal names from the user's filesystem
        /// </summary>
        public IEnumerable<string> GetArsenalNames()
        {
            DirectoryInfo directory = new DirectoryInfo(@"Arsenals\"); //Assuming Test is your Folder

            FileInfo[] Files = directory.GetFiles("*.arsenal"); //Getting Text files

            return Files.Select(f => Path.GetFileNameWithoutExtension(f.Name));
        }

        /// <summary>
        /// Validates arsenal name uses valid characters and is within the character limit
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="AppException"></exception>
        public void CheckArsenalName(string name)
        {
            if (name == null || name.Length == 0)
            {
                throw new AppException("Invalid arsenal name, must not be null or empty");
            }

            if (name.Length > 15)
            {
                throw new AppException($"Invalid arsenal name, exceeds 15 character maximum. [{name}]");
            }

            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(name.Replace(" ", "")))
            {
                throw new AppException($"Invalid arsenal name, only characters 0-9, a-z, A-Z, and spaces are allowed. [{name}]");
            }
        }

        /// <summary>
        /// Renames an arsenal on the user's filesystem
        /// </summary>
        public void Rename(string oldName, string newName)
        {
            CheckArsenalName(newName);
            DirectoryInfo directory = new DirectoryInfo(@"Arsenals\");

            var oldFile = Path.Combine(directory.FullName, $"{oldName}.arsenal");
            if (!File.Exists(oldFile))
            {
                throw new AppException($"File not found! [{oldFile}]");
            }

            var newFile = Path.Combine(directory.FullName, $"{newName}.arsenal");
            if (File.Exists(newFile))
            {
                throw new AppException($"File already exists with that name! [{oldFile}]");
            }

            File.Move(oldFile, newFile);
        }

        /// <summary>
        /// Creates a new arsenal on the user's filesystem
        /// </summary>
        public Arsenal Create(string arsenalName)
        {
            CheckArsenalName(arsenalName);
            DirectoryInfo directory = new DirectoryInfo(@"Arsenals\");

            var file = Path.Combine(directory.FullName, $"{arsenalName}.arsenal");
            if (File.Exists(file))
            {
                throw new AppException($"File already exists! [{file}]");
            }

            var blankArsenal = "FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,FF FF,01 00,\r\n";
            File.WriteAllText(file, blankArsenal);

            return Read(arsenalName);
        }

        /// <summary>
        /// Reads an arsenal from the user's filesystem
        /// </summary>
        public Arsenal Read(string arsenalName)
        {
            var arsenal = new Arsenal();
            arsenal.ArsenalName = arsenalName;
            string path = @"Arsenals\" + arsenalName + ".arsenal";
            string file = File.ReadAllText(path);
            string[] deckStrings = file.Split(',');
            if (deckStrings.Length < 30)
            {
                MessageBox.Show(@"ERROR10: The arsenal does not contain a valid amount of cards. The arsenal has been tampered with or is corrupted. Please try loading another arsenal.");
            }
            else
            {
                //manual write schools

                string loadSchoolAmount = deckStrings[30].Remove(deckStrings[30].Length - 3);
                if (loadSchoolAmount == "01" || loadSchoolAmount == "02" || loadSchoolAmount == "03")
                {
                    arsenal.SchoolAmount = int.Parse(loadSchoolAmount);
                    for (int i = 0; i < 30; i++)
                    {
                        if (!SkillDB.Skills.ContainsKey(deckStrings[i]))
                        {
                            MessageBox.Show("ERROR09: A Skill from your loaded arsenal does not exist in the game and could not be loaded. The arsenal has been tampered with or was corrupted. Please try loading another arsenal.");
                            break;
                        }
                        else
                        {
                            arsenal.Cards[i] = SkillDB.Skills[deckStrings[i]];
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ERROR06: The loaded Arsenal loaded is not set to 1,2 or 3 Schools.");
                }
            }

            arsenal.SortCards();

            return arsenal;
        }

        /// <summary>
        /// Updates an arsenal on the user's filesystem
        /// </summary>
        public void Update(Arsenal arsenal)
        {
            CheckArsenalName(arsenal.ArsenalName);

            DirectoryInfo directory = new DirectoryInfo(@"Arsenals\");
            var file = Path.Combine(directory.FullName, $"{arsenal.ArsenalName}.arsenal");
            if (!File.Exists(file))
            {
                throw new AppException($"File doesn't exist! [{file}]");
            }

            string skillString = string.Join(',', arsenal.Cards.Select(c => c.HEX));

            var schools = arsenal.Schools;
            int schoolCount = schools.Count();
            if (schoolCount == 0)
            {
                schoolCount = 1; // Sometimes arsenals are all Aura, so set the school count to 1 minimum.
            }

            File.WriteAllText(file, $"{skillString},0{schoolCount} 00,");
        }

        /// <summary>
        /// Deletes an arsenal from the user's filesystem
        /// </summary>
        public void Delete(string arsenalName)
        {
            DirectoryInfo directory = new DirectoryInfo(@"Arsenals\");

            var file = Path.Combine(directory.FullName, $"{arsenalName}.arsenal");
            if (!File.Exists(file))
            {
                throw new AppException($"File not found! [{file}]");
            }

            File.Delete(file);
        }
    }
}
