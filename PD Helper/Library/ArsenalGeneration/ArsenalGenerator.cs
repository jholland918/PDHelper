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
        private AppData _appData = AppData.Instance;

        public List<Skill> Execute(GeneratorOptions options)
        {
            List<Skill> skills = _appData.Skills.FindAll().ToList();

            int caseSize = GetCaseSize(options);

            List<string> schools = GetSchools(options, caseSize);

            var auraSkill = GetAuraSkill(skills);

            skills = FilterAuraFromSkills(skills);

            skills = FilterSkillsBySchool(skills, schools);

            skills = FilterSkillsByAttackRange(options, skills);

            var shuffledSkills = ShuffleSkills(skills);

            var randomArsenal = BuildArsenal(options, shuffledSkills, auraSkill);

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

        private Skill GetAuraSkill(IEnumerable<Skill> skills)
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

        private List<Skill> FilterSkillsByAttackRange(GeneratorOptions options, List<Skill> skills)
        {
            var attackRangeOptions = new List<string> { "all", "mine", "short", "medium", "long" };
            var attackRangesNotChosen = attackRangeOptions.Except(options.AttackRanges).ToList();

            foreach (var attackRange in attackRangesNotChosen)
            {
                skills = skills.Where(skill => skill.Type != attackRange).ToList();
            }

            return skills;
        }

        private List<Skill> ShuffleSkills(List<Skill> skills)
        {
            var skillPool = new List<Skill>();

            // Add max amount of skills in the skill pool
            for (int i = 0; i < 3; i++)
            {
                skillPool.AddRange(skills);
            }

            // Now randomize it ;)
            return skillPool.OrderBy(x => _random.Next()).ToList();
        }

        private List<Skill> BuildArsenal(GeneratorOptions options, List<Skill> shuffledSkills, Skill auraSkill)
        {
            var arsenal = new WorkArsenal();

            AddMinimumSkills(options, arsenal, shuffledSkills, auraSkill);

            AddMaximumSkills(options, arsenal, shuffledSkills);

            return arsenal.Cards;
        }

        private void AddMinimumSkills(GeneratorOptions options, WorkArsenal arsenal, List<Skill> shuffledSkills, Skill auraSkill)
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
                        arsenal.Cards.Add(auraSkill);
                    }
                }
                else
                {
                    for (int i = 0; i < minimum; i++)
                    {
                        var index = shuffledSkills.FindIndex(skill => skill.Type == typeKey);
                        arsenal.Cards.Add(shuffledSkills[index]);
                        shuffledSkills.RemoveAt(index);
                    }
                }
            }
        }

        private void AddMaximumSkills(GeneratorOptions options, WorkArsenal arsenal, IEnumerable<Skill> shuffledSkills)
        {
            foreach(var skill in shuffledSkills)
            {
                if (arsenal.Cards.Count >= 30)
                {
                    break;
                }

                int max = options.TypeMaximums[skill.Type];

                if (max == -1)
                {
                    arsenal.Cards.Add(skill);
                    continue;
                }

                if (arsenal.Cards.Count(c => c.Type == skill.Type) < max)
                {
                    arsenal.Cards.Add(skill);
                    continue;
                }
            }
        }

        private int GetRandomIndex(List<string> inputArray)
        {
            var random = new Random();
            return random.Next(inputArray.Count);
        }

        private class WorkArsenal
        {
            public List<Skill> Cards { get; set; } = new List<Skill>();
        }
    }
}
