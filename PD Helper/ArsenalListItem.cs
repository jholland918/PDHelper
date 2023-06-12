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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PD_Helper
{
    public partial class ArsenalListItem : UserControl
    {
        private readonly ArsenalService _arsenalService = new();
        private readonly GameProfileService _gameProfileService = new();
        private static Dictionary<string, PictureBox> SchoolPictures = new Dictionary<string, PictureBox>();

        private readonly Color BackgroundColor = Color.FromArgb(33, 53, 47);
        private readonly Color BackgroundColorHover = Color.FromArgb(67, 11, 15);
        private readonly Color ForegroundColor = Color.FromArgb(92, 172, 149);
        private readonly Color ForegroundColorHover = Color.FromArgb(216, 185, 24);

        private bool IsActive = false;

        public ArsenalListItem(string arsenalName)
            : this()
        {
            Initialize(arsenalName);
        }

        public ArsenalListItem()
        {
            InitializeComponent();
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

        private void Initialize(string arsenalName)
        {
            InitArsenalSlotCombo();

            SaveButton.Click += SaveButton_Click;

            foreach (Control control in Controls)
            {
                control.MouseEnter += RedirectMouseEnter;
            }

            InitializeSchoolPictures();
            var arsenal = _arsenalService.LoadArsenal(arsenalName);
            var schools = arsenal.Cards.Select(c => c.SCHOOL).Distinct();
            int schoolCount = schools.Count();
            string skillsOverAura = $"{arsenal.Cards.Where(c => c.TYPE != "Aura").Count()}/30";
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

        private void InitArsenalSlotCombo()
        {
            ArsenalSlotCombo.SelectedItem = "ARSENAL01";
            ArsenalSlotCombo.SelectedText = "ARSENAL01";
            ArsenalSlotCombo.DrawItem += (object? sender, DrawItemEventArgs e) =>
            {
                int index = e.Index >= 0 ? e.Index : 0;
                var brush = new SolidBrush(Color.FromArgb(92, 172, 149));
                e.DrawBackground();
                e.Graphics.DrawString(ArsenalSlotCombo.Items[index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            };
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            int selectedIndex = ArsenalSlotCombo.SelectedIndex;

            // Read test
            //var profile = _gameProfileService.LoadGameProfile();
            //var arsenal = _gameProfileService.ReadArsenal(profile, selectedIndex);
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
    }
}
