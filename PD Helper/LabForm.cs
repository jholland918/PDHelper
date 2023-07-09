using PD_Helper.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static PD_Helper.Form1;

namespace PD_Helper
{
    public partial class LabForm : Form
    {
        private readonly ArsenalService _arsenalService = new();
        private readonly ArsenalSkill[] _arsenalSkills = new ArsenalSkill[30];
        private Dictionary<string, PictureBox> _schoolPictures;
        private ArsenalListItem _currentArsenalListItem;
        private List<ArsenalListItem> _arsenalListItems = new List<ArsenalListItem>();
        private PDCard _lastCardSelected;
        private bool _isEditMode = false;

        private struct ArsenalSkill
        {
            public ArsenalSkill(Button button)
            {
                Button = button;
            }

            public Button Button;
        }

        public LabForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // Tooltip all the things
            var toolTips = new ToolTip();
            toolTips.SetToolTip(SaveChangesButton, "Save to filesystem");
            toolTips.SetToolTip(EditButton, "Toggle edit mode on/off");
            toolTips.SetToolTip(SortButton, "Sort skills");
            toolTips.SetToolTip(DeleteButton, "Delete from filesystem");
            toolTips.SetToolTip(NewButton, "Create arsenal");
            toolTips.SetToolTip(RenameButton, "Rename arsenal on filesystem");

            InitializeSchoolPictures();

            var skillButtons = new[] { ArsenalSkill1, ArsenalSkill2, ArsenalSkill3, ArsenalSkill4, ArsenalSkill5, ArsenalSkill6, ArsenalSkill7, ArsenalSkill8, ArsenalSkill9, ArsenalSkill10, ArsenalSkill11, ArsenalSkill12, ArsenalSkill13, ArsenalSkill14, ArsenalSkill15, ArsenalSkill16, ArsenalSkill17, ArsenalSkill18, ArsenalSkill19, ArsenalSkill20, ArsenalSkill21, ArsenalSkill22, ArsenalSkill23, ArsenalSkill24, ArsenalSkill25, ArsenalSkill26, ArsenalSkill27, ArsenalSkill28, ArsenalSkill29, ArsenalSkill30 };
            for (int i = 0; i < 30; i++)
            {
                _arsenalSkills[i] = new ArsenalSkill(skillButtons[i]);
            }

            InitializeArsenalList();

            ArsenalFilterTextBox.TextChanged += ArsenalFilterTextBox_TextChanged;
        }

        private void InitializeSchoolPictures()
        {
            SchoolPicturePsycho.Image = AppImages.Psycho;
            SchoolPictureOptical.Image = AppImages.Optical;
            SchoolPictureNature.Image = AppImages.Nature;
            SchoolPictureKi.Image = AppImages.Ki;
            SchoolPictureFaith.Image = AppImages.Faith;

            _schoolPictures = new Dictionary<string, PictureBox>
            {
                ["Faith"] = SchoolPictureFaith,
                ["Ki"] = SchoolPictureKi,
                ["Nature"] = SchoolPictureNature,
                ["Optical"] = SchoolPictureOptical,
                ["Psycho"] = SchoolPicturePsycho,
            };
        }

        private void InitializeArsenalList()
        {
            var arsenalNames = _arsenalService.GetArsenalNames();

            foreach (var arsenalName in arsenalNames)
            {
                AddArsenalToList(_arsenalService.Read(arsenalName));
            }
        }

