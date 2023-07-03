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

namespace PD_Helper
{
    public partial class ArsenalRandomizerForm : Form
    {
        private Dictionary<object, bool> _caseButtons;
        private Dictionary<object, bool> _schoolButtons;

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

        }
    }
}
