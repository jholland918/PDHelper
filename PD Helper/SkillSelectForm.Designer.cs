namespace PD_Helper
{
    partial class SkillSelectForm
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
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Header = new System.Windows.Forms.Panel();
            this.EnvironmentalCheckBox = new System.Windows.Forms.CheckBox();
            this.SpecialCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusCheckBox = new System.Windows.Forms.CheckBox();
            this.EraseCheckBox = new System.Windows.Forms.CheckBox();
            this.DefenseCheckBox = new System.Windows.Forms.CheckBox();
            this.AttackCheckBox = new System.Windows.Forms.CheckBox();
            this.AuraCheckBox = new System.Windows.Forms.CheckBox();
            this.FaithCheckBox = new System.Windows.Forms.CheckBox();
            this.KiCheckBox = new System.Windows.Forms.CheckBox();
            this.NatureCheckBox = new System.Windows.Forms.CheckBox();
            this.OpticalCheckBox = new System.Windows.Forms.CheckBox();
            this.PsychoCheckBox = new System.Windows.Forms.CheckBox();
            this.Subheader = new System.Windows.Forms.Panel();
            this.SortThenBy2Label = new System.Windows.Forms.Label();
            this.SortThenBy1Label = new System.Windows.Forms.Label();
            this.SortComboBox1 = new System.Windows.Forms.ComboBox();
            this.SortLabel = new System.Windows.Forms.Label();
            this.SortComboBox2 = new System.Windows.Forms.ComboBox();
            this.SortComboBox3 = new System.Windows.Forms.ComboBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SkillList = new System.Windows.Forms.FlowLayoutPanel();
            this.Footer = new System.Windows.Forms.Panel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.ConfirmDropdown = new System.Windows.Forms.Button();
            this.SkillDetail = new System.Windows.Forms.Panel();
            this.CardDescriptionPanel = new System.Windows.Forms.Panel();
            this.CardDescription = new System.Windows.Forms.Label();
            this.CardSubheaderPanel = new System.Windows.Forms.Panel();
            this.CardSubtitle = new System.Windows.Forms.Label();
            this.CardHeaderPanel = new System.Windows.Forms.Panel();
            this.CardTitle = new System.Windows.Forms.Label();
            this.MainLayout.SuspendLayout();
            this.Header.SuspendLayout();
            this.Subheader.SuspendLayout();
            this.Footer.SuspendLayout();
            this.SkillDetail.SuspendLayout();
            this.CardDescriptionPanel.SuspendLayout();
            this.CardSubheaderPanel.SuspendLayout();
            this.CardHeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayout
            // 
            this.MainLayout.ColumnCount = 2;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayout.Controls.Add(this.Header, 0, 0);
            this.MainLayout.Controls.Add(this.Subheader, 0, 1);
            this.MainLayout.Controls.Add(this.SkillList, 0, 2);
            this.MainLayout.Controls.Add(this.Footer, 0, 3);
            this.MainLayout.Controls.Add(this.SkillDetail, 1, 2);
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 4;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.MainLayout.Size = new System.Drawing.Size(674, 450);
            this.MainLayout.TabIndex = 0;
            // 
            // Header
            // 
            this.MainLayout.SetColumnSpan(this.Header, 2);
            this.Header.Controls.Add(this.EnvironmentalCheckBox);
            this.Header.Controls.Add(this.SpecialCheckBox);
            this.Header.Controls.Add(this.StatusCheckBox);
            this.Header.Controls.Add(this.EraseCheckBox);
            this.Header.Controls.Add(this.DefenseCheckBox);
            this.Header.Controls.Add(this.AttackCheckBox);
            this.Header.Controls.Add(this.AuraCheckBox);
            this.Header.Controls.Add(this.FaithCheckBox);
            this.Header.Controls.Add(this.KiCheckBox);
            this.Header.Controls.Add(this.NatureCheckBox);
            this.Header.Controls.Add(this.OpticalCheckBox);
            this.Header.Controls.Add(this.PsychoCheckBox);
            this.Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Header.Location = new System.Drawing.Point(3, 3);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(668, 69);
            this.Header.TabIndex = 0;
            // 
            // EnvironmentalCheckBox
            // 
            this.EnvironmentalCheckBox.AutoSize = true;
            this.EnvironmentalCheckBox.Checked = true;
            this.EnvironmentalCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnvironmentalCheckBox.Location = new System.Drawing.Point(539, 43);
            this.EnvironmentalCheckBox.Name = "EnvironmentalCheckBox";
            this.EnvironmentalCheckBox.Size = new System.Drawing.Size(103, 19);
            this.EnvironmentalCheckBox.TabIndex = 18;
            this.EnvironmentalCheckBox.Text = "Environmental";
            this.EnvironmentalCheckBox.UseVisualStyleBackColor = true;
            this.EnvironmentalCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // SpecialCheckBox
            // 
            this.SpecialCheckBox.AutoSize = true;
            this.SpecialCheckBox.Checked = true;
            this.SpecialCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SpecialCheckBox.Location = new System.Drawing.Point(458, 43);
            this.SpecialCheckBox.Name = "SpecialCheckBox";
            this.SpecialCheckBox.Size = new System.Drawing.Size(63, 19);
            this.SpecialCheckBox.TabIndex = 17;
            this.SpecialCheckBox.Text = "Special";
            this.SpecialCheckBox.UseVisualStyleBackColor = true;
            this.SpecialCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // StatusCheckBox
            // 
            this.StatusCheckBox.AutoSize = true;
            this.StatusCheckBox.Checked = true;
            this.StatusCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StatusCheckBox.Location = new System.Drawing.Point(377, 43);
            this.StatusCheckBox.Name = "StatusCheckBox";
            this.StatusCheckBox.Size = new System.Drawing.Size(58, 19);
            this.StatusCheckBox.TabIndex = 16;
            this.StatusCheckBox.Text = "Status";
            this.StatusCheckBox.UseVisualStyleBackColor = true;
            this.StatusCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // EraseCheckBox
            // 
            this.EraseCheckBox.AutoSize = true;
            this.EraseCheckBox.Checked = true;
            this.EraseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EraseCheckBox.Location = new System.Drawing.Point(296, 43);
            this.EraseCheckBox.Name = "EraseCheckBox";
            this.EraseCheckBox.Size = new System.Drawing.Size(53, 19);
            this.EraseCheckBox.TabIndex = 15;
            this.EraseCheckBox.Text = "Erase";
            this.EraseCheckBox.UseVisualStyleBackColor = true;
            this.EraseCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // DefenseCheckBox
            // 
            this.DefenseCheckBox.AutoSize = true;
            this.DefenseCheckBox.Checked = true;
            this.DefenseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DefenseCheckBox.Location = new System.Drawing.Point(215, 43);
            this.DefenseCheckBox.Name = "DefenseCheckBox";
            this.DefenseCheckBox.Size = new System.Drawing.Size(68, 19);
            this.DefenseCheckBox.TabIndex = 14;
            this.DefenseCheckBox.Text = "Defense";
            this.DefenseCheckBox.UseVisualStyleBackColor = true;
            this.DefenseCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // AttackCheckBox
            // 
            this.AttackCheckBox.AutoSize = true;
            this.AttackCheckBox.Checked = true;
            this.AttackCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AttackCheckBox.Location = new System.Drawing.Point(134, 43);
            this.AttackCheckBox.Name = "AttackCheckBox";
            this.AttackCheckBox.Size = new System.Drawing.Size(60, 19);
            this.AttackCheckBox.TabIndex = 13;
            this.AttackCheckBox.Text = "Attack";
            this.AttackCheckBox.UseVisualStyleBackColor = true;
            this.AttackCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // AuraCheckBox
            // 
            this.AuraCheckBox.AutoSize = true;
            this.AuraCheckBox.Checked = true;
            this.AuraCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AuraCheckBox.Location = new System.Drawing.Point(53, 43);
            this.AuraCheckBox.Name = "AuraCheckBox";
            this.AuraCheckBox.Size = new System.Drawing.Size(51, 19);
            this.AuraCheckBox.TabIndex = 12;
            this.AuraCheckBox.Text = "Aura";
            this.AuraCheckBox.UseVisualStyleBackColor = true;
            this.AuraCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // FaithCheckBox
            // 
            this.FaithCheckBox.AutoSize = true;
            this.FaithCheckBox.Checked = true;
            this.FaithCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FaithCheckBox.Location = new System.Drawing.Point(458, 12);
            this.FaithCheckBox.Name = "FaithCheckBox";
            this.FaithCheckBox.Size = new System.Drawing.Size(52, 19);
            this.FaithCheckBox.TabIndex = 11;
            this.FaithCheckBox.Text = "Faith";
            this.FaithCheckBox.UseVisualStyleBackColor = true;
            this.FaithCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // KiCheckBox
            // 
            this.KiCheckBox.AutoSize = true;
            this.KiCheckBox.Checked = true;
            this.KiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KiCheckBox.Location = new System.Drawing.Point(377, 12);
            this.KiCheckBox.Name = "KiCheckBox";
            this.KiCheckBox.Size = new System.Drawing.Size(36, 19);
            this.KiCheckBox.TabIndex = 10;
            this.KiCheckBox.Text = "Ki";
            this.KiCheckBox.UseVisualStyleBackColor = true;
            this.KiCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // NatureCheckBox
            // 
            this.NatureCheckBox.AutoSize = true;
            this.NatureCheckBox.Checked = true;
            this.NatureCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NatureCheckBox.Location = new System.Drawing.Point(296, 12);
            this.NatureCheckBox.Name = "NatureCheckBox";
            this.NatureCheckBox.Size = new System.Drawing.Size(62, 19);
            this.NatureCheckBox.TabIndex = 9;
            this.NatureCheckBox.Text = "Nature";
            this.NatureCheckBox.UseVisualStyleBackColor = true;
            this.NatureCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // OpticalCheckBox
            // 
            this.OpticalCheckBox.AutoSize = true;
            this.OpticalCheckBox.Checked = true;
            this.OpticalCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OpticalCheckBox.Location = new System.Drawing.Point(215, 12);
            this.OpticalCheckBox.Name = "OpticalCheckBox";
            this.OpticalCheckBox.Size = new System.Drawing.Size(64, 19);
            this.OpticalCheckBox.TabIndex = 8;
            this.OpticalCheckBox.Text = "Optical";
            this.OpticalCheckBox.UseVisualStyleBackColor = true;
            this.OpticalCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // PsychoCheckBox
            // 
            this.PsychoCheckBox.AutoSize = true;
            this.PsychoCheckBox.Checked = true;
            this.PsychoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PsychoCheckBox.Location = new System.Drawing.Point(134, 12);
            this.PsychoCheckBox.Name = "PsychoCheckBox";
            this.PsychoCheckBox.Size = new System.Drawing.Size(64, 19);
            this.PsychoCheckBox.TabIndex = 7;
            this.PsychoCheckBox.Text = "Psycho";
            this.PsychoCheckBox.UseVisualStyleBackColor = true;
            this.PsychoCheckBox.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);
            // 
            // Subheader
            // 
            this.MainLayout.SetColumnSpan(this.Subheader, 2);
            this.Subheader.Controls.Add(this.SortThenBy2Label);
            this.Subheader.Controls.Add(this.SortThenBy1Label);
            this.Subheader.Controls.Add(this.SortComboBox1);
            this.Subheader.Controls.Add(this.SortLabel);
            this.Subheader.Controls.Add(this.SortComboBox2);
            this.Subheader.Controls.Add(this.SortComboBox3);
            this.Subheader.Controls.Add(this.SearchLabel);
            this.Subheader.Controls.Add(this.SearchTextBox);
            this.Subheader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Subheader.Location = new System.Drawing.Point(3, 78);
            this.Subheader.Name = "Subheader";
            this.Subheader.Size = new System.Drawing.Size(668, 29);
            this.Subheader.TabIndex = 1;
            // 
            // SortThenBy2Label
            // 
            this.SortThenBy2Label.AutoSize = true;
            this.SortThenBy2Label.Location = new System.Drawing.Point(519, 6);
            this.SortThenBy2Label.Name = "SortThenBy2Label";
            this.SortThenBy2Label.Size = new System.Drawing.Size(15, 15);
            this.SortThenBy2Label.TabIndex = 7;
            this.SortThenBy2Label.Text = ">";
            // 
            // SortThenBy1Label
            // 
            this.SortThenBy1Label.AutoSize = true;
            this.SortThenBy1Label.Location = new System.Drawing.Point(374, 6);
            this.SortThenBy1Label.Name = "SortThenBy1Label";
            this.SortThenBy1Label.Size = new System.Drawing.Size(15, 15);
            this.SortThenBy1Label.TabIndex = 6;
            this.SortThenBy1Label.Text = ">";
            // 
            // SortComboBox1
            // 
            this.SortComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SortComboBox1.FormattingEnabled = true;
            this.SortComboBox1.Items.AddRange(new object[] {
            "School",
            "Cost",
            "Strength",
            "Number of Uses",
            "Range",
            "ID",
            "Rarity",
            "Amount",
            "None"});
            this.SortComboBox1.Location = new System.Drawing.Point(247, 3);
            this.SortComboBox1.Name = "SortComboBox1";
            this.SortComboBox1.Size = new System.Drawing.Size(121, 23);
            this.SortComboBox1.TabIndex = 5;
            this.SortComboBox1.SelectedIndexChanged += new System.EventHandler(this.SortComboBox1_SelectedIndexChanged);
            // 
            // SortLabel
            // 
            this.SortLabel.AutoSize = true;
            this.SortLabel.Location = new System.Drawing.Point(213, 6);
            this.SortLabel.Name = "SortLabel";
            this.SortLabel.Size = new System.Drawing.Size(28, 15);
            this.SortLabel.TabIndex = 4;
            this.SortLabel.Text = "Sort";
            // 
            // SortComboBox2
            // 
            this.SortComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SortComboBox2.FormattingEnabled = true;
            this.SortComboBox2.Items.AddRange(new object[] {
            "School",
            "Cost",
            "Strength",
            "Number of Uses",
            "Range",
            "ID",
            "Rarity",
            "Amount",
            "None"});
            this.SortComboBox2.Location = new System.Drawing.Point(392, 3);
            this.SortComboBox2.Name = "SortComboBox2";
            this.SortComboBox2.Size = new System.Drawing.Size(121, 23);
            this.SortComboBox2.TabIndex = 3;
            this.SortComboBox2.SelectedIndexChanged += new System.EventHandler(this.SortComboBox2_SelectedIndexChanged);
            // 
            // SortComboBox3
            // 
            this.SortComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SortComboBox3.FormattingEnabled = true;
            this.SortComboBox3.Items.AddRange(new object[] {
            "School",
            "Cost",
            "Strength",
            "Number of Uses",
            "Range",
            "ID",
            "Rarity",
            "Amount",
            "None"});
            this.SortComboBox3.Location = new System.Drawing.Point(536, 3);
            this.SortComboBox3.Name = "SortComboBox3";
            this.SortComboBox3.Size = new System.Drawing.Size(121, 23);
            this.SortComboBox3.TabIndex = 2;
            this.SortComboBox3.SelectedIndexChanged += new System.EventHandler(this.SortComboBox3_SelectedIndexChanged);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(9, 6);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(42, 15);
            this.SearchLabel.TabIndex = 1;
            this.SearchLabel.Text = "Search";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(53, 3);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(139, 23);
            this.SearchTextBox.TabIndex = 0;
            // 
            // SkillList
            // 
            this.SkillList.AutoScroll = true;
            this.SkillList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SkillList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.SkillList.Location = new System.Drawing.Point(3, 113);
            this.SkillList.Name = "SkillList";
            this.SkillList.Size = new System.Drawing.Size(331, 299);
            this.SkillList.TabIndex = 3;
            this.SkillList.WrapContents = false;
            // 
            // Footer
            // 
            this.MainLayout.SetColumnSpan(this.Footer, 2);
            this.Footer.Controls.Add(this.CancelButton);
            this.Footer.Controls.Add(this.ConfirmButton);
            this.Footer.Controls.Add(this.ConfirmDropdown);
            this.Footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Footer.Location = new System.Drawing.Point(3, 418);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(668, 29);
            this.Footer.TabIndex = 2;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(469, 3);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(550, 3);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 1;
            this.ConfirmButton.Text = "Confirm";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // ConfirmDropdown
            // 
            this.ConfirmDropdown.Location = new System.Drawing.Point(624, 3);
            this.ConfirmDropdown.Name = "ConfirmDropdown";
            this.ConfirmDropdown.Size = new System.Drawing.Size(33, 23);
            this.ConfirmDropdown.TabIndex = 0;
            this.ConfirmDropdown.Text = "⯆";
            this.ConfirmDropdown.UseVisualStyleBackColor = true;
            this.ConfirmDropdown.Click += new System.EventHandler(this.ConfirmDropdown_Click);
            // 
            // SkillDetail
            // 
            this.SkillDetail.Controls.Add(this.CardDescriptionPanel);
            this.SkillDetail.Controls.Add(this.CardSubheaderPanel);
            this.SkillDetail.Controls.Add(this.CardHeaderPanel);
            this.SkillDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SkillDetail.Location = new System.Drawing.Point(340, 113);
            this.SkillDetail.Name = "SkillDetail";
            this.SkillDetail.Size = new System.Drawing.Size(331, 299);
            this.SkillDetail.TabIndex = 4;
            // 
            // CardDescriptionPanel
            // 
            this.CardDescriptionPanel.Controls.Add(this.CardDescription);
            this.CardDescriptionPanel.Location = new System.Drawing.Point(3, 101);
            this.CardDescriptionPanel.Name = "CardDescriptionPanel";
            this.CardDescriptionPanel.Size = new System.Drawing.Size(382, 29);
            this.CardDescriptionPanel.TabIndex = 2;
            // 
            // CardDescription
            // 
            this.CardDescription.AutoSize = true;
            this.CardDescription.Location = new System.Drawing.Point(3, 0);
            this.CardDescription.Name = "CardDescription";
            this.CardDescription.Size = new System.Drawing.Size(95, 15);
            this.CardDescription.TabIndex = 0;
            this.CardDescription.Text = "Card Description";
            // 
            // CardSubheaderPanel
            // 
            this.CardSubheaderPanel.Controls.Add(this.CardSubtitle);
            this.CardSubheaderPanel.Location = new System.Drawing.Point(3, 66);
            this.CardSubheaderPanel.Name = "CardSubheaderPanel";
            this.CardSubheaderPanel.Size = new System.Drawing.Size(382, 29);
            this.CardSubheaderPanel.TabIndex = 1;
            // 
            // CardSubtitle
            // 
            this.CardSubtitle.AutoSize = true;
            this.CardSubtitle.Location = new System.Drawing.Point(3, 0);
            this.CardSubtitle.Name = "CardSubtitle";
            this.CardSubtitle.Size = new System.Drawing.Size(75, 15);
            this.CardSubtitle.TabIndex = 0;
            this.CardSubtitle.Text = "Card Subtitle";
            // 
            // CardHeaderPanel
            // 
            this.CardHeaderPanel.Controls.Add(this.CardTitle);
            this.CardHeaderPanel.Location = new System.Drawing.Point(3, 31);
            this.CardHeaderPanel.Name = "CardHeaderPanel";
            this.CardHeaderPanel.Size = new System.Drawing.Size(382, 29);
            this.CardHeaderPanel.TabIndex = 0;
            // 
            // CardTitle
            // 
            this.CardTitle.AutoSize = true;
            this.CardTitle.Location = new System.Drawing.Point(3, 0);
            this.CardTitle.Name = "CardTitle";
            this.CardTitle.Size = new System.Drawing.Size(57, 15);
            this.CardTitle.TabIndex = 0;
            this.CardTitle.Text = "Card Title";
            // 
            // SkillSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 450);
            this.Controls.Add(this.MainLayout);
            this.Name = "SkillSelectForm";
            this.Text = "SkillSelectForm";
            this.MainLayout.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            this.Subheader.ResumeLayout(false);
            this.Subheader.PerformLayout();
            this.Footer.ResumeLayout(false);
            this.SkillDetail.ResumeLayout(false);
            this.CardDescriptionPanel.ResumeLayout(false);
            this.CardDescriptionPanel.PerformLayout();
            this.CardSubheaderPanel.ResumeLayout(false);
            this.CardSubheaderPanel.PerformLayout();
            this.CardHeaderPanel.ResumeLayout(false);
            this.CardHeaderPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel MainLayout;
        private Panel Header;
        private Panel Subheader;
        private Panel Footer;
        private FlowLayoutPanel SkillList;
        private Panel SkillDetail;
        private Label SearchLabel;
        private TextBox SearchTextBox;
        private Label SortThenBy2Label;
        private Label SortThenBy1Label;
        private ComboBox SortComboBox1;
        private Label SortLabel;
        private ComboBox SortComboBox2;
        private ComboBox SortComboBox3;
        private Button CancelButton;
        private Button ConfirmButton;
        private Button ConfirmDropdown;
        private Panel CardDescriptionPanel;
        private Panel CardSubheaderPanel;
        private Panel CardHeaderPanel;
        private Label CardDescription;
        private Label CardSubtitle;
        private Label CardTitle;
        private CheckBox PsychoCheckBox;
        private CheckBox FaithCheckBox;
        private CheckBox KiCheckBox;
        private CheckBox NatureCheckBox;
        private CheckBox OpticalCheckBox;
        private CheckBox EraseCheckBox;
        private CheckBox DefenseCheckBox;
        private CheckBox AttackCheckBox;
        private CheckBox AuraCheckBox;
        private CheckBox EnvironmentalCheckBox;
        private CheckBox SpecialCheckBox;
        private CheckBox StatusCheckBox;
    }
}