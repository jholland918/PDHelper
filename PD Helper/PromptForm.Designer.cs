namespace PD_Helper
{
    partial class PromptForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPanel = new System.Windows.Forms.Panel();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.AcceptButton1 = new System.Windows.Forms.Button();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.PromptLabel = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.CancelButton1);
            this.MainPanel.Controls.Add(this.AcceptButton1);
            this.MainPanel.Controls.Add(this.InputTextBox);
            this.MainPanel.Controls.Add(this.PromptLabel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(357, 121);
            this.MainPanel.TabIndex = 0;
            // 
            // CancelButton1
            // 
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(170, 70);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(75, 23);
            this.CancelButton1.TabIndex = 3;
            this.CancelButton1.Text = "Cancel";
            this.CancelButton1.UseVisualStyleBackColor = true;
            // 
            // AcceptButton1
            // 
            this.AcceptButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptButton1.Location = new System.Drawing.Point(251, 70);
            this.AcceptButton1.Name = "AcceptButton1";
            this.AcceptButton1.Size = new System.Drawing.Size(75, 23);
            this.AcceptButton1.TabIndex = 2;
            this.AcceptButton1.Text = "Ok";
            this.AcceptButton1.UseVisualStyleBackColor = true;
            // 
            // InputTextBox
            // 
            this.InputTextBox.Location = new System.Drawing.Point(25, 41);
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(301, 23);
            this.InputTextBox.TabIndex = 1;
            // 
            // PromptLabel
            // 
            this.PromptLabel.AutoSize = true;
            this.PromptLabel.Location = new System.Drawing.Point(25, 23);
            this.PromptLabel.Name = "PromptLabel";
            this.PromptLabel.Size = new System.Drawing.Size(57, 15);
            this.PromptLabel.TabIndex = 0;
            this.PromptLabel.Text = "Enter text";
            // 
            // PromptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 121);
            this.Controls.Add(this.MainPanel);
            this.Name = "PromptForm";
            this.Text = "PromptForm";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel MainPanel;
        private Button AcceptButton1;
        private TextBox InputTextBox;
        private Label PromptLabel;
        private Button CancelButton1;
    }
}