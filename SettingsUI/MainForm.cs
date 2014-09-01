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
                {ceCheckBox,   0x00000001}, // creature_equip_template
                {cmCheckBox,   0x00000002}, // creature_model_info
                {mCheckBox,    0x00000004}, // creature_movement
                {csCheckBox,   0x00000008}, // creature
                {ctCheckBox,   0x00000010}, // creature_template
                {ctaCheckBox,  0x00000020}, // creature_template_addon
                {ctxCheckBox,  0x00000040}, // creature_text
                {vtaCheckBox,  0x00000080}, // vehicle_template_accessory

                {dmCheckBox,   0x00000100}, // defense_message
                {godCheckBox,  0x00000200}, // gameobject_db2_position
                {gosCheckBox,  0x00000400}, // gameobject
                {gotCheckBox,  0x00000800}, // gameobject_template
                {pciCheckBox,  0x00001000}, // playercreateinfo
                {pciaCheckBox, 0x00002000}, // playercreateinfo_action
                {pcisCheckBox, 0x00004000}, // playercreateinfo_spell
                {wuCheckBox,   0x00008000}, // weather_updates

                {btCheckBox,   0x00010000}, // broadcast_text
                {gCheckBox,    0x00020000}, // gossip_menu
                {gmoCheckBox,  0x00040000}, // gossip_menu_option
                {onCheckBox,   0x00080000}, // ObjectNames
                {poiCheckBox,  0x00100000}, // points_of_interest
                {qpoiCheckBox, 0x00200000}, // quest_poi
                {qpoipCheckBox,0x00400000}, // quest_poi_points
                {qtCheckBox,   0x00800000}, // quest_template

                {itCheckBox,   0x01000000}, // item_template
                {lCheckBox,    0x02000000}, // LootTemplate
                {ntxtCheckBox, 0x04000000}, // npc_text
                {ntCheckBox,   0x08000000}, // npc_trainer
                {nvCheckBox,   0x10000000}, // npc_vendor
                {ptCheckBox,   0x20000000}, // page_text
                {sdCheckBox,   0x40000000}, // SniffData
                //{sdoCheckBox,  0x80000000}, // SniffDataOpcodes
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
                {logErrorCheckBox, new Tuple<string, dynamic>("LogErrors", false)},
                {logPacketErrorsCheckBox, new Tuple<string, dynamic>("LogPacketErrors", false)},
                {ceCheckBox, new Tuple<string, dynamic>("creature_equip_template", false)},
                {cmCheckBox, new Tuple<string, dynamic>("creature_model_info", false)},
                {mCheckBox, new Tuple<string, dynamic>("creature_movement", false)},
                {csCheckBox, new Tuple<string, dynamic>("creature", false)},
                {ctCheckBox, new Tuple<string, dynamic>("creature_template", false)},
                {ctaCheckBox, new Tuple<string, dynamic>("creature_template_addon", false)},
                {ctxCheckBox, new Tuple<string, dynamic>("creature_text", false)},
                {vtaCheckBox, new Tuple<string, dynamic>("vehicle_template_accessory", false)},

                {dmCheckBox, new Tuple<string, dynamic>("defense_message", false)},
                {godCheckBox, new Tuple<string, dynamic>("gameobject_db2_position", false)},
                {gosCheckBox, new Tuple<string, dynamic>("gameobject", false)},
                {gotCheckBox, new Tuple<string, dynamic>("gameobject_template", false)},
                {pciCheckBox, new Tuple<string, dynamic>("playercreateinfo", false)},
                {pciaCheckBox, new Tuple<string, dynamic>("playercreateinfo_action", false)},
                {pcisCheckBox, new Tuple<string, dynamic>("playercreateinfo_spell", false)},
                {wuCheckBox, new Tuple<string, dynamic>("weather_updates", false)},

                {btCheckBox, new Tuple<string, dynamic>("broadcast_text", false)},
                {gCheckBox, new Tuple<string, dynamic>("gossip_menu", false)},
                {gmoCheckBox, new Tuple<string, dynamic>("gossip_menu_option", false)},
                {onCheckBox, new Tuple<string, dynamic>("ObjectNames", false)},
                {poiCheckBox, new Tuple<string, dynamic>("points_of_interest", false)},
                {qpoiCheckBox, new Tuple<string, dynamic>("quest_poi", false)},
                {qpoipCheckBox, new Tuple<string, dynamic>("quest_poi_points", false)},
                {qtCheckBox, new Tuple<string, dynamic>("quest_template", false)},

                {itCheckBox, new Tuple<string, dynamic>("item_template", false)},
                {lCheckBox, new Tuple<string, dynamic>("LootTemplate", false)},
                {ntxtCheckBox, new Tuple<string, dynamic>("npc_text", false)},
                {ntCheckBox, new Tuple<string, dynamic>("npc_trainer", false)},
                {nvCheckBox, new Tuple<string, dynamic>("npc_vendor", false)},
                {ptCheckBox, new Tuple<string, dynamic>("page_text", false)},
                {sdCheckBox, new Tuple<string, dynamic>("SniffData", false)},
                //{sdoCheckBox, new Tuple<string, dynamic>("SniffDataOpcodes", false)},

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
