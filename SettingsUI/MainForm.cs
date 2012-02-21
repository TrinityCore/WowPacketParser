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
        private Dictionary<Control, Tuple<string, object>> _settings;

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

            _settings = new Dictionary<Control, Tuple<string, object>>
            {
                // UI element - setting name - default value
                {opcodesTextBox, new Tuple<string, object>("Filters", string.Empty)},
                {ignoreOpcodesTextBox, new Tuple<string, object>("IgnoreFilters", string.Empty)},
                {filtersEntryTextBox, new Tuple<string, object>("IgnoreByEntryFilters", string.Empty)},
                {areasTextBox, new Tuple<string, object>("AreaFilters", string.Empty)},
                {sqlFileNameTextBox, new Tuple<string, object>("SQLFileName", string.Empty)},
                {packetNumUpDown, new Tuple<string, object>("FilterPacketsNum", 0)},
                {minPacketNumUpDown, new Tuple<string, object>("FilterPacketNumLow", 0)},
                {maxPacketNumUpDown, new Tuple<string, object>("FilterPacketNumHigh", 0)},
                {clientBuildComboBox, new Tuple<string, object>("ClientBuild", 0)},
                {threadsReadNumericUpDown, new Tuple<string, object>("ThreadsRead", 0)},
                {threadsParseNumericUpDown, new Tuple<string, object>("ThreadsParse", 0)},
                {sqlOutputMaskLabel, new Tuple<string, object>("SQLOutput", 0)},
                {showPromptCheckBox, new Tuple<string, object>("ShowEndPrompt", true)},
                {logErrorCheckBox, new Tuple<string, object>("LogErrors", false)},
                {splitOutputCheckBox, new Tuple<string, object>("SplitOutput", false)},
                {debugReadsCheckBox, new Tuple<string, object>("DebugReads", false)},
                {parsingLogCheckBox, new Tuple<string, object>("ParsingLog", false)},
                {dumpFormatComboBox, new Tuple<string, object>("DumpFormat", DumpFormat.Text)},
                {statsComboBox, new Tuple<string, object>("StatsOutput", StatsOutput.Global)},
                {sshEnabledCheckBox, new Tuple<string, object>("SSHEnabled", false)},
                {sshServerTextBox, new Tuple<string, object>("SSHHost", "localhost")},
                {sshUsernameTextBox, new Tuple<string, object>("SSHUsername", "root")},
                {sshPasswordTextBox, new Tuple<string, object>("SSHPassword", string.Empty)},
                {sshPortNumericUpDown, new Tuple<string, object>("SSHPort", 22)},
                {sshLocalPortNumericUpDown, new Tuple<string, object>("SSHLocalPort", 3307)},
                {dbEnabledCheckBox, new Tuple<string, object>("DBEnabled", false)},
                {serverTextBox, new Tuple<string, object>("Server", "localhost")},
                {portNumericUpDown, new Tuple<string, object>("Port", 3306)},
                {usernameTextBox, new Tuple<string, object>("Username", "root")},
                {passwordTextBox, new Tuple<string, object>("Password", string.Empty)},
                {databaseTextBox, new Tuple<string, object>("Database", "WPP")},
                {charSetComboBox, new Tuple<string, object>("CharacterSet", CharacterSet.UTF8)},
	        };

            var set = new Settings();
            if (!set.ExistingFile)
                LoadDefaults();
        }

        private void LoadDefaults()
        {
            foreach (var element in _settings)
            {
                if (element.Key is CheckBox) // special case for checkboxes, changing "Text" is not correct
                    ((CheckBox) element.Key).Checked = (bool) element.Value.Item2;
                else
                    element.Key.Text = element.Value.Item2.ToString();
            }
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
