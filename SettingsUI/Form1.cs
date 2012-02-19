using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SettingsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SQLOutputMask = 0;
        }

        private uint _sqlOutputMask;

        private uint SQLOutputMask
        {
            get { return _sqlOutputMask; }
            set
            {
                sqlOutputMaskLabel.Text = "Current value: " + value;
                _sqlOutputMask = value;
            }
        }

        private Dictionary<CheckBox, uint> _sqlOutputValues;

        private void DBEnabledCheckBoxCheckedChanged(object sender, EventArgs e)
        {
                foreach (var control in dbTabPage.Controls.OfType<Control>())
                    control.Enabled = dbEnabledCheckBox.Checked;

                if (dbEnabledCheckBox.Checked)
                    connectionControl.SelectTab(dbTabPage);
        }

        private void SSHEnabledCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in sshTabPage.Controls.OfType<Control>())
                control.Enabled = sshEnabledCheckBox.Checked;

            if (sshEnabledCheckBox.Checked)
                connectionControl.SelectTab(sshTabPage);
        }

        private void Form1Load(object sender, EventArgs e)
        {
            DBEnabledCheckBoxCheckedChanged(sender, e);
            SSHEnabledCheckBoxCheckedChanged(sender, e);
            _sqlOutputValues = new Dictionary<CheckBox, uint>
                                   {
                                       {gotCheckBox,  0x0001},
                                       {gosCheckBox,  0x0002},
                                       {qtCheckBox,   0x0004},
                                       {qpoiCheckBox, 0x0008},
                                       {ctCheckBox,   0x0010},
                                       {csCheckBox,   0x0020},
                                       {ntCheckBox,   0x0040},
                                       {nvCheckBox,   0x0080},
                                       {ntxtCheckBox, 0x0100},
                                       {lCheckBox,    0x0200},
                                       {gCheckBox,    0x0400},
                                       {ptCheckBox,   0x0800},
                                       {siCheckBox,   0x1000},
                                       {sdCheckBox,   0x2000},
                                       {sdoCheckBox,  0x4000}
                                   };
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SQLOutputCheckBoxChanged(object sender, EventArgs e)
        {
            SQLOutputMask ^= _sqlOutputValues[((CheckBox)sender)];
        }

        private void SelectNoneButtonClick(object sender, EventArgs e)
        {
            UpdateSQLOutputCheckBoxes(uint.MinValue);
        }

        private void SelectAllButtonClick(object sender, EventArgs e)
        {
            UpdateSQLOutputCheckBoxes(uint.MaxValue);
        }

        private void UpdateSQLOutputCheckBoxes(uint value)
        {
            foreach (var control in sqlOutputTableLayoutPanel.Controls.OfType<CheckBox>())
                control.Checked = (_sqlOutputValues[(control)] & value) != 0;
        }
    }
}
