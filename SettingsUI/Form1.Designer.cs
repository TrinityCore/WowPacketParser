namespace SettingsUI
{
    partial class Form1
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
            System.Windows.Forms.GroupBox filtersGroupBox;
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.connectionControl = new System.Windows.Forms.TabControl();
            this.dbTabPage = new System.Windows.Forms.TabPage();
            this.charSetComboBox = new System.Windows.Forms.ComboBox();
            this.databaseTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.characterSetLabel = new System.Windows.Forms.Label();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.sshTabPage = new System.Windows.Forms.TabPage();
            this.sshLocalPortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.sshPasswordTextBox = new System.Windows.Forms.TextBox();
            this.sshUsernameTextBox = new System.Windows.Forms.TextBox();
            this.sshPortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.sshServerTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sshServerLabel = new System.Windows.Forms.Label();
            this.threadsLabel = new System.Windows.Forms.Label();
            this.threadsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.threadsPanel = new System.Windows.Forms.Panel();
            this.dumpFormatComboBox = new System.Windows.Forms.ComboBox();
            this.dumpFormatLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.clientBuildPanel = new System.Windows.Forms.Panel();
            this.clientBuildLabel = new System.Windows.Forms.Label();
            this.clientBuildComboBox = new System.Windows.Forms.ComboBox();
            this.showPromptCheckBox = new System.Windows.Forms.CheckBox();
            this.debugReadsCheckBox = new System.Windows.Forms.CheckBox();
            this.sqlOutputGroupBox = new System.Windows.Forms.GroupBox();
            this.sqlOutputTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gotCheckBox = new System.Windows.Forms.CheckBox();
            this.gosCheckBox = new System.Windows.Forms.CheckBox();
            this.qtCheckBox = new System.Windows.Forms.CheckBox();
            this.qpoiCheckBox = new System.Windows.Forms.CheckBox();
            this.ptCheckBox = new System.Windows.Forms.CheckBox();
            this.sdoCheckBox = new System.Windows.Forms.CheckBox();
            this.ctCheckBox = new System.Windows.Forms.CheckBox();
            this.csCheckBox = new System.Windows.Forms.CheckBox();
            this.ntCheckBox = new System.Windows.Forms.CheckBox();
            this.nvCheckBox = new System.Windows.Forms.CheckBox();
            this.gCheckBox = new System.Windows.Forms.CheckBox();
            this.ntxtCheckBox = new System.Windows.Forms.CheckBox();
            this.lCheckBox = new System.Windows.Forms.CheckBox();
            this.sdCheckBox = new System.Windows.Forms.CheckBox();
            this.siCheckBox = new System.Windows.Forms.CheckBox();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.selectNoneButton = new System.Windows.Forms.Button();
            this.sqlOutputMaskLabel = new System.Windows.Forms.Label();
            this.sshEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.dbEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.labelFilters = new System.Windows.Forms.Label();
            this.ignoreLabel = new System.Windows.Forms.Label();
            this.filtersEntryLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            filtersGroupBox = new System.Windows.Forms.GroupBox();
            filtersGroupBox.SuspendLayout();
            this.connectionControl.SuspendLayout();
            this.dbTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.sshTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sshLocalPortNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sshPortNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsNumericUpDown)).BeginInit();
            this.threadsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.clientBuildPanel.SuspendLayout();
            this.sqlOutputGroupBox.SuspendLayout();
            this.sqlOutputTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // filtersGroupBox
            // 
            filtersGroupBox.Controls.Add(this.label2);
            filtersGroupBox.Controls.Add(this.filtersEntryLabel);
            filtersGroupBox.Controls.Add(this.ignoreLabel);
            filtersGroupBox.Controls.Add(this.labelFilters);
            filtersGroupBox.Location = new System.Drawing.Point(12, 215);
            filtersGroupBox.Name = "filtersGroupBox";
            filtersGroupBox.Size = new System.Drawing.Size(457, 107);
            filtersGroupBox.TabIndex = 17;
            filtersGroupBox.TabStop = false;
            filtersGroupBox.Text = "Filters";
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(313, 516);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(394, 516);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // connectionControl
            // 
            this.connectionControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionControl.Controls.Add(this.dbTabPage);
            this.connectionControl.Controls.Add(this.sshTabPage);
            this.connectionControl.HotTrack = true;
            this.connectionControl.Location = new System.Drawing.Point(254, 12);
            this.connectionControl.Name = "connectionControl";
            this.connectionControl.SelectedIndex = 0;
            this.connectionControl.Size = new System.Drawing.Size(215, 197);
            this.connectionControl.TabIndex = 3;
            // 
            // dbTabPage
            // 
            this.dbTabPage.Controls.Add(this.charSetComboBox);
            this.dbTabPage.Controls.Add(this.databaseTextBox);
            this.dbTabPage.Controls.Add(this.passwordTextBox);
            this.dbTabPage.Controls.Add(this.usernameTextBox);
            this.dbTabPage.Controls.Add(this.portNumericUpDown);
            this.dbTabPage.Controls.Add(this.serverTextBox);
            this.dbTabPage.Controls.Add(this.characterSetLabel);
            this.dbTabPage.Controls.Add(this.databaseLabel);
            this.dbTabPage.Controls.Add(this.passwordLabel);
            this.dbTabPage.Controls.Add(this.usernameLabel);
            this.dbTabPage.Controls.Add(this.portLabel);
            this.dbTabPage.Controls.Add(this.serverLabel);
            this.dbTabPage.Location = new System.Drawing.Point(4, 22);
            this.dbTabPage.Name = "dbTabPage";
            this.dbTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dbTabPage.Size = new System.Drawing.Size(207, 171);
            this.dbTabPage.TabIndex = 0;
            this.dbTabPage.Text = "MySQL";
            this.dbTabPage.UseVisualStyleBackColor = true;
            // 
            // charSetComboBox
            // 
            this.charSetComboBox.Items.AddRange(new object[] {
            "utf8",
            "latin1"});
            this.charSetComboBox.Location = new System.Drawing.Point(67, 139);
            this.charSetComboBox.Name = "charSetComboBox";
            this.charSetComboBox.Size = new System.Drawing.Size(121, 21);
            this.charSetComboBox.TabIndex = 5;
            this.charSetComboBox.Text = "utf8";
            // 
            // databaseTextBox
            // 
            this.databaseTextBox.Location = new System.Drawing.Point(67, 113);
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.Size = new System.Drawing.Size(100, 20);
            this.databaseTextBox.TabIndex = 4;
            this.databaseTextBox.Text = "WPP";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(67, 86);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(67, 59);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(100, 20);
            this.usernameTextBox.TabIndex = 2;
            this.usernameTextBox.Text = "root";
            // 
            // portNumericUpDown
            // 
            this.portNumericUpDown.Location = new System.Drawing.Point(67, 33);
            this.portNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.portNumericUpDown.TabIndex = 1;
            this.portNumericUpDown.Value = new decimal(new int[] {
            3306,
            0,
            0,
            0});
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(67, 6);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(100, 20);
            this.serverTextBox.TabIndex = 0;
            this.serverTextBox.Text = "localhost";
            // 
            // characterSetLabel
            // 
            this.characterSetLabel.AutoSize = true;
            this.characterSetLabel.Location = new System.Drawing.Point(6, 142);
            this.characterSetLabel.Name = "characterSetLabel";
            this.characterSetLabel.Size = new System.Drawing.Size(48, 13);
            this.characterSetLabel.TabIndex = 5;
            this.characterSetLabel.Text = "CharSet:";
            // 
            // databaseLabel
            // 
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Location = new System.Drawing.Point(6, 116);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(56, 13);
            this.databaseLabel.TabIndex = 4;
            this.databaseLabel.Text = "Database:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(6, 89);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "Password:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(6, 62);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "Username:";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(6, 34);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "Port:";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(6, 9);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(41, 13);
            this.serverLabel.TabIndex = 0;
            this.serverLabel.Text = "Server:";
            // 
            // sshTabPage
            // 
            this.sshTabPage.Controls.Add(this.sshLocalPortNumericUpDown);
            this.sshTabPage.Controls.Add(this.label1);
            this.sshTabPage.Controls.Add(this.sshPasswordTextBox);
            this.sshTabPage.Controls.Add(this.sshUsernameTextBox);
            this.sshTabPage.Controls.Add(this.sshPortNumericUpDown);
            this.sshTabPage.Controls.Add(this.sshServerTextBox);
            this.sshTabPage.Controls.Add(this.label3);
            this.sshTabPage.Controls.Add(this.label4);
            this.sshTabPage.Controls.Add(this.label5);
            this.sshTabPage.Controls.Add(this.sshServerLabel);
            this.sshTabPage.Location = new System.Drawing.Point(4, 22);
            this.sshTabPage.Name = "sshTabPage";
            this.sshTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.sshTabPage.Size = new System.Drawing.Size(207, 171);
            this.sshTabPage.TabIndex = 1;
            this.sshTabPage.Text = "SSH";
            this.sshTabPage.UseVisualStyleBackColor = true;
            // 
            // sshLocalPortNumericUpDown
            // 
            this.sshLocalPortNumericUpDown.Location = new System.Drawing.Point(67, 112);
            this.sshLocalPortNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.sshLocalPortNumericUpDown.Name = "sshLocalPortNumericUpDown";
            this.sshLocalPortNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.sshLocalPortNumericUpDown.TabIndex = 5;
            this.sshLocalPortNumericUpDown.Value = new decimal(new int[] {
            3307,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Local Port:";
            // 
            // sshPasswordTextBox
            // 
            this.sshPasswordTextBox.Location = new System.Drawing.Point(67, 86);
            this.sshPasswordTextBox.Name = "sshPasswordTextBox";
            this.sshPasswordTextBox.PasswordChar = '*';
            this.sshPasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.sshPasswordTextBox.TabIndex = 3;
            this.sshPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // sshUsernameTextBox
            // 
            this.sshUsernameTextBox.Location = new System.Drawing.Point(67, 59);
            this.sshUsernameTextBox.Name = "sshUsernameTextBox";
            this.sshUsernameTextBox.Size = new System.Drawing.Size(100, 20);
            this.sshUsernameTextBox.TabIndex = 2;
            this.sshUsernameTextBox.Text = "root";
            // 
            // sshPortNumericUpDown
            // 
            this.sshPortNumericUpDown.Location = new System.Drawing.Point(67, 33);
            this.sshPortNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.sshPortNumericUpDown.Name = "sshPortNumericUpDown";
            this.sshPortNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.sshPortNumericUpDown.TabIndex = 1;
            this.sshPortNumericUpDown.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // sshServerTextBox
            // 
            this.sshServerTextBox.Location = new System.Drawing.Point(67, 6);
            this.sshServerTextBox.Name = "sshServerTextBox";
            this.sshServerTextBox.Size = new System.Drawing.Size(100, 20);
            this.sshServerTextBox.TabIndex = 0;
            this.sshServerTextBox.Text = "localhost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Username:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Port:";
            // 
            // sshServerLabel
            // 
            this.sshServerLabel.AutoSize = true;
            this.sshServerLabel.Location = new System.Drawing.Point(6, 9);
            this.sshServerLabel.Name = "sshServerLabel";
            this.sshServerLabel.Size = new System.Drawing.Size(41, 13);
            this.sshServerLabel.TabIndex = 0;
            this.sshServerLabel.Text = "Server:";
            // 
            // threadsLabel
            // 
            this.threadsLabel.AutoSize = true;
            this.threadsLabel.Location = new System.Drawing.Point(3, 5);
            this.threadsLabel.Name = "threadsLabel";
            this.threadsLabel.Size = new System.Drawing.Size(49, 13);
            this.threadsLabel.TabIndex = 4;
            this.threadsLabel.Text = "Threads:";
            // 
            // threadsNumericUpDown
            // 
            this.threadsNumericUpDown.Location = new System.Drawing.Point(58, 3);
            this.threadsNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.threadsNumericUpDown.Name = "threadsNumericUpDown";
            this.threadsNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.threadsNumericUpDown.TabIndex = 5;
            // 
            // threadsPanel
            // 
            this.threadsPanel.Controls.Add(this.threadsLabel);
            this.threadsPanel.Controls.Add(this.threadsNumericUpDown);
            this.threadsPanel.Location = new System.Drawing.Point(12, 12);
            this.threadsPanel.Name = "threadsPanel";
            this.threadsPanel.Size = new System.Drawing.Size(110, 28);
            this.threadsPanel.TabIndex = 6;
            // 
            // dumpFormatComboBox
            // 
            this.dumpFormatComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.dumpFormatComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.dumpFormatComboBox.DisplayMember = "Text";
            this.dumpFormatComboBox.FormattingEnabled = true;
            this.dumpFormatComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dumpFormatComboBox.Items.AddRange(new object[] {
            "No dump",
            "Text",
            "Text (only the headers of each packet)",
            "Text: (summary - only the header of 1st occurrence of each opc)",
            "Binary (.pkt)",
            "Binary (.bin)"});
            this.dumpFormatComboBox.Location = new System.Drawing.Point(82, 3);
            this.dumpFormatComboBox.Name = "dumpFormatComboBox";
            this.dumpFormatComboBox.Size = new System.Drawing.Size(128, 21);
            this.dumpFormatComboBox.TabIndex = 7;
            this.dumpFormatComboBox.Text = "Text";
            // 
            // dumpFormatLabel
            // 
            this.dumpFormatLabel.AutoSize = true;
            this.dumpFormatLabel.Location = new System.Drawing.Point(3, 6);
            this.dumpFormatLabel.Name = "dumpFormatLabel";
            this.dumpFormatLabel.Size = new System.Drawing.Size(73, 13);
            this.dumpFormatLabel.TabIndex = 8;
            this.dumpFormatLabel.Text = "Dump Format:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dumpFormatLabel);
            this.panel1.Controls.Add(this.dumpFormatComboBox);
            this.panel1.Location = new System.Drawing.Point(12, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 29);
            this.panel1.TabIndex = 9;
            // 
            // clientBuildPanel
            // 
            this.clientBuildPanel.Controls.Add(this.clientBuildLabel);
            this.clientBuildPanel.Controls.Add(this.clientBuildComboBox);
            this.clientBuildPanel.Location = new System.Drawing.Point(12, 81);
            this.clientBuildPanel.Name = "clientBuildPanel";
            this.clientBuildPanel.Size = new System.Drawing.Size(213, 29);
            this.clientBuildPanel.TabIndex = 10;
            // 
            // clientBuildLabel
            // 
            this.clientBuildLabel.AutoSize = true;
            this.clientBuildLabel.Location = new System.Drawing.Point(3, 6);
            this.clientBuildLabel.Name = "clientBuildLabel";
            this.clientBuildLabel.Size = new System.Drawing.Size(62, 13);
            this.clientBuildLabel.TabIndex = 8;
            this.clientBuildLabel.Text = "Client Build:";
            // 
            // clientBuildComboBox
            // 
            this.clientBuildComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.clientBuildComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.clientBuildComboBox.DisplayMember = "Text";
            this.clientBuildComboBox.FormattingEnabled = true;
            this.clientBuildComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.clientBuildComboBox.Items.AddRange(new object[] {
            "None",
            "V4_3_0_15050",
            "V4_3_0_15005",
            "V4_2_2_14545",
            "V4_2_0a_14480",
            "V4_2_0_14333",
            "V4_1_0a_14007",
            "V4_1_0_13914",
            "V4_0_6a_13623",
            "V4_0_6_13596",
            "V4_0_3_13329",
            "V4_0_1a_13205",
            "V4_0_1_13164",
            "V3_3_5a_12340",
            "V3_3_5_12213",
            "V3_3_3a_11723",
            "V3_3_3_11685",
            "V3_3_0a_11159",
            "V3_3_0_10958",
            "V3_2_2a_10505",
            "V3_2_2_10482",
            "V3_2_0a_10314",
            "V3_2_0_10192",
            "V3_1_3_9947",
            "V3_1_2_9901",
            "V3_1_1a_9835",
            "V3_1_1_9806",
            "V3_1_0_9767",
            "V3_0_9_9551",
            "V3_0_8a_9506",
            "V3_0_8_9464",
            "V3_0_3_9183",
            "V3_0_2_9056",
            "V2_4_3_8606",
            "V2_4_2_8209",
            "V2_4_1_8125",
            "V2_4_0_8089",
            "V2_3_3_7799",
            "V2_3_2_7741",
            "V2_3_0_7561",
            "V2_2_3_7359",
            "V2_2_2_7318",
            "V2_2_0_7272",
            "V2_1_3_6898",
            "V2_1_2_6803",
            "V2_1_1_6739",
            "V2_1_0_6692",
            "V2_0_6_6337",
            "V2_0_3_6299",
            "V2_0_1_6180"});
            this.clientBuildComboBox.Location = new System.Drawing.Point(82, 3);
            this.clientBuildComboBox.Name = "clientBuildComboBox";
            this.clientBuildComboBox.Size = new System.Drawing.Size(128, 21);
            this.clientBuildComboBox.TabIndex = 7;
            this.clientBuildComboBox.Text = "Auto";
            // 
            // showPromptCheckBox
            // 
            this.showPromptCheckBox.AutoSize = true;
            this.showPromptCheckBox.Checked = true;
            this.showPromptCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPromptCheckBox.Location = new System.Drawing.Point(18, 162);
            this.showPromptCheckBox.Name = "showPromptCheckBox";
            this.showPromptCheckBox.Size = new System.Drawing.Size(111, 17);
            this.showPromptCheckBox.TabIndex = 12;
            this.showPromptCheckBox.Text = "Show End Prompt";
            this.showPromptCheckBox.UseVisualStyleBackColor = true;
            // 
            // debugReadsCheckBox
            // 
            this.debugReadsCheckBox.AutoSize = true;
            this.debugReadsCheckBox.Location = new System.Drawing.Point(18, 185);
            this.debugReadsCheckBox.Name = "debugReadsCheckBox";
            this.debugReadsCheckBox.Size = new System.Drawing.Size(92, 17);
            this.debugReadsCheckBox.TabIndex = 13;
            this.debugReadsCheckBox.Text = "Debug Reads";
            this.debugReadsCheckBox.UseVisualStyleBackColor = true;
            // 
            // sqlOutputGroupBox
            // 
            this.sqlOutputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sqlOutputGroupBox.Controls.Add(this.sqlOutputTableLayoutPanel);
            this.sqlOutputGroupBox.Controls.Add(this.selectAllButton);
            this.sqlOutputGroupBox.Controls.Add(this.selectNoneButton);
            this.sqlOutputGroupBox.Controls.Add(this.sqlOutputMaskLabel);
            this.sqlOutputGroupBox.Location = new System.Drawing.Point(12, 340);
            this.sqlOutputGroupBox.Name = "sqlOutputGroupBox";
            this.sqlOutputGroupBox.Size = new System.Drawing.Size(457, 170);
            this.sqlOutputGroupBox.TabIndex = 14;
            this.sqlOutputGroupBox.TabStop = false;
            this.sqlOutputGroupBox.Text = "SQL Output";
            // 
            // sqlOutputTableLayoutPanel
            // 
            this.sqlOutputTableLayoutPanel.ColumnCount = 3;
            this.sqlOutputTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sqlOutputTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sqlOutputTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sqlOutputTableLayoutPanel.Controls.Add(this.gotCheckBox, 0, 0);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.gosCheckBox, 0, 1);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.qtCheckBox, 0, 2);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.qpoiCheckBox, 0, 3);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.ptCheckBox, 2, 1);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.sdoCheckBox, 2, 4);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.ctCheckBox, 0, 4);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.csCheckBox, 1, 0);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.ntCheckBox, 1, 1);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.nvCheckBox, 1, 2);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.gCheckBox, 2, 0);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.ntxtCheckBox, 1, 3);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.lCheckBox, 1, 4);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.sdCheckBox, 2, 3);
            this.sqlOutputTableLayoutPanel.Controls.Add(this.siCheckBox, 2, 2);
            this.sqlOutputTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.sqlOutputTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.sqlOutputTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.sqlOutputTableLayoutPanel.Name = "sqlOutputTableLayoutPanel";
            this.sqlOutputTableLayoutPanel.RowCount = 5;
            this.sqlOutputTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sqlOutputTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sqlOutputTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sqlOutputTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sqlOutputTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sqlOutputTableLayoutPanel.Size = new System.Drawing.Size(451, 120);
            this.sqlOutputTableLayoutPanel.TabIndex = 18;
            // 
            // gotCheckBox
            // 
            this.gotCheckBox.AutoSize = true;
            this.gotCheckBox.Location = new System.Drawing.Point(3, 3);
            this.gotCheckBox.Name = "gotCheckBox";
            this.gotCheckBox.Size = new System.Drawing.Size(129, 17);
            this.gotCheckBox.TabIndex = 0;
            this.gotCheckBox.Text = "GameObjectTemplate";
            this.gotCheckBox.UseVisualStyleBackColor = true;
            this.gotCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // gosCheckBox
            // 
            this.gosCheckBox.AutoSize = true;
            this.gosCheckBox.Location = new System.Drawing.Point(3, 26);
            this.gosCheckBox.Name = "gosCheckBox";
            this.gosCheckBox.Size = new System.Drawing.Size(123, 17);
            this.gosCheckBox.TabIndex = 1;
            this.gosCheckBox.Text = "GameObjectSpawns";
            this.gosCheckBox.UseVisualStyleBackColor = true;
            this.gosCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // qtCheckBox
            // 
            this.qtCheckBox.AutoSize = true;
            this.qtCheckBox.Location = new System.Drawing.Point(3, 49);
            this.qtCheckBox.Name = "qtCheckBox";
            this.qtCheckBox.Size = new System.Drawing.Size(98, 17);
            this.qtCheckBox.TabIndex = 2;
            this.qtCheckBox.Text = "QuestTemplate";
            this.qtCheckBox.UseVisualStyleBackColor = true;
            this.qtCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // qpoiCheckBox
            // 
            this.qpoiCheckBox.AutoSize = true;
            this.qpoiCheckBox.Location = new System.Drawing.Point(3, 72);
            this.qpoiCheckBox.Name = "qpoiCheckBox";
            this.qpoiCheckBox.Size = new System.Drawing.Size(72, 17);
            this.qpoiCheckBox.TabIndex = 3;
            this.qpoiCheckBox.Text = "QuestPOI";
            this.qpoiCheckBox.UseVisualStyleBackColor = true;
            this.qpoiCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // ptCheckBox
            // 
            this.ptCheckBox.AutoSize = true;
            this.ptCheckBox.Location = new System.Drawing.Point(303, 26);
            this.ptCheckBox.Name = "ptCheckBox";
            this.ptCheckBox.Size = new System.Drawing.Size(72, 17);
            this.ptCheckBox.TabIndex = 11;
            this.ptCheckBox.Text = "PageText";
            this.ptCheckBox.UseVisualStyleBackColor = true;
            this.ptCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // sdoCheckBox
            // 
            this.sdoCheckBox.AutoSize = true;
            this.sdoCheckBox.Location = new System.Drawing.Point(303, 95);
            this.sdoCheckBox.Name = "sdoCheckBox";
            this.sdoCheckBox.Size = new System.Drawing.Size(116, 17);
            this.sdoCheckBox.TabIndex = 14;
            this.sdoCheckBox.Text = "SniffData:Opcodes";
            this.sdoCheckBox.UseVisualStyleBackColor = true;
            this.sdoCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // ctCheckBox
            // 
            this.ctCheckBox.AutoSize = true;
            this.ctCheckBox.Location = new System.Drawing.Point(3, 95);
            this.ctCheckBox.Name = "ctCheckBox";
            this.ctCheckBox.Size = new System.Drawing.Size(110, 17);
            this.ctCheckBox.TabIndex = 4;
            this.ctCheckBox.Text = "CreatureTemplate";
            this.ctCheckBox.UseVisualStyleBackColor = true;
            this.ctCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // csCheckBox
            // 
            this.csCheckBox.AutoSize = true;
            this.csCheckBox.Location = new System.Drawing.Point(153, 3);
            this.csCheckBox.Name = "csCheckBox";
            this.csCheckBox.Size = new System.Drawing.Size(104, 17);
            this.csCheckBox.TabIndex = 5;
            this.csCheckBox.Text = "CreatureSpawns";
            this.csCheckBox.UseVisualStyleBackColor = true;
            this.csCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // ntCheckBox
            // 
            this.ntCheckBox.AutoSize = true;
            this.ntCheckBox.Location = new System.Drawing.Point(153, 26);
            this.ntCheckBox.Name = "ntCheckBox";
            this.ntCheckBox.Size = new System.Drawing.Size(79, 17);
            this.ntCheckBox.TabIndex = 6;
            this.ntCheckBox.Text = "NpcTrainer";
            this.ntCheckBox.UseVisualStyleBackColor = true;
            this.ntCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // nvCheckBox
            // 
            this.nvCheckBox.AutoSize = true;
            this.nvCheckBox.Location = new System.Drawing.Point(153, 49);
            this.nvCheckBox.Name = "nvCheckBox";
            this.nvCheckBox.Size = new System.Drawing.Size(80, 17);
            this.nvCheckBox.TabIndex = 7;
            this.nvCheckBox.Text = "NpcVendor";
            this.nvCheckBox.UseVisualStyleBackColor = true;
            this.nvCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // gCheckBox
            // 
            this.gCheckBox.AutoSize = true;
            this.gCheckBox.Location = new System.Drawing.Point(303, 3);
            this.gCheckBox.Name = "gCheckBox";
            this.gCheckBox.Size = new System.Drawing.Size(58, 17);
            this.gCheckBox.TabIndex = 10;
            this.gCheckBox.Text = "Gossip";
            this.gCheckBox.UseVisualStyleBackColor = true;
            this.gCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // ntxtCheckBox
            // 
            this.ntxtCheckBox.AutoSize = true;
            this.ntxtCheckBox.Location = new System.Drawing.Point(153, 72);
            this.ntxtCheckBox.Name = "ntxtCheckBox";
            this.ntxtCheckBox.Size = new System.Drawing.Size(67, 17);
            this.ntxtCheckBox.TabIndex = 8;
            this.ntxtCheckBox.Text = "NpcText";
            this.ntxtCheckBox.UseVisualStyleBackColor = true;
            this.ntxtCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // lCheckBox
            // 
            this.lCheckBox.AutoSize = true;
            this.lCheckBox.Location = new System.Drawing.Point(153, 95);
            this.lCheckBox.Name = "lCheckBox";
            this.lCheckBox.Size = new System.Drawing.Size(47, 17);
            this.lCheckBox.TabIndex = 9;
            this.lCheckBox.Text = "Loot";
            this.lCheckBox.UseVisualStyleBackColor = true;
            this.lCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // sdCheckBox
            // 
            this.sdCheckBox.AutoSize = true;
            this.sdCheckBox.Location = new System.Drawing.Point(303, 72);
            this.sdCheckBox.Name = "sdCheckBox";
            this.sdCheckBox.Size = new System.Drawing.Size(70, 17);
            this.sdCheckBox.TabIndex = 13;
            this.sdCheckBox.Text = "SniffData";
            this.sdCheckBox.UseVisualStyleBackColor = true;
            this.sdCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // siCheckBox
            // 
            this.siCheckBox.AutoSize = true;
            this.siCheckBox.Location = new System.Drawing.Point(303, 49);
            this.siCheckBox.Name = "siCheckBox";
            this.siCheckBox.Size = new System.Drawing.Size(100, 17);
            this.siCheckBox.TabIndex = 12;
            this.siCheckBox.Text = "StartInformation";
            this.siCheckBox.UseVisualStyleBackColor = true;
            this.siCheckBox.CheckedChanged += new System.EventHandler(this.SQLOutputCheckBoxChanged);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAllButton.Location = new System.Drawing.Point(295, 139);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(75, 23);
            this.selectAllButton.TabIndex = 17;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.SelectAllButtonClick);
            // 
            // selectNoneButton
            // 
            this.selectNoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectNoneButton.Location = new System.Drawing.Point(376, 139);
            this.selectNoneButton.Name = "selectNoneButton";
            this.selectNoneButton.Size = new System.Drawing.Size(75, 23);
            this.selectNoneButton.TabIndex = 16;
            this.selectNoneButton.Text = "Select None";
            this.selectNoneButton.UseVisualStyleBackColor = true;
            this.selectNoneButton.Click += new System.EventHandler(this.SelectNoneButtonClick);
            // 
            // sqlOutputMaskLabel
            // 
            this.sqlOutputMaskLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sqlOutputMaskLabel.AutoSize = true;
            this.sqlOutputMaskLabel.Location = new System.Drawing.Point(6, 144);
            this.sqlOutputMaskLabel.Name = "sqlOutputMaskLabel";
            this.sqlOutputMaskLabel.Size = new System.Drawing.Size(86, 13);
            this.sqlOutputMaskLabel.TabIndex = 15;
            this.sqlOutputMaskLabel.Text = "SQLOutputMask";
            // 
            // sshEnabledCheckBox
            // 
            this.sshEnabledCheckBox.AutoSize = true;
            this.sshEnabledCheckBox.Location = new System.Drawing.Point(18, 139);
            this.sshEnabledCheckBox.Name = "sshEnabledCheckBox";
            this.sshEnabledCheckBox.Size = new System.Drawing.Size(90, 17);
            this.sshEnabledCheckBox.TabIndex = 15;
            this.sshEnabledCheckBox.Text = "SSH Enabled";
            this.sshEnabledCheckBox.UseVisualStyleBackColor = true;
            this.sshEnabledCheckBox.CheckedChanged += new System.EventHandler(this.SSHEnabledCheckBoxCheckedChanged);
            // 
            // dbEnabledCheckBox
            // 
            this.dbEnabledCheckBox.AutoSize = true;
            this.dbEnabledCheckBox.Checked = true;
            this.dbEnabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dbEnabledCheckBox.Location = new System.Drawing.Point(18, 116);
            this.dbEnabledCheckBox.Name = "dbEnabledCheckBox";
            this.dbEnabledCheckBox.Size = new System.Drawing.Size(83, 17);
            this.dbEnabledCheckBox.TabIndex = 16;
            this.dbEnabledCheckBox.Text = "DB Enabled";
            this.dbEnabledCheckBox.UseVisualStyleBackColor = true;
            this.dbEnabledCheckBox.CheckedChanged += new System.EventHandler(this.DBEnabledCheckBoxCheckedChanged);
            // 
            // labelFilters
            // 
            this.labelFilters.AutoSize = true;
            this.labelFilters.Location = new System.Drawing.Point(15, 20);
            this.labelFilters.Name = "labelFilters";
            this.labelFilters.Size = new System.Drawing.Size(37, 13);
            this.labelFilters.TabIndex = 0;
            this.labelFilters.Text = "Filters:";
            // 
            // ignoreLabel
            // 
            this.ignoreLabel.AutoSize = true;
            this.ignoreLabel.Location = new System.Drawing.Point(12, 49);
            this.ignoreLabel.Name = "ignoreLabel";
            this.ignoreLabel.Size = new System.Drawing.Size(70, 13);
            this.ignoreLabel.TabIndex = 1;
            this.ignoreLabel.Text = "Ignore Filters:";
            // 
            // filtersEntryLabel
            // 
            this.filtersEntryLabel.AutoSize = true;
            this.filtersEntryLabel.Location = new System.Drawing.Point(12, 75);
            this.filtersEntryLabel.Name = "filtersEntryLabel";
            this.filtersEntryLabel.Size = new System.Drawing.Size(107, 13);
            this.filtersEntryLabel.TabIndex = 2;
            this.filtersEntryLabel.Text = "Ignore Filters by entry";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Num";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 551);
            this.Controls.Add(filtersGroupBox);
            this.Controls.Add(this.dbEnabledCheckBox);
            this.Controls.Add(this.sshEnabledCheckBox);
            this.Controls.Add(this.sqlOutputGroupBox);
            this.Controls.Add(this.debugReadsCheckBox);
            this.Controls.Add(this.showPromptCheckBox);
            this.Controls.Add(this.clientBuildPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.threadsPanel);
            this.Controls.Add(this.connectionControl);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "WPP Settings";
            this.Load += new System.EventHandler(this.Form1Load);
            filtersGroupBox.ResumeLayout(false);
            filtersGroupBox.PerformLayout();
            this.connectionControl.ResumeLayout(false);
            this.dbTabPage.ResumeLayout(false);
            this.dbTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.sshTabPage.ResumeLayout(false);
            this.sshTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sshLocalPortNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sshPortNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsNumericUpDown)).EndInit();
            this.threadsPanel.ResumeLayout(false);
            this.threadsPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.clientBuildPanel.ResumeLayout(false);
            this.clientBuildPanel.PerformLayout();
            this.sqlOutputGroupBox.ResumeLayout(false);
            this.sqlOutputGroupBox.PerformLayout();
            this.sqlOutputTableLayoutPanel.ResumeLayout(false);
            this.sqlOutputTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TabControl connectionControl;
        private System.Windows.Forms.TabPage dbTabPage;
        private System.Windows.Forms.TabPage sshTabPage;
        private System.Windows.Forms.Label characterSetLabel;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.NumericUpDown portNumericUpDown;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.ComboBox charSetComboBox;
        private System.Windows.Forms.TextBox databaseTextBox;
        private System.Windows.Forms.NumericUpDown sshLocalPortNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sshPasswordTextBox;
        private System.Windows.Forms.TextBox sshUsernameTextBox;
        private System.Windows.Forms.NumericUpDown sshPortNumericUpDown;
        private System.Windows.Forms.TextBox sshServerTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label sshServerLabel;
        private System.Windows.Forms.Label threadsLabel;
        private System.Windows.Forms.NumericUpDown threadsNumericUpDown;
        private System.Windows.Forms.Panel threadsPanel;
        private System.Windows.Forms.ComboBox dumpFormatComboBox;
        private System.Windows.Forms.Label dumpFormatLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel clientBuildPanel;
        private System.Windows.Forms.Label clientBuildLabel;
        private System.Windows.Forms.ComboBox clientBuildComboBox;
        private System.Windows.Forms.CheckBox showPromptCheckBox;
        private System.Windows.Forms.CheckBox debugReadsCheckBox;
        private System.Windows.Forms.GroupBox sqlOutputGroupBox;
        private System.Windows.Forms.CheckBox sdoCheckBox;
        private System.Windows.Forms.CheckBox sdCheckBox;
        private System.Windows.Forms.CheckBox siCheckBox;
        private System.Windows.Forms.CheckBox ptCheckBox;
        private System.Windows.Forms.CheckBox gCheckBox;
        private System.Windows.Forms.CheckBox lCheckBox;
        private System.Windows.Forms.CheckBox ntxtCheckBox;
        private System.Windows.Forms.CheckBox nvCheckBox;
        private System.Windows.Forms.CheckBox ntCheckBox;
        private System.Windows.Forms.CheckBox csCheckBox;
        private System.Windows.Forms.CheckBox ctCheckBox;
        private System.Windows.Forms.CheckBox qpoiCheckBox;
        private System.Windows.Forms.CheckBox qtCheckBox;
        private System.Windows.Forms.CheckBox gosCheckBox;
        private System.Windows.Forms.CheckBox gotCheckBox;
        private System.Windows.Forms.CheckBox sshEnabledCheckBox;
        private System.Windows.Forms.CheckBox dbEnabledCheckBox;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button selectNoneButton;
        private System.Windows.Forms.Label sqlOutputMaskLabel;
        private System.Windows.Forms.TableLayoutPanel sqlOutputTableLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label filtersEntryLabel;
        private System.Windows.Forms.Label ignoreLabel;
        private System.Windows.Forms.Label labelFilters;
    }
}

