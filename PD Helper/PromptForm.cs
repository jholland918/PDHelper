namespace PD_Helper
{
    public partial class PromptForm : Form
    {
        public PromptForm()
        {
            InitializeComponent();
        }

        public static string Show(string promptText, string inputValue = null)
        {
            var form = new PromptForm()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen
            };

            if (inputValue != null || inputValue.Length > 0 )
            {
                form.InputTextBox.Text = inputValue;
            }

            form.PromptLabel.Text = promptText;
            form.AcceptButton = form.AcceptButton1;
            form.CancelButton = form.CancelButton1;
            var dialogResult = form.ShowDialog();
            form.Close();

            return dialogResult == DialogResult.OK ? form.InputTextBox.Text : "";
        }
    }
}
