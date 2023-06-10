﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PD_Helper.Form1;

namespace PD_Helper.Library
{
    internal class ArsenalService
    {
        Dictionary<string, PDCard> cardDef = JsonConvert.DeserializeObject<Dictionary<string, PDCard>>(File.ReadAllText("SkillDB.json"));

        public Arsenal LoadArsenal(string arsenalName)
        {
            var arsenal = new Arsenal();
            arsenal.Name = arsenalName;
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
                    var schoolNumericValue = Int32.Parse(loadSchoolAmount);
                    for (int i = 0; i < 30; i++)
                    {
                        if (!cardDef.ContainsKey(deckStrings[i]))
                        {
                            MessageBox.Show("ERROR09: A Skill from your loaded arsenal does not exist in the game and could not be loaded. The arsenal has been tampered with or was corrupted. Please try loading another arsenal.");
                            break;
                        }
                        else
                        {
                            arsenal.Cards.Add(cardDef[deckStrings[i]]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ERROR06: The loaded Arsenal loaded is not set to 1,2 or 3 Schools.");
                }
            }

            return arsenal;
        }
    }
}