        private void RenderArsenal(ArsenalListItem arsenalListItem)
        {
            if (!ArsenalPanel.Visible)
            {
                ArsenalPanel.Visible = true;
            }

            int schoolCount = arsenalListItem.Arsenal.Schools.Count();
            if (arsenalListItem.Arsenal.Schools.Count() == 0)
            {
                schoolCount = 1; // Sometimes arsenals are all Aura, so set the school count to 1 minimum.
            }

            ArsenalCasePicture.Image = AppImages.GetArsenalCase(schoolCount);
            ArsenalNameLabel.Text = arsenalListItem.ArsenalName;

            foreach (KeyValuePair<string, PictureBox> schoolPicture in _schoolPictures)
            {
                if (arsenalListItem.Arsenal.Schools.Contains(schoolPicture.Key))
                {
                    schoolPicture.Value.Image = AppImages.GetSchool(schoolPicture.Key);
                    schoolPicture.Value.BackColor = AppColors.ForegroundColor;
                }
                else
                {
                    schoolPicture.Value.Image = null;
                    schoolPicture.Value.BackColor = Color.Transparent;
                }
            }

            SkillsOverAuraLabel.Text = arsenalListItem.Arsenal.SkillsOverAura;

            for (int i = 0; i < 30; i++)
            {
                var card = arsenalListItem.Arsenal.Cards[i];
                var skill = _arsenalSkills[i].Button;
                skill.Text = card.NAME;
                skill.BackColor = AppColors.GetSkillColor(card.TYPE);
                skill.Image = AppImages.GetSchool(card.SCHOOL);
                skill.AccessibleName = i.ToString(); // I should probably put this index on a subclass of Button instead of this hacky thing... :)
            }
        }

        private void ArsenalFilterTextBox_TextChanged(object? sender, EventArgs e)
        {
            var searchTerm = ArsenalFilterTextBox.Text;
            if (searchTerm.Length == 0)
            {
                ArsenalListBody.SuspendLayout();
                foreach (Control control in ArsenalListBody.Controls)
                {
                    control.Show();
                }
                ArsenalListBody.ResumeLayout();
                return;
            }

            ArsenalListBody.SuspendLayout();
            foreach (Control control in ArsenalListBody.Controls)
            {
                if (control.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    control.Show();
                }
                else
                {
                    control.Hide();
                }
            }
            ArsenalListBody.ResumeLayout();
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            string newName = PromptForm.Show("Enter new arsenal name", ArsenalNameLabel.Text).Trim();
            if (newName.Length == 0)
            {
                return;
            }

            string oldName = ArsenalNameLabel.Text;
            _arsenalService.Rename(oldName, newName);
            ArsenalNameLabel.Text = newName;
            _arsenalListItems.Where(i => i.ArsenalName == oldName).First().ArsenalName = newName;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string arsenalName = ArsenalNameLabel.Text;
            if (!ConfirmForm.Show($"Delete arsenal \"{arsenalName}\"?"))
            {
                return;
            }

            _arsenalService.Delete(arsenalName);

            var item = _arsenalListItems.Where(x => x.ToString() == arsenalName).First();
            _arsenalListItems.Remove(item);
            ArsenalListBody.Controls.Remove(item);
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            string arsenalName = PromptForm.Show("Enter new arsenal name").Trim();
            if (arsenalName.Length == 0)
            {
                return;
            }

            var arsenal = _arsenalService.Create(arsenalName);
            var arsenalListItem = AddArsenalToList(arsenal);
            ArsenalListBody.Controls.SetChildIndex(arsenalListItem, 0);
        }

        private void ArsenalSkill_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                int index = int.Parse(button.AccessibleName);
                var card = _currentArsenalListItem.Arsenal.Cards[index];
                CardTitlePanel.BackColor = AppColors.GetSkillColor(card.TYPE);
                CardSchoolPicture.Image = AppImages.GetSchool(card.SCHOOL);
                CardTitleLabel.Text = card.NAME;

                string strengthText;
                switch (card.TYPE)
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

