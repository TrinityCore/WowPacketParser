using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PacketViewer.Processing;
using PacketParser.Loading;
using PacketParser.Misc;
using PacketParser.Enums;
using System.IO;

namespace PacketViewer.Forms
{
    public partial class FormFileOpenDetails : Form
    {
        public FormFileOpenDetails(string fileName)
        {
            InitializeComponent();
            comboBoxReaderType.Items.AddRange(Reader.GetReaderTypes().ToArray());
            comboBoxClientVersion.Items.AddRange(ClientVersion.GetAvailableVersions().ToArray().Cast<Object>().ToArray<Object>());
            var rName = Reader.GetReaderTypeByFileName(fileName);
            if (!String.IsNullOrEmpty(rName))
            {
                comboBoxReaderType.SelectedItem = rName;
                var r = Reader.GetReader(rName, fileName);
                var build = r.GetBuild();
                if (build != ClientVersionBuild.Zero)
                    comboBoxClientVersion.SelectedItem = build;
                else
                    comboBoxClientVersion.Text = "select";
                r.Dispose();
            }
            else
            {
                comboBoxReaderType.Text = "select";
                comboBoxClientVersion.Text = "select";
            }
            textBoxFileName.Text = Path.GetFileName(fileName);
        }

        private void labelClientVersion_Click(object sender, EventArgs e)
        {

        }

        private void labelParserType_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxClientVersion.SelectedIndex == -1 || comboBoxReaderType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select proper client version and reader type before proceeding");
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
