using PD_Helper.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PD_Helper.Form1;


namespace PD_Helper
{
    public partial class SkillSelectForm : Form
    {
        private AppData _appData = AppData.Instance;
        private ArsenalListItem _arsenalListItem;
        private Skill _card;
        private static string _activeType = "Aura";
        private Dictionary<string, Button> _typeButtons;
        private List<SkillButton> _skillButtons = new List<SkillButton>();
        private Dictionary<string, Button> _schoolButtons;
        private static Dictionary<string, bool> _activeSchools = new Dictionary<string, bool>
        {
            ["Psycho"] = true,
            ["Optical"] = true,
            ["Nature"] = true,
            ["Ki"] = true,
            ["Faith"] = true,
        };

        private List<string> _sortOptions = new List<string>
        {
            "School",
            "Cost",
            "Strength",
            "Number of Uses",
            "Range",
            "ID",
            "Amount",
            "None"
        };

        public Skill SelectedSkill { get; private set; }

        public int ConfirmCount { get; private set; } = 0;

        public SkillSelectForm(ArsenalListItem arsenalListItem, Skill card)
        {
            _arsenalListItem = arsenalListItem;
            _card = card;
            InitializeComponent();
            Initialize();
        }

        private static int[] _selectedSortOptions = new[] { 0, 1, 2 };

        private void Initialize()
        {
            SortComboBox1.DataSource = new List<string>(_sortOptions);
            SortComboBox2.DataSource = new List<string>(_sortOptions);
            SortComboBox3.DataSource = new List<string>(_sortOptions);
            SortComboBox1.SelectedIndex = _selectedSortOptions[0];
            SortComboBox2.SelectedIndex = _selectedSortOptions[1];
            SortComboBox3.SelectedIndex = _selectedSortOptions[2];
            SortComboBox1.SelectedIndexChanged += SortComboBox_SelectedIndexChanged;
            SortComboBox2.SelectedIndexChanged += SortComboBox_SelectedIndexChanged;
            SortComboBox3.SelectedIndexChanged += SortComboBox_SelectedIndexChanged;

            _typeButtons = new Dictionary<string, Button>
            {
                ["Aura"] = AuraButton,
                ["Attack"] = AttackButton,
                ["Defense"] = DefenseButton,
                ["Erase"] = EraseButton,
                ["Status"] = StatusButton,
                ["Special"] = SpecialButton,
                ["Environment"] = EnvironmentButton,
            };

            SetActiveType(_card.Type);

            _schoolButtons = new Dictionary<string, Button>
            {
                ["Psycho"] = PsychoButton,
                ["Optical"] = OpticalButton,
                ["Nature"] = NatureButton,
                ["Ki"] = KiButton,
                ["Faith"] = FaithButton,
            };

            var arsenalSchools = _arsenalListItem.Arsenal.Cards.Select(c => c.School).Distinct();
            if (arsenalSchools.Any())
            {
                foreach (string key in _activeSchools.Keys)
                {
                    _activeSchools[key] = arsenalSchools.Contains(key);
                }
            }

            foreach (var kvp in _activeSchools)
            {
                _schoolButtons[kvp.Key].BackColor = _activeSchools[kvp.Key] ? AppColors.ForegroundColor : AppColors.BackgroundColor;
            }

            var skills = _appData.Skills.FindAll().OrderBy(x => x.School).ThenBy(x => x.Cost).ThenBy(x => x.Damage);
            foreach (var skill in skills)
            {
                var button = new SkillButton(skill);
                _skillButtons.Add(button);
                button.MouseEnter += SkillButton_MouseEnter;
                button.Click += SkillButton_Click;
                SkillList.Controls.Add(button);
            }

            FilterSkills();

            var skillButton = _skillButtons.FirstOrDefault(b => b.Card.PdId == _card.PdId);
            if (skillButton != null)
            {
                SkillList.ScrollControlIntoView(skillButton);
            }
        }

        private void SkillButton_Click(object? sender, EventArgs e)
        {
            if (sender is SkillButton button)
            {
                SelectedSkill = _appData.GetSkill(button.SkillKey);
                Close();
            }
        }

        private void FilterSkills()
        {
            SkillList.SuspendLayout();
            foreach (var skillButton in _skillButtons)
            {
                skillButton.Visible = ShowSkill(skillButton.Card);
            }
            SkillList.ResumeLayout();
        }

