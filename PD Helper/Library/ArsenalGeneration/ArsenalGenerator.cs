using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;
using static System.Windows.Forms.Design.AxImporter;

namespace PD_Helper.Library.ArsenalGeneration
{
    public class ArsenalGenerator
    {
        private static Random _random = new Random();


        public List<PDCard> Execute(GeneratorOptions options)
        {
            List<PDCard> skills = SkillDB.Skills.Values.ToList();

            int caseSize = GetCaseSize(options);

            List<string> schools = GetSchools(options, caseSize);

            PDCard auraSkill = GetAuraSkill(skills);

            skills = FilterAuraFromSkills(skills);

            skills = FilterSkillsBySchool(skills, schools);

            skills = FilterSkillsByAttackRange(options, skills);

            List<PDCard> shuffledSkills = ShuffleSkills(skills);

            List<PDCard> randomArsenal = BuildArsenal(options, shuffledSkills, auraSkill);

            return randomArsenal;
        }

        private int GetCaseSize(GeneratorOptions options)
        {
            int next = _random.Next(options.CaseSizes.Count);
            return options.CaseSizes[next];
        }

        private List<string> GetSchools(GeneratorOptions options, int caseSize)
        {
            var schools = new List<string>();

            for (int i = 0; i < caseSize; i++)
            {
                var index = GetRandomIndex(options.Schools);
                schools.Add(options.Schools[index]);
                options.Schools.RemoveAt(index);
            }

            return schools;
        }

        private PDCard GetAuraSkill(List<PDCard> skills)
        {
            return skills.FirstOrDefault(skill => skill.TYPE == "Aura");
        }

        private List<PDCard> FilterAuraFromSkills(List<PDCard> skills)
        {
            return skills.Where(skill => skill.TYPE != "Aura").ToList();
        }

        private List<PDCard> FilterSkillsBySchool(List<PDCard> skills, List<string> schools)
        {
            return skills.Where(skill => schools.Contains(skill.SCHOOL)).ToList();
        }

        private List<PDCard> FilterSkillsByAttackRange(GeneratorOptions options, List<PDCard> skills)
        {
            var attackRangeOptions = new List<string> { "all", "mine", "short", "medium", "long" };
            var attackRangesNotChosen = attackRangeOptions.Except(options.AttackRanges).ToList();

            foreach (var attackRange in attackRangesNotChosen)
            {
                skills = skills.Where(skill => skill.RANGE != attackRange).ToList();
            }

            return skills;
        }

        private List<PDCard> ShuffleSkills(List<PDCard> skills)
        {
            var skillPool = new List<PDCard>();

            // Add max amount of skills in the skill pool
            for (int i = 0; i < 3; i++)
            {
                skillPool.AddRange(skills);
            }

            // Now randomize it ;)
            return skillPool.OrderBy(x => _random.Next()).ToList();
        }

        private List<PDCard> BuildArsenal(GeneratorOptions options, List<PDCard> shuffledSkills, PDCard auraSkill)
        {
            var randomArsenal = new List<PDCard>();
            var arsenalCounter = new ArsenalCounter { Total = 0 };

            BuildArsenalMinimums(options, randomArsenal, arsenalCounter, shuffledSkills, auraSkill);

            BuildAuraByRandomCount(options, randomArsenal, arsenalCounter, auraSkill);

            BuildArsenalRandoms(randomArsenal, arsenalCounter, shuffledSkills);

            return randomArsenal;
        }

        private void BuildArsenalMinimums(GeneratorOptions options, List<PDCard> randomArsenal, ArsenalCounter arsenalCounter, List<PDCard> shuffledSkills, PDCard auraSkill)
        {
            foreach (var typeKey in options.TypeMinimums.Keys)
            {
                var minimum = options.TypeMinimums[typeKey];

                if (minimum == -1)
                {
                    continue;
                }

                if (typeKey == "Aura")
                {
                    for (int i = 0; i < minimum; i++)
                    {
                        randomArsenal.Add(auraSkill);
                        arsenalCounter.Total++;
                    }
                }
                else
                {
                    for (int i = 0; i < minimum; i++)
                    {
                        var index = shuffledSkills.FindIndex(skill => skill.TYPE == typeKey);
                        randomArsenal.Add(shuffledSkills[index]);
                        shuffledSkills.RemoveAt(index);
                        arsenalCounter.Total++;
                    }
                }
            }
        }

        private void BuildAuraByRandomCount(GeneratorOptions options, List<PDCard> randomArsenal, ArsenalCounter arsenalCounter, PDCard auraSkill)
        {
            if (!options.TypeMinimums.ContainsKey("Aura"))
            {
                var remainingArsenalSlots = 30 - arsenalCounter.Total;
                var numOfAuraToAdd = GetRandomInt(1, remainingArsenalSlots);

                for (int i = 0; i < numOfAuraToAdd; i++)
                {
                    randomArsenal.Add(auraSkill);
                    arsenalCounter.Total++;
                }
            }
        }

        private void BuildArsenalRandoms(List<PDCard> randomArsenal, ArsenalCounter arsenalCounter, List<PDCard> shuffledSkills)
        {
            var remainingOpenEntries = 30 - arsenalCounter.Total;

            for (int i = 0; i < remainingOpenEntries; i++)
            {
                randomArsenal.Add(shuffledSkills[i]);
            }
        }

        private int GetRandomInt(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max + 1);
        }

        private int GetRandomIndex(List<string> inputArray)
        {
            var random = new Random();
            return random.Next(inputArray.Count);
        }

        private class ArsenalCounter
        {
            public int Total { get; set; }
        }
    }
}
