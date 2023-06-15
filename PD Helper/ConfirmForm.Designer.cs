namespace PD_Helper
{
    partial class ConfirmForm
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
            this.PromptLabel = new System.Windows.Forms.Label();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.AcceptButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PromptLabel
            // 
            this.PromptLabel.AutoSize = true;
            this.PromptLabel.Location = new System.Drawing.Point(25, 23);
            this.PromptLabel.Name = "PromptLabel";
            this.PromptLabel.Size = new System.Drawing.Size(38, 15);
            this.PromptLabel.TabIndex = 0;
            this.PromptLabel.Text = "label1";
            // 
            // CancelButton1
            // 
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(188, 88);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(75, 23);
            this.CancelButton1.TabIndex = 1;
            this.CancelButton1.Text = "Cancel";
            this.CancelButton1.UseVisualStyleBackColor = true;
            // 
            // AcceptButton1
            // 
            this.AcceptButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptButton1.Location = new System.Drawing.Point(269, 88);
            this.AcceptButton1.Name = "AcceptButton1";
            this.AcceptButton1.Size = new System.Drawing.Size(75, 23);
            this.AcceptButton1.TabIndex = 2;
            this.AcceptButton1.Text = "Ok";
            this.AcceptButton1.UseVisualStyleBackColor = true;
            // 
            // ConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 123);
            this.Controls.Add(this.AcceptButton1);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.PromptLabel);
            this.Name = "ConfirmForm";
            this.Text = "ConfirmForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label PromptLabel;
        private Button CancelButton1;
        private Button AcceptButton1;
    }
}