        private bool ShowSkill(Skill card)
        {
            var searchTerm = SearchTextBox.Text.Trim();
            if (searchTerm.Length > 0 && !card.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Type filter
            if (card.Type != _activeType)
            {
                return false;
            }

            // School filters
            if (!_activeSchools["Psycho"] && card.School == "Psycho")
            {
                return false;
            }

            if (!_activeSchools["Optical"] && card.School == "Optical")
            {
                return false;
            }

            if (!_activeSchools["Nature"] && card.School == "Nature")
            {
                return false;
            }

            if (!_activeSchools["Ki"] && card.School == "Ki")
            {
                return false;
            }

            if (!_activeSchools["Faith"] && card.School == "Faith")
            {
                return false;
            }

            return true;
        }

        private void SetActiveType(string? type = null)
        {
            if (type != null)
            {
                _activeType = type;
            }

            foreach (var kvp in _typeButtons)
            {
                if (kvp.Key == _activeType)
                {
                    kvp.Value.BackColor = AppColors.GetSkillColor(kvp.Key);
                    kvp.Value.ForeColor = AppColors.BackgroundColor;
                }
                else
                {
                    kvp.Value.BackColor = AppColors.BackgroundColor;
                    kvp.Value.ForeColor = AppColors.GetSkillColor(kvp.Key);
                }
            }

            FilterSkills();
        }

        private void SetActiveSchool(string school)
        {
            _activeSchools[school] = !_activeSchools[school];
            foreach (var kvp in _activeSchools)
            {
                _schoolButtons[kvp.Key].BackColor = _activeSchools[kvp.Key] ? AppColors.ForegroundColor : AppColors.BackgroundColorMedium;
            }

            FilterSkills();
        }

        private void SetExclusiveSchool(MouseEventArgs e, string school)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            foreach (var kvp in _activeSchools)
            {
                if (kvp.Key == school)
                {
                    _activeSchools[kvp.Key] = true;
                    _schoolButtons[kvp.Key].BackColor = AppColors.ForegroundColor;
                }
                else
                {
                    _activeSchools[kvp.Key] = false;
                    _schoolButtons[kvp.Key].BackColor = AppColors.BackgroundColorMedium;

                }
            }

            FilterSkills();
        }

        private void SortSkills()
        {
            object GetSortProp(Skill card, string sortType)
            {
                switch (sortType)
                {
                    case "School":
                        return card.School;
                    case "Cost":
                        return card.Cost;
                    case "Strength":
                        return card.Damage;
                    case "Number of Uses":
                        return card.Usage;
                    case "Range":
                        return card.Range;
                    case "ID":
                        return card.PdId;
                    case "Amount":
                        return card.Cost;
                    default:
                        return 1;
                }
            }

            _selectedSortOptions[0] = SortComboBox1.SelectedIndex;
            _selectedSortOptions[1] = SortComboBox2.SelectedIndex;
            _selectedSortOptions[2] = SortComboBox3.SelectedIndex;

            var sortType1 = SortComboBox1.SelectedItem.ToString();
            var sortType2 = SortComboBox2.SelectedItem.ToString();
            var sortType3 = SortComboBox3.SelectedItem.ToString();
            var sortedControls =
                SkillList.Controls.Cast<SkillButton>()
                .OrderBy(x => GetSortProp(x.Card, sortType1))
                .ThenBy(x => GetSortProp(x.Card, sortType2))
                .ThenBy(x => GetSortProp(x.Card, sortType3))
                .ToArray();

            SkillList.SuspendLayout();
            SkillList.Controls.Clear();
            SkillList.Controls.AddRange(sortedControls);
            SkillList.ResumeLayout();
        }

        private void SkillButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is SkillButton button)
            {
                var skill = _appData.GetSkill(button.SkillKey);
                CardHeaderPanel.BackColor = AppColors.GetSkillColor(skill.Type);
                CardSchoolPicture.Image = AppImages.GetSchool(skill.School);
                CardTitle.Text = skill.Name;

                string strengthText;
                switch (skill.Type)
                {
                    case "Attack":
                        strengthText = "DEF";
                        break;
                    case "Defense":
                        strengthText = "DEF";
                        break;
                    default:
                        strengthText = "---";
                        break;
                }

                CardSubtitleLeft.Text = $"COST {skill.Cost} {strengthText} {skill.Damage}";
                CardSubtitleRight.Text = $"@ {skill.Usage} {skill.Range}";
                CardRange.Image = AppImages.GetRange(skill.Range);
                CardDescription.Text = skill.Description;
            }
        }

        #region Event Handlers
        private void SearchTextBox_TextChanged(object sender, EventArgs e) => FilterSkills();
        private void SortComboBox_SelectedIndexChanged(object? sender, EventArgs e) => SortSkills();
        private void AuraButton_Click(object sender, EventArgs e) => SetActiveType("Aura");
        private void AttackButton_Click(object sender, EventArgs e) => SetActiveType("Attack");
        private void DefenseButton_Click(object sender, EventArgs e) => SetActiveType("Defense");
        private void EraseButton_Click(object sender, EventArgs e) => SetActiveType("Erase");
        private void StatusButton_Click(object sender, EventArgs e) => SetActiveType("Status");
        private void SpecialButton_Click(object sender, EventArgs e) => SetActiveType("Special");
        private void EnvironmentButton_Click(object sender, EventArgs e) => SetActiveType("Environment");
        private void PsychoButton_Click(object sender, EventArgs e) => SetActiveSchool("Psycho");
        private void OpticalButton_Click(object sender, EventArgs e) => SetActiveSchool("Optical");
        private void NatureButton_Click(object sender, EventArgs e) => SetActiveSchool("Nature");
        private void KiButton_Click(object sender, EventArgs e) => SetActiveSchool("Ki");
        private void FaithButton_Click(object sender, EventArgs e) => SetActiveSchool("Faith");
        private void PsychoButton_MouseUp(object sender, MouseEventArgs e) => SetExclusiveSchool(e, "Psycho");
        private void OpticalButton_MouseUp(object sender, MouseEventArgs e) => SetExclusiveSchool(e, "Optical");
        private void NatureButton_MouseUp(object sender, MouseEventArgs e) => SetExclusiveSchool(e, "Nature");
        private void KiButton_MouseUp(object sender, MouseEventArgs e) => SetExclusiveSchool(e, "Ki");
        private void FaithButton_MouseUp(object sender, MouseEventArgs e) => SetExclusiveSchool(e, "Faith");
        #endregion Event Handlers
    }
}
