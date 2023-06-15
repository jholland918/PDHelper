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
    public partial class ConfirmForm : Form
    {
        public ConfirmForm()
        {
            InitializeComponent();
        }

        public static bool Show(string promptText)
        {
            var form = new ConfirmForm()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen
            };

            form.PromptLabel.Text = promptText;
            form.AcceptButton = form.AcceptButton1;
            form.CancelButton = form.CancelButton1;
            var dialogResult = form.ShowDialog();
            form.Close();

            return dialogResult == DialogResult.OK;
        }
    }
}
