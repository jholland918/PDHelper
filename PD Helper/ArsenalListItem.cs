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

        private readonly Color BackgroundColor = Color.FromArgb(33, 53, 47);
        private readonly Color BackgroundColorHover = Color.FromArgb(67, 11, 15);
        private readonly Color ForegroundColor = Color.FromArgb(92, 172, 149);
        private readonly Color ForegroundColorHover = Color.FromArgb(216, 185, 24);
        private bool IsActive = false;

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

        public ArsenalListItem(string arsenalName)
            : this()
        {
            Initialize(arsenalName);
        }

        public ArsenalListItem()
        {
            InitializeComponent();
        }

        private void Initialize(string arsenalName)
        {
            for (int i = 1; i<= 16; i++)
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
            _arsenal = _arsenalService.LoadArsenal(arsenalName);
            var schools = _arsenal.Schools;
            int schoolCount = schools.Count();
            string skillsOverAura = $"{_arsenal.Cards.Where(c => c.TYPE != "Aura").Count()}/30";
            ArsenalCasePicture.Image = AppImages.GetArsenalCase(schoolCount);
            ArsenalNameLabel.Text = arsenalName;
            foreach (KeyValuePair<string, PictureBox> schoolPicture in SchoolPictures)
            {
                if (schools.Contains(schoolPicture.Key))
                {
                    schoolPicture.Value.Show();
                }
                else
                {
                    schoolPicture.Value.Hide();
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

            ArsenalNameLabel.ForeColor = ForegroundColorHover;
            SkillsOverAuraLabel.ForeColor = ForegroundColorHover;
            SaveButton.ForeColor = ForegroundColorHover;
            SaveDropdownMenu.ForeColor = ForegroundColorHover;

            ContainerTable.BackColor = BackgroundColorHover;
            ArsenalCasePicture.BackColor = ForegroundColorHover;
            SchoolPicturePsycho.BackColor = ForegroundColorHover;
            SchoolPictureOptical.BackColor = ForegroundColorHover;
            SchoolPictureNature.BackColor = ForegroundColorHover;
            SchoolPictureKi.BackColor = ForegroundColorHover;
            SchoolPictureFaith.BackColor = ForegroundColorHover;

        }

        public void SetInactiveColors()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;

            ArsenalNameLabel.ForeColor = ForegroundColor;
            SkillsOverAuraLabel.ForeColor = ForegroundColor;
            SaveButton.ForeColor = ForegroundColor;
            SaveDropdownMenu.ForeColor = ForegroundColor;

            ContainerTable.BackColor = BackgroundColor;
            ArsenalCasePicture.BackColor = ForegroundColor;
            SchoolPicturePsycho.BackColor = ForegroundColor;
            SchoolPictureOptical.BackColor = ForegroundColor;
            SchoolPictureNature.BackColor = ForegroundColor;
            SchoolPictureKi.BackColor = ForegroundColor;
            SchoolPictureFaith.BackColor = ForegroundColor;
        }

        private void RedirectMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }
    }
}