                CardSubtitleLeftLabel.Text = $"COST {card.COST} {strengthText} {card.DAMAGE}";
                CardSubtitleRightLabel.Text = $"@ {card.USAGE} {card.RANGE}";
                CardDescriptionLabel.Text = card.DESCRIPTION;
                RangePictureBox.Image = AppImages.GetRange(card.RANGE);
            }
        }

        private void ArsenalListItem_MouseEnter(object? sender, EventArgs e)
        {
            if (_isEditMode)
            {
                return;
            }

            if (sender is ArsenalListItem arsenalListItem)
            {
                if (_currentArsenalListItem != arsenalListItem)
                {
                    _currentArsenalListItem?.SetInactiveColors();
                    _currentArsenalListItem = arsenalListItem;
                    arsenalListItem.SetActiveColors();
                }

                RenderArsenal(arsenalListItem);
            }
        }

        private ArsenalListItem AddArsenalToList(Arsenal arsenal)
        {
            var arsenalListItem = new ArsenalListItem(arsenal);
            _arsenalListItems.Add(arsenalListItem);
            arsenalListItem.MouseEnter += ArsenalListItem_MouseEnter;
            ArsenalListBody.Controls.Add(arsenalListItem);
            return arsenalListItem;
        }

        private void ArsenalSkill_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
            var button = (Button)sender;
            var cardIndex = int.Parse(button.AccessibleName);
            var card = _currentArsenalListItem.Arsenal.Cards[cardIndex];
            var skillSelectForm = new SkillSelectForm(_currentArsenalListItem, card);
            skillSelectForm.ShowDialog();

            if (skillSelectForm.SelectedSkill == null)
            {
                return;
            }

            _lastCardSelected = skillSelectForm.SelectedSkill;

            UpdateArsenalCard(button, _lastCardSelected);
        }

        private void ArsenalSkill_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (sender is Button button)
                {
                    UpdateArsenalCard(button, _lastCardSelected);
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (sender is Button button)
                {
                    UpdateArsenalCard(button, SkillDB.Skills["FF FF"]);
                }
            }
        }

        private void UpdateArsenalCard(Button button, PDCard card)
        {
            var cardIndex = int.Parse(button.AccessibleName);
            var skill = _arsenalSkills[cardIndex].Button;
            _currentArsenalListItem.Arsenal.Cards[cardIndex] = card;
            skill.Text = card.NAME;
            skill.BackColor = AppColors.GetSkillColor(card.TYPE);
            skill.Image = AppImages.GetSchool(card.SCHOOL);
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            _arsenalService.Update(_currentArsenalListItem.Arsenal);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            SetEditMode(!_isEditMode);
        }

        private void SetEditMode(bool isEditMode)
        {
            _isEditMode = isEditMode;
            EditButton.Text = _isEditMode ? "Edit: On" : "Edit: Off";
            SaveChangesAnimationTimer.Enabled = isEditMode;
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            _currentArsenalListItem.Arsenal.SortCards();
            RenderArsenal(_currentArsenalListItem);
        }

        private int _saveButtonOpacity = 0;
        private int _saveButtonAmount = 20;
        private int _saveButtonRest = 0;
        private void SaveChangesAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_saveButtonRest > 0)
            {
                _saveButtonRest -= 20;
                return;
            }

            _saveButtonOpacity += _saveButtonAmount;
            if (_saveButtonOpacity > 255)
            {
                _saveButtonAmount = -20;
                _saveButtonOpacity = 255;
            }

            if (_saveButtonOpacity < 0)
            {
                _saveButtonRest = 400;

                _saveButtonAmount = 20;
                _saveButtonOpacity = 0;
            }

            SaveChangesButton.BackColor = Color.FromArgb(_saveButtonOpacity, AppColors.ForegroundColor);
            
        }

        private void RandomizerButton_Click(object sender, EventArgs e)
        {
            var form = new ArsenalRandomizerForm();
            form.ShowDialog();

            if (form.Arsenal == null)
            {
                return;
            }

            for (int i = 0; i < 30; i++)
            {
                var btn = _arsenalSkills[i].Button;
                UpdateArsenalCard(btn, form.Arsenal[i]);
            }

            _currentArsenalListItem.Arsenal.SortCards();
            RenderArsenal(_currentArsenalListItem);
        }
    }
}
