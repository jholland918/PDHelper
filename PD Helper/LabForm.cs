﻿using PD_Helper.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        private readonly GameService _gameService = new();
        private readonly ArsenalService _arsenalService = new();
        private readonly List<Button> _arsenalSkills = new();
        private Dictionary<string, PictureBox> _schoolPictures;
        private ArsenalListItem _currentArsenalListItem = null;
        private Dictionary<int, string> _arsenalRows = new();

        public LabForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            InitializeSchoolPictures();

            _arsenalSkills.AddRange(new[] { ArsenalSkill1, ArsenalSkill2, ArsenalSkill3, ArsenalSkill4, ArsenalSkill5, ArsenalSkill6, ArsenalSkill7, ArsenalSkill8, ArsenalSkill9, ArsenalSkill10, ArsenalSkill11, ArsenalSkill12, ArsenalSkill13, ArsenalSkill14, ArsenalSkill15, ArsenalSkill16, ArsenalSkill17, ArsenalSkill18, ArsenalSkill19, ArsenalSkill20, ArsenalSkill21, ArsenalSkill22, ArsenalSkill23, ArsenalSkill24, ArsenalSkill25, ArsenalSkill26, ArsenalSkill27, ArsenalSkill28, ArsenalSkill29, ArsenalSkill30 });

            InitializeArsenalList();

            ArsenalFilterTextBox.TextChanged += ArsenalFilterTextBox_TextChanged;
        }

        private void ArsenalFilterTextBox_TextChanged(object? sender, EventArgs e)
        {
            var searchTerm = ArsenalFilterTextBox.Text;
            if (searchTerm.Length == 0)
            {
                foreach (var row in _arsenalRows)
                {
                    ArsenalList.RowStyles[row.Key].SizeType = SizeType.AutoSize;
                }
                return;
            }

            foreach (var row in _arsenalRows)
            {
                if (row.Value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    ArsenalList.RowStyles[row.Key].SizeType = SizeType.AutoSize;
                }
                else
                {
                    ArsenalList.RowStyles[row.Key].SizeType = SizeType.Absolute; // Since height is zero, this hides the row
                }
            }
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

        private List<ArsenalListItem> _arsenalListItems = new List<ArsenalListItem>();

        private void InitializeArsenalList()
        {
            var arsenalNames = GetPdHelperArsenals();

            foreach (var arsenalName in arsenalNames)
            {
                var arsenal = _arsenalService.LoadArsenal(arsenalName);

                var arsenalListItem = new ArsenalListItem(arsenalName);
                _arsenalListItems.Add(arsenalListItem);

                var cards = _arsenalService.SortCards(arsenal.Cards.ToList());

                var schools = arsenal.Schools;

                int schoolCount = schools.Count();

                string skillsOverAura = $"{cards.Where(c => c.TYPE != "Aura").Count()}/30";

                arsenalListItem.MouseEnter += (object? sender, EventArgs e) =>
                {
                    if (_currentArsenalListItem != arsenalListItem)
                    {
                        _currentArsenalListItem?.SetInactiveColors();
                        _currentArsenalListItem = arsenalListItem;
                        arsenalListItem.SetActiveColors();
                    }

                    RenderArsenal(arsenalListItem.ArsenalName, cards, schools, schoolCount, skillsOverAura);
                };

                AddItem(ArsenalList, arsenalListItem, arsenalName);
            }
        }

        private void AddItem(TableLayoutPanel panel, ArsenalListItem arsenalListItem, string arsenalName)
        {
            // Get a reference to the previous existent 
            RowStyle temp = panel.RowStyles[panel.RowCount - 1];

            // Increase panel rows count by one
            panel.RowCount++;

            // Add a new RowStyle as a copy of the previous one
            panel.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));

            // Add your control
            panel.Controls.Add(arsenalListItem, 0, panel.RowCount - 1);

            // Add the arsenal name with it's row index so we can use it for filtering.
            _arsenalRows.Add(panel.RowCount - 1, arsenalName);
        }

        private void RenderArsenal(string arsenalName, List<PDCard> cards, IEnumerable<string> schools, int schoolCount, string skillsOverAura)
        {
            if (!ArsenalPanel.Visible)
            {
                ArsenalPanel.Visible = true;
            }

            if (schoolCount == 0)
            {
                schoolCount = 1; // Sometimes arsenals are all Aura, so set the school count to 1 minimum.
            }

            ArsenalCasePicture.Image = AppImages.GetArsenalCase(schoolCount);
            ArsenalNameLabel.Text = arsenalName;

            foreach (KeyValuePair<string, PictureBox> schoolPicture in _schoolPictures)
            {
                if (schools.Contains(schoolPicture.Key))
                {
                    schoolPicture.Value.Image = AppImages.GetSchool(schoolPicture.Key);
                    schoolPicture.Value.BackColor = Color.FromArgb(92, 172, 149);
                }
                else
                {
                    schoolPicture.Value.Image = null;
                    schoolPicture.Value.BackColor = Color.Transparent;
                }
            }

            SkillsOverAuraLabel.Text = skillsOverAura;

            for (int i = 0; i < 30; i++)
            {
                var card = cards[i];
                var skill = _arsenalSkills[i];
                skill.Text = card.NAME;
                skill.BackColor = AppColors.GetSkillColor(card.TYPE);
                skill.Image = AppImages.GetSchool(card.SCHOOL);
                skill.MouseEnter += (object? sender, EventArgs e) => RenderCard(card);
            }
        }

        private void RenderCard(PDCard card)
        {
            CardTitlePanel.BackColor = AppColors.GetSkillColor(card.TYPE);
            CardSchoolPicture.Image = AppImages.GetSchool(card.SCHOOL);
            CardTitleLabel.Text = card.NAME;
            CardSubtitleLabel.Text = $"COST {card.COST} STR {card.DAMAGE} @ {card.RANGE}";
            CardDescriptionLabel.Text = card.DESCRIPTION;
        }

        /// <summary>
        /// Gets arsenal names for locally saved arsenal files
        /// </summary>
        private IEnumerable<string> GetPdHelperArsenals()
        {
            DirectoryInfo directory = new DirectoryInfo(@"Arsenals\"); //Assuming Test is your Folder

            FileInfo[] Files = directory.GetFiles("*.arsenal"); //Getting Text files

            return Files.Select(f => Path.GetFileNameWithoutExtension(f.Name));
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

            ArsenalList.Controls.Clear();
            _arsenalListItems.Clear();
            InitializeArsenalList();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            string arsenalName = PromptForm.Show("Enter new arsenal name").Trim();
            if (arsenalName.Length == 0)
            {
                return;
            }

            _arsenalService.Create(arsenalName);

            var arsenal = _arsenalService.LoadArsenal(arsenalName);

            var arsenalListItem = new ArsenalListItem(arsenalName);
            _arsenalListItems.Add(arsenalListItem);

            var cards = _arsenalService.SortCards(arsenal.Cards.ToList());

            var schools = arsenal.Schools;

            int schoolCount = schools.Count();

            string skillsOverAura = $"{cards.Where(c => c.TYPE != "Aura").Count()}/30";

            arsenalListItem.MouseEnter += (object? sender, EventArgs e) =>
            {
                if (_currentArsenalListItem != arsenalListItem)
                {
                    _currentArsenalListItem?.SetInactiveColors();
                    _currentArsenalListItem = arsenalListItem;
                    arsenalListItem.SetActiveColors();
                }

                RenderArsenal(arsenalListItem.ArsenalName, cards, schools, schoolCount, skillsOverAura);
            };

            AddItem(ArsenalList, arsenalListItem, arsenalName);
        }
    }
}
