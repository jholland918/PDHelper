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

        public SkillSelectForm(ArsenalListItem arsenalListItem, PDCard card)
        {
            this.arsenalListItem = arsenalListItem;
            this.card = card;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            foreach (KeyValuePair<string, PDCard> skill in SkillDB.Skills)
            {
                var button = new SkillButton(skill);
                skillButtons.Add(button);
                button.MouseEnter += SkillButton_MouseEnter;
                SkillList.Controls.Add(button);
            }
        }

        private void SkillButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is SkillButton button)
            {
                var skill = SkillDB.Skills[button.SkillKey];
                CardTitle.Text = skill.NAME;
                CardSubtitle.Text = $"COST {skill.COST} STR {skill.DAMAGE} @ {skill.RANGE}";
                CardDescription.Text = skill.DESCRIPTION;
            }
        }

        private void AuraButton_Click(object sender, EventArgs e)
        {

        }

        private void AttackButton_Click(object sender, EventArgs e)
        {

        }

        private void DefenseButton_Click(object sender, EventArgs e)
        {

        }

        private void EraseButton_Click(object sender, EventArgs e)
        {

        }

        private void StatusButton_Click(object sender, EventArgs e)
        {

        }

        private void SpecialButton_Click(object sender, EventArgs e)
        {

        }

        private void EnvironmentalButton_Click(object sender, EventArgs e)
        {

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
            // School filters
            if (!PsychoCheckBox.Checked && card.SCHOOL == "Psycho")
            {
                return false;
            }

            if (!OpticalCheckBox.Checked && card.SCHOOL == "Optical")
            {
                return false;
            }

            if (!NatureCheckBox.Checked && card.SCHOOL == "Nature")
            {
                return false;
            }

            if (!KiCheckBox.Checked && card.SCHOOL == "Ki")
            {
                return false;
            }

            if (!FaithCheckBox.Checked && card.SCHOOL == "Faith")
            {
                return false;
            }

            if (!AuraCheckBox.Checked && card.SCHOOL == "Aura")
            {
                return false;
            }

            // Type filters
            if (!AttackCheckBox.Checked && card.TYPE == "Attack")
            {
                return false;
            }

            if (!DefenseCheckBox.Checked && card.TYPE == "Defense")
            {
                return false;
            }

            if (!EraseCheckBox.Checked && card.TYPE == "Erase")
            {
                return false;
            }

            if (!StatusCheckBox.Checked && card.TYPE == "Status")
            {
                return false;
            }

            if (!SpecialCheckBox.Checked && card.TYPE == "Special")
            {
                return false;
            }

            if (!EnvironmentalCheckBox.Checked && card.TYPE == "Environment")
            {
                return false;
            }


            return true;
        }
    }
}
