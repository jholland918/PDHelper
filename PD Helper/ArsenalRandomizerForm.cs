using PD_Helper.Library;
using PD_Helper.Library.ArsenalGeneration;
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
    public partial class ArsenalRandomizerForm : Form
    {
        private ArsenalGenerator _arsenalGenerator = new ArsenalGenerator();
        private Dictionary<object, bool> _caseButtons;
        private Dictionary<object, bool> _schoolButtons;

        /// <summary>
        /// The arsenal generated from the randomizer
        /// </summary>
        public List<PDCard> Arsenal { get; private set; }

        public ArsenalRandomizerForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _caseButtons = new Dictionary<object, bool>
             {
                [OneSchoolCaseButton] = true,
                [TwoSchoolCaseButton] = true,
                [ThreeSchoolCaseButton] = true,
            };

            _schoolButtons = new Dictionary<object, bool>
            {
                [PsychoSchoolButton] = true,
                [OpticalSchoolButton] = true,
                [NatureSchoolButton] = true,
                [KiSchoolButton] = true,
                [FaithSchoolButton] = true,
            };

            StyleButtons(_caseButtons);
            StyleButtons(_schoolButtons);
        }

        private void CaseButton_Click(object sender, EventArgs e)
        {
            SelectButtons(sender, _caseButtons);
            StyleButtons(_caseButtons);
        }

        private void SchoolButton_Click(object sender, EventArgs e)
        {
            SelectButtons(sender, _schoolButtons);
            StyleButtons(_schoolButtons);
        }

        private void SelectButtons(object sender, Dictionary<object, bool> buttons)
        {
            buttons[sender] = !buttons[sender];

            if (buttons.All(kvp => kvp.Value == false))
            {
                foreach (var key in buttons.Keys)
                {
                    if (key != sender)
                    {
                        buttons[key] = true;
                    }
                }
            }
        }

        private void StyleButtons(Dictionary<object, bool> buttons)
        {
            foreach (var kvp in buttons)
            {
                var button = (Button)kvp.Key;

                if (buttons[kvp.Key])
                {
                    button.BackColor = AppColors.ForegroundColor;
                }
                else
                {
                    button.BackColor = AppColors.BackgroundColorMedium;
                }
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var caseSizes = new List<int>();
            if (_caseButtons[OneSchoolCaseButton])
            {
                caseSizes.Add(1);
            }
            if (_caseButtons[TwoSchoolCaseButton])
            {
                caseSizes.Add(2);
            }
            if (_caseButtons[ThreeSchoolCaseButton])
            {
                caseSizes.Add(3);
            }

            var schools = new List<string>();
            if (_schoolButtons[PsychoSchoolButton])
            {
                schools.Add("Psycho");
            }
            if (_schoolButtons[OpticalSchoolButton])
            {
                schools.Add("Optical");
            }
            if (_schoolButtons[NatureSchoolButton])
            {
                schools.Add("Nature");
            }
            if (_schoolButtons[KiSchoolButton])
            {
                schools.Add("Ki");
            }
            if (_schoolButtons[FaithSchoolButton])
            {
                schools.Add("Faith");
            }

            var typeMinimums = new Dictionary<string, int>
            {
                ["Aura"] = (int)AuraMin.Value,
                ["Attack"] = (int)AttackMin.Value,
                ["Defense"] = (int)DefenseMin.Value,
                ["Erase"] = (int)EraseMin.Value,
                ["Status"] = (int)StatusMin.Value,
                ["Special"] = (int)SpecialMin.Value,
                ["Environment"] = (int)EnvironmentMin.Value,
            };

            var typeMaximums = new Dictionary<string, int>
            {
                ["Aura"] = (int)AuraMax.Value,
                ["Attack"] = (int)AttackMax.Value,
                ["Defense"] = (int)DefenseMax.Value,
                ["Erase"] = (int)EraseMax.Value,
                ["Status"] = (int)StatusMax.Value,
                ["Special"] = (int)SpecialMax.Value,
                ["Environment"] = (int)EnvironmentMax.Value,
            };

            var attackRanges = new List<string>();
            if (AllAttackRangeCheckbox.Checked)
            {
                attackRanges.Add("all");
            }

            if (MineAttackRangeCheckbox.Checked)
            {
                attackRanges.Add("mine");
            }

            if (ShortAttackRangeCheckbox.Checked)
            {
                attackRanges.Add("short");
            }

            if (MediumAttackRangeCheckbox.Checked)
            {
                attackRanges.Add("medium");
            }

            if (LongAttackRangeCheckbox.Checked)
            {
                attackRanges.Add("long");
            }

            var options = new GeneratorOptions
            {
                CaseSizes = caseSizes,
                Schools = schools,
                TypeMinimums = typeMinimums,
                TypeMaximums = typeMaximums,
                AttackRanges = attackRanges,
            };

            Arsenal = _arsenalGenerator.Execute(options);

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
