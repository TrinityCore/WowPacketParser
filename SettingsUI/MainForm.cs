using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
                sqlOutputMaskLabel.Text = String.Format(CultureInfo.InvariantCulture, "{0,6:D6} (0x{0,5:X5})", value);
                _sqlOutputMask = value;
            }
        }

        private Dictionary<CheckBox, uint> _sqlOutputValues;
        private Dictionary<Control, Tuple<string, dynamic>> _defaultSettings;
        private Settings _configuration;

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
                {gotCheckBox,  0x00001}, // GameObjectTemplate
                {gosCheckBox,  0x00002}, // GameObjectSpawns
                {qtCheckBox,   0x00004}, // QuestTemplate
                {qpoiCheckBox, 0x00008}, // QuestPOI
                {ctCheckBox,   0x00010}, // CreatureTemplate
                {csCheckBox,   0x00020}, // CreatureSpawns
                {ntCheckBox,   0x00040}, // NpcTrainer
                {nvCheckBox,   0x00080}, // NpcVendor
                {ntxtCheckBox, 0x00100}, // NpcText
                {lCheckBox,    0x00200}, // Loot
                {gCheckBox,    0x00400}, // Gossip
                {ptCheckBox,   0x00800}, // PageText
                {siCheckBox,   0x01000}, // StartInformation
                {sdCheckBox,   0x02000}, // SniffData
                {sdoCheckBox,  0x04000}, // SniffData:Opcodes
                {onCheckBox,   0x08000}, // ObjectNames
                {ceCheckBox,   0x10000}, // CreatureEquip
                {mCheckBox,    0x20000}, // Creature Movement
                {itCheckBox,   0x40000}, // Item Template
            };

            _defaultSettings = new Dictionary<Control, Tuple<string, dynamic>>
            {
                // UI element - setting name - default value
                {opcodesTextBox, new Tuple<string, dynamic>("Filters", string.Empty)},
                {ignoreOpcodesTextBox, new Tuple<string, dynamic>("IgnoreFilters", string.Empty)},
                {filtersEntryTextBox, new Tuple<string, dynamic>("IgnoreByEntryFilters", string.Empty)},
                {areasTextBox, new Tuple<string, dynamic>("AreaFilters", string.Empty)},
                {sqlFileNameTextBox, new Tuple<string, dynamic>("SQLFileName", string.Empty)},
                {packetNumUpDown, new Tuple<string, dynamic>("FilterPacketsNum", 0)},
                {minPacketNumUpDown, new Tuple<string, dynamic>("FilterPacketNumLow", 0)},
                {maxPacketNumUpDown, new Tuple<string, dynamic>("FilterPacketNumHigh", 0)},
                {clientBuildComboBox, new Tuple<string, dynamic>("ClientBuild", string.Empty)},
                {sqlOutputMaskLabel, new Tuple<string, dynamic>("SQLOutput", 0)},
                {showPromptCheckBox, new Tuple<string, dynamic>("ShowEndPrompt", true)},
                //{packetLogErrorsCheckbox, new Tuple<string, dynamic>("LogPacketErrors", false)},
                {logErrorCheckBox, new Tuple<string, dynamic>("LogErrors", false)},
                {splitOutputCheckBox, new Tuple<string, dynamic>("SplitOutput", false)},
                {debugReadsCheckBox, new Tuple<string, dynamic>("DebugReads", false)},
                {parsingLogCheckBox, new Tuple<string, dynamic>("ParsingLog", false)},
                {dumpFormatComboBox, new Tuple<string, dynamic>("DumpFormat", DumpFormat.Text)},
                {statsComboBox, new Tuple<string, dynamic>("StatsOutput", StatsOutput.Global)},
                {sshEnabledCheckBox, new Tuple<string, dynamic>("SSHEnabled", false)},
                {sshServerTextBox, new Tuple<string, dynamic>("SSHHost", "localhost")},
                {sshUsernameTextBox, new Tuple<string, dynamic>("SSHUsername", "root")},
                {sshPasswordTextBox, new Tuple<string, dynamic>("SSHPassword", string.Empty)},
                {sshPortNumericUpDown, new Tuple<string, dynamic>("SSHPort", 22)},
                {sshLocalPortNumericUpDown, new Tuple<string, dynamic>("SSHLocalPort", 3307)},
                {dbEnabledCheckBox, new Tuple<string, dynamic>("DBEnabled", false)},
                {serverTextBox, new Tuple<string, dynamic>("Server", "localhost")},
                {portNumericUpDown, new Tuple<string, dynamic>("Port", "3306")},
                {usernameTextBox, new Tuple<string, dynamic>("Username", "root")},
                {passwordTextBox, new Tuple<string, dynamic>("Password", string.Empty)},
                {databaseTextBox, new Tuple<string, dynamic>("WPPDatabase", "WPP")},
                {databaseWTextBox, new Tuple<string, dynamic>("TDBDatabase", "world")},
                {charSetComboBox, new Tuple<string, dynamic>("CharacterSet", CharacterSet.UTF8)},
            };

            try
            {
                _configuration = new Settings();
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show(ex.Message, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);

                Close();
            }

            LoadSettings();

        }

        private void LoadSettings()
        {
            foreach (var element in _defaultSettings)
            {
                try
                {
                    var val = _configuration.GetSetting(element.Value.Item1, element.Value.Item2);

                    if (element.Key is CheckBox) // special case for checkboxes, changing "Text" is not correct
                        ((CheckBox)element.Key).Checked = Convert.ToBoolean(val);
                    else if (String.Equals(element.Value.Item1, "SQLOutput"))
                        UpdateSQLOutputCheckBoxes(Convert.ToUInt32(val));
                    else
                        element.Key.Text = val.ToString();
                }
                catch
                {
                    MessageBox.Show(String.Format("Setting: {0}, default: {1}", element.Value.Item1, element.Value.Item2), "Failed to get setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveSettings()
        {
            foreach (var element in _defaultSettings)
            {
                string val;
                if (element.Key is CheckBox)
                    val = ((CheckBox)element.Key).Checked ? "true" : "false";
                else if (element.Value.Item2 is Enum)
                {
                    var a = Enum.Parse(element.Value.Item2.GetType(), element.Key.Text);
                    var enumType = element.Value.Item2.GetType();
                    var undertype = Enum.GetUnderlyingType(enumType);
                    val = Convert.ChangeType(a, undertype).ToString();
                }
                else if (String.Equals(element.Value.Item1, "SQLOutput"))
                    val = SQLOutputMask.ToString();
                else
                    val = element.Key.Text;

                _configuration.SetSetting(element.Value.Item1, val);
            }

            _configuration.Save();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            SaveSettings();
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
