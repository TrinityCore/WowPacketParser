namespace PacketViewer.Forms
{
    partial class FormFileOpenDetails
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
            this.buttonOpen = new System.Windows.Forms.Button();
            this.comboBoxClientVersion = new System.Windows.Forms.ComboBox();
            this.labelClientVersion = new System.Windows.Forms.Label();
            this.comboBoxReaderType = new System.Windows.Forms.ComboBox();
            this.labelReaderType = new System.Windows.Forms.Label();
            this.groupBoxDetailsView = new System.Windows.Forms.GroupBox();
            this.checkBoxShowHexOutput = new System.Windows.Forms.CheckBox();
            this.checkBoxShowTextOutput = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFileName = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.groupBoxDetailsView.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(29, 206);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxClientVersion
            // 
            this.comboBoxClientVersion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxClientVersion.FormattingEnabled = true;
            this.comboBoxClientVersion.Location = new System.Drawing.Point(105, 35);
            this.comboBoxClientVersion.Name = "comboBoxClientVersion";
            this.comboBoxClientVersion.Size = new System.Drawing.Size(121, 21);
            this.comboBoxClientVersion.TabIndex = 1;
            // 
            // labelClientVersion
            // 
            this.labelClientVersion.AutoSize = true;
            this.labelClientVersion.Location = new System.Drawing.Point(9, 38);
            this.labelClientVersion.Name = "labelClientVersion";
            this.labelClientVersion.Size = new System.Drawing.Size(74, 13);
            this.labelClientVersion.TabIndex = 2;
            this.labelClientVersion.Text = "Client Version:";
            this.labelClientVersion.Click += new System.EventHandler(this.labelClientVersion_Click);
            // 
            // comboBoxReaderType
            // 
            this.comboBoxReaderType.FormattingEnabled = true;
            this.comboBoxReaderType.Location = new System.Drawing.Point(105, 64);
            this.comboBoxReaderType.Name = "comboBoxReaderType";
            this.comboBoxReaderType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxReaderType.TabIndex = 3;
            // 
            // labelReaderType
            // 
            this.labelReaderType.AutoSize = true;
            this.labelReaderType.Location = new System.Drawing.Point(9, 67);
            this.labelReaderType.Name = "labelReaderType";
            this.labelReaderType.Size = new System.Drawing.Size(72, 13);
            this.labelReaderType.TabIndex = 4;
            this.labelReaderType.Text = "Reader Type:";
            this.labelReaderType.Click += new System.EventHandler(this.labelParserType_Click);
            // 
            // groupBoxDetailsView
            // 
            this.groupBoxDetailsView.Controls.Add(this.checkBoxShowHexOutput);
            this.groupBoxDetailsView.Controls.Add(this.checkBoxShowTextOutput);
            this.groupBoxDetailsView.Location = new System.Drawing.Point(12, 101);
            this.groupBoxDetailsView.Name = "groupBoxDetailsView";
            this.groupBoxDetailsView.Size = new System.Drawing.Size(200, 67);
            this.groupBoxDetailsView.TabIndex = 5;
            this.groupBoxDetailsView.TabStop = false;
            this.groupBoxDetailsView.Text = "DetailsView";
            // 
            // checkBoxShowHexOutput
            // 
            this.checkBoxShowHexOutput.AutoSize = true;
            this.checkBoxShowHexOutput.Checked = true;
            this.checkBoxShowHexOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowHexOutput.Location = new System.Drawing.Point(6, 42);
            this.checkBoxShowHexOutput.Name = "checkBoxShowHexOutput";
            this.checkBoxShowHexOutput.Size = new System.Drawing.Size(110, 17);
            this.checkBoxShowHexOutput.TabIndex = 3;
            this.checkBoxShowHexOutput.Text = "Show Hex Output";
            this.checkBoxShowHexOutput.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowTextOutput
            // 
            this.checkBoxShowTextOutput.AutoSize = true;
            this.checkBoxShowTextOutput.Checked = true;
            this.checkBoxShowTextOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowTextOutput.Location = new System.Drawing.Point(6, 19);
            this.checkBoxShowTextOutput.Name = "checkBoxShowTextOutput";
            this.checkBoxShowTextOutput.Size = new System.Drawing.Size(112, 17);
            this.checkBoxShowTextOutput.TabIndex = 2;
            this.checkBoxShowTextOutput.Text = "Show Text Output";
            this.checkBoxShowTextOutput.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(123, 206);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(9, 12);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(57, 13);
            this.labelFileName.TabIndex = 7;
            this.labelFileName.Text = "File Name:";
            this.labelFileName.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Enabled = false;
            this.textBoxFileName.Location = new System.Drawing.Point(73, 9);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(153, 20);
            this.textBoxFileName.TabIndex = 8;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FormFileOpenDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 243);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.labelFileName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxDetailsView);
            this.Controls.Add(this.labelReaderType);
            this.Controls.Add(this.comboBoxReaderType);
            this.Controls.Add(this.labelClientVersion);
            this.Controls.Add(this.comboBoxClientVersion);
            this.Controls.Add(this.buttonOpen);
            this.Name = "FormFileOpenDetails";
            this.Text = "Open File Details";
            this.TopMost = true;
            this.groupBoxDetailsView.ResumeLayout(false);
            this.groupBoxDetailsView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Label labelClientVersion;
        public System.Windows.Forms.ComboBox comboBoxReaderType;
        private System.Windows.Forms.Label labelReaderType;
        private System.Windows.Forms.GroupBox groupBoxDetailsView;
        public System.Windows.Forms.CheckBox checkBoxShowHexOutput;
        public System.Windows.Forms.CheckBox checkBoxShowTextOutput;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFileName;
        public System.Windows.Forms.TextBox textBoxFileName;
        public System.Windows.Forms.ComboBox comboBoxClientVersion;
    }
}