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
        private ArsenalListItem arsenalListItem;
        private PDCard card;
        private List<SkillButton> skillButtons = new List<SkillButton>();
        private Dictionary<string, bool> activeSchools = new Dictionary<string, bool>
        {
            ["Psycho"] = true,
            ["Optical"] = true,
            ["Nature"] = true,
            ["Ki"] = true,
            ["Faith"] = true,
        };


        public SkillSelectForm(ArsenalListItem arsenalListItem, PDCard card)
        {
            this.arsenalListItem = arsenalListItem;
            this.card = card;
            InitializeComponent();
            Initialize();
        }

        private Dictionary<string, Button> schoolButtons;

        private void Initialize()
        {
            typeButtons = new Dictionary<string, Button>
            {
                ["Aura"] = AuraButton,
                ["Attack"] = AttackButton,
                ["Defense"] = DefenseButton,
                ["Erase"] = EraseButton,
                ["Status"] = StatusButton,
                ["Special"] = SpecialButton,
                ["Environment"] = EnvironmentButton,
            };

            schoolButtons = new Dictionary<string, Button>
            {
                ["Psycho"] = PsychoButton,
                ["Optical"] = OpticalButton,
                ["Nature"] = NatureButton,
                ["Ki"] = KiButton,
                ["Faith"] = FaithButton,
            };

            foreach (KeyValuePair<string, PDCard> skill in SkillDB.Skills)
            {
                var button = new SkillButton(skill);
                skillButtons.Add(button);
                button.MouseEnter += SkillButton_MouseEnter;
                SkillList.Controls.Add(button);
            }

            FilterSkills();
        }

        private void SkillButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is SkillButton button)
            {
                var skill = SkillDB.Skills[button.SkillKey];
                CardHeaderPanel.BackColor = AppColors.GetSkillColor(skill.TYPE);
                CardSchoolPicture.Image = AppImages.GetSchool(skill.SCHOOL);
                CardTitle.Text = skill.NAME;
                CardSubtitle.Text = $"COST {skill.COST} STR {skill.DAMAGE} @ {skill.RANGE}";
                CardDescription.Text = skill.DESCRIPTION;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {

        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {

        }

        private void ConfirmDropdown_Click(object sender, EventArgs e)
        {

        }

        private void SortComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SortComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SortComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FilterSkills();
        }

        private void FilterSkills()
        {
            SkillList.SuspendLayout();
            foreach(var skillButton in skillButtons)
            {
                skillButton.Visible = ShowSkill(skillButton.Card);
            }
            SkillList.ResumeLayout();
        }

        private bool ShowSkill(PDCard card)
        {
            var searchTerm = SearchTextBox.Text.Trim();
            if (searchTerm.Length > 0 && !card.NAME.Contains(searchTerm,StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Type filter
            if (card.TYPE != activeType)
            {
                return false;
            }

            // School filters
            if (!activeSchools["Psycho"] && card.SCHOOL == "Psycho")
            {
                return false;
            }

            if (!activeSchools["Optical"] && card.SCHOOL == "Optical")
            {
                return false;
            }

            if (!activeSchools["Nature"] && card.SCHOOL == "Nature")
            {
                return false;
            }

            if (!activeSchools["Ki"] && card.SCHOOL == "Ki")
            {
                return false;
            }

            if (!activeSchools["Faith"] && card.SCHOOL == "Faith")
            {
                return false;
            }

            return true;
        }

        private string activeType = "Aura";
        private Dictionary<string, Button> typeButtons;

        private void AuraButton_Click(object sender, EventArgs e)
        {
            SetActiveType( "Aura");
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            SetActiveType( "Attack");
        }

        private void DefenseButton_Click(object sender, EventArgs e)
        {
            SetActiveType( "Defense");
        }

        private void EraseButton_Click(object sender, EventArgs e)
        {
            SetActiveType( "Erase");
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            SetActiveType ("Status");
        }

        private void SpecialButton_Click(object sender, EventArgs e)
        {
            SetActiveType( "Special");
        }

        private void EnvironmentButton_Click(object sender, EventArgs e)
        {
            SetActiveType( "Environment");
        }

        private void PsychoButton_Click(object sender, EventArgs e)
        {
            SetActiveSchool("Psycho");
        }

        private void OpticalButton_Click(object sender, EventArgs e)
        {
            SetActiveSchool("Optical");
        }

        private void NatureButton_Click(object sender, EventArgs e)
        {
            SetActiveSchool("Nature");
        }

        private void KiButton_Click(object sender, EventArgs e)
        {
            SetActiveSchool("Ki");
        }

        private void FaithButton_Click(object sender, EventArgs e)
        {
            SetActiveSchool("Faith");
        }

        private void SetActiveType(string type)
        {
            activeType = type;
            foreach (var kvp in typeButtons)
            {
                if (kvp.Key == activeType)
                {
                    //kvp.Value.Font = new Font(kvp.Value.Font, FontStyle.Bold);
                    kvp.Value.BackColor = AppColors.GetSkillColor(kvp.Key);
                    kvp.Value.ForeColor = AppColors.BackgroundColor;
                }
                else
                {
                    //kvp.Value.Font = new Font(kvp.Value.Font, FontStyle.Regular);
                    kvp.Value.BackColor = AppColors.BackgroundColor;
                    kvp.Value.ForeColor = AppColors.GetSkillColor(kvp.Key);
                }
            }

            FilterSkills();
        }

        private void SetActiveSchool(string school)
        {
            activeSchools[school] = !activeSchools[school];
            foreach(var kvp in activeSchools)
            {
                schoolButtons[kvp.Key].BackColor = activeSchools[kvp.Key] ? AppColors.ForegroundColor : AppColors.BackgroundColorMedium;
            }

            FilterSkills();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterSkills();
        }
    }
}
