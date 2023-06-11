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
using System.Xml.Linq;
using static PD_Helper.Form1;

namespace PD_Helper
{
    public partial class LabForm : Form
    {
        private readonly ArsenalService _arsenalService = new ArsenalService();
        private readonly List<Button> _arsenalSkills = new List<Button>();

        public LabForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            SchoolPicture1.Image = AppImages.Psycho;
            SchoolPicture2.Image = AppImages.Optical;
            SchoolPicture3.Image = AppImages.Nature;
            SchoolPicture4.Image = AppImages.Ki;
            SchoolPicture5.Image = AppImages.Faith;

            _arsenalSkills.AddRange(new[] { ArsenalSkill1, ArsenalSkill2, ArsenalSkill3, ArsenalSkill4, ArsenalSkill5, ArsenalSkill6, ArsenalSkill7, ArsenalSkill8, ArsenalSkill9, ArsenalSkill10, ArsenalSkill11, ArsenalSkill12, ArsenalSkill13, ArsenalSkill14, ArsenalSkill15, ArsenalSkill16, ArsenalSkill17, ArsenalSkill18, ArsenalSkill19, ArsenalSkill20, ArsenalSkill21, ArsenalSkill22, ArsenalSkill23, ArsenalSkill24, ArsenalSkill25, ArsenalSkill26, ArsenalSkill27, ArsenalSkill28, ArsenalSkill29, ArsenalSkill30 });

            var arsenalNames = GetPdHelperArsenals();

            foreach (var arsenalName in arsenalNames)
            {
                var arsenal = _arsenalService.LoadArsenal(arsenalName);
                var cards = new List<PDCard>();

                // Fill with arsenal cards
                foreach (var card in arsenal.Cards)
                {
                    cards.Add(card);
                }

                // Fill out the rest of the arsenal with Aura...
                for (int i = cards.Count; i < 30; i++)
                {
                    cards.Add(new PDCard
                    {
                        NAME = "Aura"
                    });
                }

                var button = new Button
                {
                    Name = arsenalName,
                    Text = arsenalName,
                };

                var schools = cards.Select(c => c.SCHOOL).Distinct();

                int schoolCount = schools.Count();

                string skillsOverAura = $"{cards.Where(c => c.TYPE != "Aura").Count()}/30";

                button.MouseEnter += (object? sender, EventArgs e) =>
                {
                    switch (schoolCount)
                    {
                        case 1:
                            ArsenalCasePicture.Image = AppImages.ArsenalCase1;
                            break;
                        case 2:
                            ArsenalCasePicture.Image = AppImages.ArsenalCase2;
                            break;
                        case 3:
                            ArsenalCasePicture.Image = AppImages.ArsenalCase3;
                            break;
                    }

                    ArsenalNameLabel.Text = arsenalName;

                    SchoolPicture1.Hide();
                    SchoolPicture2.Hide();
                    SchoolPicture3.Hide();
                    SchoolPicture4.Hide();
                    SchoolPicture5.Hide();

                    foreach (var school in schools)
                    {
                        switch (school)
                        {
                            case "Faith":
                                SchoolPicture5.Show();
                                break;
                            case "Ki":
                                SchoolPicture4.Show();
                                break;
                            case "Nature":
                                SchoolPicture3.Show();
                                break;
                            case "Optical":
                                SchoolPicture2.Show();
                                break;
                            case "Psycho":
                                SchoolPicture1.Show();
                                break;
                        }
                    }

                    SkillsOverAuraLabel.Text = skillsOverAura;

                    for (int i = 0; i < 30; i++)
                    {
                        var card = cards[i];
                        var skill = _arsenalSkills[i];

                        skill.Text = card.NAME;

                        switch (card.TYPE)
                        {
                            case "Attack":
                                skill.BackColor = AppColors.Attack;
                                break;
                            case "Defense":
                                skill.BackColor = AppColors.Defense;
                                break;
                            case "Environment":
                                skill.BackColor = AppColors.Environment;
                                break;
                            case "Special":
                                skill.BackColor = AppColors.Special;
                                break;
                            case "Status":
                                skill.BackColor = AppColors.Status;
                                break;
                            case "Erase":
                                skill.BackColor = AppColors.Erase;
                                break;
                            case "Aura":
                                skill.BackColor = AppColors.Aura;
                                break;
                            default:
                                throw new Exception(card.TYPE);
                        }

                        switch (card.SCHOOL)
                        {
                            case "Aura":
                                skill.Image = AppImages.Aura;
                                break;
                            case "Faith":
                                skill.Image = AppImages.Faith;
                                break;
                            case "Ki":
                                skill.Image = AppImages.Ki;
                                break;
                            case "Nature":
                                skill.Image = AppImages.Nature;
                                break;
                            case "Optical":
                                skill.Image = AppImages.Optical;
                                break;
                            case "Psycho":
                                skill.Image = AppImages.Psycho;
                                break;
                            default:
                                throw new Exception(card.SCHOOL);
                        }

                        skill.MouseEnter += (object? sender, EventArgs e) =>
                        {
                            CardTitleLabel.Text = card.NAME;
                            CardSubtitleLabel.Text = $"COST {card.COST} STR {card.DAMAGE} @ {card.RANGE}";
                            CardDescriptionLabel.Text = card.DESCRIPTION;
                        };
                    }
                };

                ArsenalListPanel.Controls.Add(button);
            }
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

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
