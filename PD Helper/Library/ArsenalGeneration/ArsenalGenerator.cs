using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;

namespace PD_Helper.Library.ArsenalGeneration
{
    public class ArsenalGenerator
    {
        private static Random _random = new Random();

        public List<PDCard> Execute(List<PDCard> skills, GeneratorOptions options)
        {
            int caseSize = GetCaseSize(options.CaseSizes);

            List<string> schools = GetSchools(options.Schools, caseSize);

            PDCard auraSkill = GetAuraSkill(skills);

            skills = FilterAuraFromSkills(skills);

            skills = FilterSkillsBySchool(skills, schools);

            //skills = FilterSkillsByAttackRange(skills, options.AttackRanges);

            List<PDCard> shuffledSkills = ShuffleSkills(skills);

            List<PDCard> randomArsenal = BuildArsenal(shuffledSkills, auraSkill, options.TypeMinimums);

            return randomArsenal;
        }

        private int GetCaseSize(List<int> caseSizes)
        {
            int next = _random.Next(caseSizes.Count);
            return caseSizes[next];
        }

        private List<string> GetSchools(List<string> schoolsInput, int caseSize)
        {
            var schools = new List<string>();

            for (int i = 0; i < caseSize; i++)
            {
                var index = GetRandomIndex(schoolsInput);
                schools.Add(schoolsInput[index]);
                schoolsInput.RemoveAt(index);
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

        private List<PDCard> FilterSkillsByAttackRange(List<PDCard> skills, List<string> attackRanges)
        {
            var attackRangeOptions = new List<string> { "all", "mine", "short", "medium", "long" };
            var attackRangesNotChosen = attackRangeOptions.Except(attackRanges).ToList();

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

        private List<PDCard> BuildArsenal(List<PDCard> shuffledSkills, PDCard auraSkill, Dictionary<string, int> typeMinimums)
        {
            var randomArsenal = new List<PDCard>();
            var arsenalCounter = new ArsenalCounter { Total = 0 };

            BuildArsenalMinimums(randomArsenal, arsenalCounter, typeMinimums, shuffledSkills, auraSkill);

            BuildAuraByRandomCount(randomArsenal, arsenalCounter, auraSkill, typeMinimums);

            BuildArsenalRandoms(randomArsenal, arsenalCounter, shuffledSkills);

            return randomArsenal;
        }

        private void BuildArsenalMinimums(List<PDCard> randomArsenal, ArsenalCounter arsenalCounter, Dictionary<string, int> typeMinimums, List<PDCard> shuffledSkills, PDCard auraSkill)
        {
            foreach (var typeKey in typeMinimums.Keys)
            {
                var minimum = typeMinimums[typeKey];

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

        private void BuildAuraByRandomCount(List<PDCard> randomArsenal, ArsenalCounter arsenalCounter, PDCard auraSkill, Dictionary<string, int> typeMinimums)
        {
            if (!typeMinimums.ContainsKey("Aura"))
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
