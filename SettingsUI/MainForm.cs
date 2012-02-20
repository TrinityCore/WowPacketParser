using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SettingsUI
{
    public partial class MainForm : Form
    {
        public MainForm()
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
                {gotCheckBox,  0x0001}, // GameObjectTemplate
                {gosCheckBox,  0x0002}, // GameObjectSpawns
                {qtCheckBox,   0x0004}, // QuestTemplate
                {qpoiCheckBox, 0x0008}, // QuestPOI
                {ctCheckBox,   0x0010}, // CreatureTemplate
                {csCheckBox,   0x0020}, // CreatureSpawns
                {ntCheckBox,   0x0040}, // NpcTrainer
                {nvCheckBox,   0x0080}, // NpcVendor
                {ntxtCheckBox, 0x0100}, // NpcText
                {lCheckBox,    0x0200}, // Loot
                {gCheckBox,    0x0400}, // Gossip
                {ptCheckBox,   0x0800}, // PageText
                {siCheckBox,   0x1000}, // StartInformation
                {sdCheckBox,   0x2000}, // SniffData
                {sdoCheckBox,  0x4000}, // SniffData:Opcodes
                {onCheckBox,   0x8000}, // ObjectNames
                {ceCheckBox,  0x10000}  // CreatureEquip
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
