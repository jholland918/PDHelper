using PD_Helper.Library;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace PD_Helper
{
    public partial class ArsenalListItem : UserControl
    {
        private Arsenal _arsenal;
        private readonly ArsenalService _arsenalService = new();
        private readonly GameService _gameService = new();
        private static Dictionary<string, PictureBox> SchoolPictures = new Dictionary<string, PictureBox>();

        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripitem?view=windowsdesktop-7.0#remarks
        private readonly ContextMenuStrip _saveDropdownStrip = new ContextMenuStrip
        {
            ShowCheckMargin = false,
            ShowImageMargin = false,
        };

        private bool IsActive = false;

        public Arsenal Arsenal { get { return _arsenal; } }

        public string ArsenalName
        {
            get
            {
                return _arsenal.ArsenalName;
            }

            set
            {
                _arsenal.ArsenalName = value;
                ArsenalNameLabel.Text = value;
            }
        }

        public ArsenalListItem(Arsenal arsenal)
            : this()
        {
            Initialize(arsenal);
        }

        public ArsenalListItem()
        {
            InitializeComponent();
        }

        private void Initialize(Arsenal arsenal)
        {
            var toolTips = new System.Windows.Forms.ToolTip();
            toolTips.SetToolTip(SaveButton, "Save to game at selected slot");

            _arsenal = arsenal;

            for (int i = 1; i <= 16; i++)
            {
                var button = new ToolStripButton($"ARSENAL {i.ToString().PadLeft(2, '0')}");
                // Due to scoping behavior, we must copy the i value to a locally scoped variable (indexCopy) to retain the current value passed to the click handler.
                int indexCopy = i;
                button.Click += (s, e) => SaveArsenalToGame(indexCopy);
                _saveDropdownStrip.Items.Add(button);
            }

            SaveDropdownMenu.Click += SaveDropdownMenu_Click;
            SaveButton.Text = $"ARSENAL {_saveArsenalIndex.ToString().PadLeft(2, '0')}";
            SaveButton.Click += (s, e) => SaveArsenalToGame();

            foreach (Control control in Controls)
            {
                control.MouseEnter += RedirectMouseEnter;
            }

            InitializeSchoolPictures();
            var schools = _arsenal.Schools;
            int schoolCount = schools.Count();
            if (schoolCount == 0)
            {
                schoolCount = 1; // Sometimes arsenals are all Aura, so set the school count to 1 minimum.
            }
            string skillsOverAura = $"{_arsenal.Cards.Where(c => c.Type != "Aura").Count()}/30";
            ArsenalCasePicture.Image = AppImages.GetArsenalCase(schoolCount);
            ArsenalNameLabel.Text = _arsenal.ArsenalName;
            foreach (KeyValuePair<string, PictureBox> schoolPicture in SchoolPictures)
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
        }


        private int _saveArsenalIndex = 1;

        private void SaveDropdownMenu_Click(object? sender, EventArgs e)
        {
            _saveDropdownStrip.Show(SaveDropdownMenu, new Point(0, SaveDropdownMenu.Height));
        }

        private void SaveArsenalToGame(int? arsenalIndex = null)
        {
            if (arsenalIndex != null)
            {
                _saveArsenalIndex = arsenalIndex.Value;
                SaveButton.Text = $"ARSENAL {_saveArsenalIndex.ToString().PadLeft(2, '0')}";
            }

            Debug.WriteLine($"Saving [{_arsenal.ArsenalName}] to index [{_saveArsenalIndex})");

            _arsenal.ArsenalIndex = _saveArsenalIndex - 1;
            _gameService.WriteArsenal(_arsenal);
        }

        private void InitializeSchoolPictures()
        {
            SchoolPicturePsycho.Image = AppImages.Psycho;
            SchoolPictureOptical.Image = AppImages.Optical;
            SchoolPictureNature.Image = AppImages.Nature;
            SchoolPictureKi.Image = AppImages.Ki;
            SchoolPictureFaith.Image = AppImages.Faith;

            SchoolPictures = new Dictionary<string, PictureBox>
            {
                ["Faith"] = SchoolPictureFaith,
                ["Ki"] = SchoolPictureKi,
                ["Nature"] = SchoolPictureNature,
                ["Optical"] = SchoolPictureOptical,
                ["Psycho"] = SchoolPicturePsycho,
            };
        }

        public void SetActiveColors()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;

            ArsenalNameLabel.ForeColor = AppColors.ForegroundColorHover;
            SkillsOverAuraLabel.ForeColor = AppColors.ForegroundColorHover;
            SaveButton.ForeColor = AppColors.ForegroundColorHover;
            SaveDropdownMenu.ForeColor = AppColors.ForegroundColorHover;
            MainContainer.BackColor = AppColors.BackgroundColorHover;
            ArsenalCasePicture.BackColor = AppColors.ForegroundColorHover;

            SchoolPicturePsycho.BackColor = SchoolPicturePsycho.Image == null ? Color.Transparent : AppColors.ForegroundColorHover;
            SchoolPictureOptical.BackColor = SchoolPictureOptical.Image == null ? Color.Transparent : AppColors.ForegroundColorHover;
            SchoolPictureNature.BackColor = SchoolPictureNature.Image == null ? Color.Transparent : AppColors.ForegroundColorHover;
            SchoolPictureKi.BackColor = SchoolPictureKi.Image == null ? Color.Transparent : AppColors.ForegroundColorHover;
            SchoolPictureFaith.BackColor = SchoolPictureFaith.Image == null ? Color.Transparent : AppColors.ForegroundColorHover;

        }

        public void SetInactiveColors()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;

            ArsenalNameLabel.ForeColor = AppColors.ForegroundColor;
            SkillsOverAuraLabel.ForeColor = AppColors.ForegroundColor;
            SaveButton.ForeColor = AppColors.ForegroundColor;
            SaveDropdownMenu.ForeColor = AppColors.ForegroundColor;
            MainContainer.BackColor = AppColors.BackgroundColor;
            ArsenalCasePicture.BackColor = AppColors.ForegroundColor;

            SchoolPicturePsycho.BackColor = SchoolPicturePsycho.Image == null ? Color.Transparent : AppColors.ForegroundColor;
            SchoolPictureOptical.BackColor = SchoolPictureOptical.Image == null ? Color.Transparent : AppColors.ForegroundColor;
            SchoolPictureNature.BackColor = SchoolPictureNature.Image == null ? Color.Transparent : AppColors.ForegroundColor;
            SchoolPictureKi.BackColor = SchoolPictureKi.Image == null ? Color.Transparent : AppColors.ForegroundColor;
            SchoolPictureFaith.BackColor = SchoolPictureFaith.Image == null ? Color.Transparent : AppColors.ForegroundColor;
        }

        private void RedirectMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        public override string ToString()
        {
            return _arsenal.ArsenalName;
        }
    }
}
