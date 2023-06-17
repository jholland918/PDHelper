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
                var btn = new Button();
                btn.Text = $"{skill.Value.NAME}";
                btn.AccessibleName = skill.Key;
                btn.MouseEnter += SkillButton_MouseEnter;
                SkillList.Controls.Add(btn);
            }
        }

        private void SkillButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var skill = SkillDB.Skills[button.AccessibleName];
                CardTitle.Text = skill.NAME;
                CardSubtitle.Text = $"COST {skill.COST} STR {skill.DAMAGE} @ {skill.RANGE}";
                CardDescription.Text = skill.DESCRIPTION;
            }
        }

        private void PsychoButton_Click(object sender, EventArgs e)
        {

        }

        private void OpticalButton_Click(object sender, EventArgs e)
        {

        }

        private void NatureButton_Click(object sender, EventArgs e)
        {

        }

        private void KiButton_Click(object sender, EventArgs e)
        {

        }

        private void FaithButton_Click(object sender, EventArgs e)
        {

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
    }
}
