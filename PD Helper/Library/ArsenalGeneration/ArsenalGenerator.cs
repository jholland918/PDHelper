using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD_Helper.Library.ArsenalGeneration
{
    public class ArsenalGenerator
    {
        public List<int> Execute(List<Skill> skills, Options options)
        {
            List<int> randomArsenal;

            var caseSize = GetCaseSize(options.CaseSize);

            var schools = GetSchools(options.Schools, caseSize);

            var auraSkill = GetAuraSkill(skills);

            skills = FilterAuraFromSkills(skills);

            skills = FilterSkillsBySchool(skills, schools);

            skills = FilterSkillsByAttackRange(skills, options.AttackRanges);

            var shuffledSkills = ShuffleSkills(skills);

            //randomArsenal = BuildArsenal(shuffledSkills, auraSkill, options.TypeMinimums);

            //return randomArsenal.Select(skill => skill.Id).ToList();

            return null;
        }

        private int GetCaseSize(int caseSizeInput)
        {
            int caseSize;

            if (caseSizeInput >= 1 && caseSizeInput <= 3)
            {
                caseSize = caseSizeInput;
            }
            else
            {
                caseSize = GetRandomInt(1, 3);
            }

            return caseSize;
        }

        private List<string> GetSchools(List<string> schoolsInput, int caseSize)
        {
            var schools = new List<string>();

            if (schoolsInput.Count <= caseSize)
            {
                schools = schoolsInput;
            }
            else
            {
                for (int i = 0; i < caseSize; i++)
                {
                    var index = GetRandomIndex(schoolsInput);
                    schools.Add(schoolsInput[index]);
                    schoolsInput.RemoveAt(index);
                }
            }

            return schools;
        }

        private Skill GetAuraSkill(List<Skill> skills)
        {
            return skills.FirstOrDefault(skill => skill.Type == "Aura");
        }

        private List<Skill> FilterAuraFromSkills(List<Skill> skills)
        {
            return skills.Where(skill => skill.Type != "Aura").ToList();
        }

        private List<Skill> FilterSkillsBySchool(List<Skill> skills, List<string> schools)
        {
            return skills.Where(skill => schools.Contains(skill.School)).ToList();
        }

        private List<Skill> FilterSkillsByAttackRange(List<Skill> skills, List<string> attackRanges)
        {
            var attackRangeOptions = new List<string> { "all", "mine", "short", "medium", "long" };
            var attackRangesNotChosen = attackRangeOptions.Except(attackRanges).ToList();

            foreach (var attackRange in attackRangesNotChosen)
            {
                skills = skills.Where(skill => skill.Distance != attackRange).ToList();
            }

            return skills;
        }

        private List<Skill> ShuffleSkills(List<Skill> skills)
        {
            var skillPool = new List<Skill>();

            for (int i = 0; i < 3; i++)
            {
                skillPool.AddRange(skills);
            }

            var random = new Random();
            return skillPool.OrderBy(x => random.Next()).ToList();
        }

        private List<Skill> BuildArsenal(List<Skill> shuffledSkills, Skill auraSkill, Dictionary<string, int> typeMinimums)
        {
            var randomArsenal = new List<Skill>();
            var arsenalCounter = new ArsenalCounter { Total = 0 };

            BuildArsenalMinimums(randomArsenal, arsenalCounter, typeMinimums, shuffledSkills, auraSkill);

            BuildAuraByRandomCount(randomArsenal, arsenalCounter, auraSkill, typeMinimums);

            BuildArsenalRandoms(randomArsenal, arsenalCounter, shuffledSkills);

            return randomArsenal;
        }

        private void BuildArsenalMinimums(List<Skill> randomArsenal, ArsenalCounter arsenalCounter, Dictionary<string, int> typeMinimums, List<Skill> shuffledSkills, Skill auraSkill)
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
                        var index = shuffledSkills.FindIndex(skill => skill.Type == typeKey);
                        randomArsenal.Add(shuffledSkills[index]);
                        shuffledSkills.RemoveAt(index);
                        arsenalCounter.Total++;
                    }
                }
            }
        }

        private void BuildAuraByRandomCount(List<Skill> randomArsenal, ArsenalCounter arsenalCounter, Skill auraSkill, Dictionary<string, int> typeMinimums)
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

        private void BuildArsenalRandoms(List<Skill> randomArsenal, ArsenalCounter arsenalCounter, List<Skill> shuffledSkills)
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

    public class Skill
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string School { get; set; }
        public string Distance { get; set; }
    }

    public class Options
    {
        public int CaseSize { get; set; }
        public List<string> Schools { get; set; }
        public List<string> AttackRanges { get; set; }
        public Dictionary<string, int> TypeMinimums { get; set; }
    }
}
