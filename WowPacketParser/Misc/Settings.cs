using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Settings
    {
        private static readonly KeyValueConfigurationCollection SettingsCollection = GetConfiguration();

        public static readonly string[] Filters = GetStringList("Filters", new string[0]);
        public static readonly string[] IgnoreFilters = GetStringList("IgnoreFilters", new string[0]);
        public static readonly string[] IgnoreByEntryFilters = GetStringList("IgnoreByEntryFilters", new string[0]);
        public static readonly string[] AreaFilters = GetStringList("AreaFilters", new string[0]);
        public static readonly int FilterPacketNumLow = GetInt32("FilterPacketNumLow", 0);
        public static readonly int FilterPacketNumHigh = GetInt32("FilterPacketNumHigh", 0);
        public static readonly int FilterPacketsNum = GetInt32("FilterPacketsNum", 0);
        public static readonly ClientVersionBuild ClientBuild = GetEnum("ClientBuild", ClientVersionBuild.Zero);
        public static readonly DumpFormatType DumpFormat = GetEnum("DumpFormat", DumpFormatType.Text);
        public static readonly int SQLOutputFlag = GetSQLOutputFlag();
        public static readonly string SQLFileName = GetString("SQLFileName", string.Empty);
        public static readonly bool ShowEndPrompt = GetBoolean("ShowEndPrompt", false);
        public static readonly bool LogErrors = GetBoolean("LogErrors", false);
        public static readonly bool LogPacketErrors = GetBoolean("LogPacketErrors", false);
        public static readonly bool DebugReads = GetBoolean("DebugReads", false);
        public static readonly bool ParsingLog = GetBoolean("ParsingLog", false);

        public static readonly bool SSHEnabled = GetBoolean("SSHEnabled", false);
        public static readonly string SSHHost = GetString("SSHHost", "localhost");
        public static readonly string SSHUsername = GetString("SSHUsername", string.Empty);
        public static readonly string SSHPassword = GetString("SSHPassword", string.Empty);
        public static readonly int SSHPort = GetInt32("SSHPort", 22);
        public static readonly int SSHLocalPort = GetInt32("SSHLocalPort", 3307);

        public static readonly bool DBEnabled = GetBoolean("DBEnabled", false);
        public static readonly string Server = GetString("Server", "localhost");
        public static readonly string Port = GetString("Port", "3306");
        public static readonly string Username = GetString("Username", "root");
        public static readonly string Password = GetString("Password", string.Empty);
        public static readonly string WPPDatabase = GetString("WPPDatabase", "WPP");
        public static readonly string TDBDatabase = GetString("TDBDatabase", "world");
        public static readonly string CharacterSet = GetString("CharacterSet", "utf8");

        private static KeyValueConfigurationCollection GetConfiguration()
        {
            var args = Environment.GetCommandLineArgs();
            var opts = new Dictionary<string, string>();
            string configFile = null;
            KeyValueConfigurationCollection settings = null;
            for (var i = 1; i < args.Length - 1; ++i)
            {
                var opt = args[i];
                if (opt[0] != '/')
                    break;

                // analyze options
                var optname = opt.Substring(1);
                switch (optname)
                {
                    case "ConfigFile":
                        configFile = args[i + 1];
                        break;
                    default:
                        opts.Add(optname, args[i + 1]);
                        break;
                }
                ++i;
            }
            // load different config file
            if (configFile != null)
            {
                string configPath = System.IO.Path.Combine(Environment.CurrentDirectory, configFile);

                try
                {
                    // Map the new configuration file.
                    var configFileMap = new ExeConfigurationFileMap {ExeConfigFilename = configPath};

                    // Get the mapped configuration file
                    var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                    settings = ((AppSettingsSection)config.GetSection("appSettings")).Settings;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not load config file {0}, reason: {1}", configPath,  ex.Message);
                }
            }
            if (settings == null)
                settings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;

            // override config options with options from command line
            foreach(var pair in opts)
            {
                settings.Remove(pair.Key);
                settings.Add(pair.Key, pair.Value);
            }

            return settings;
        }

        private static string GetString(string key, string defValue)
        {
            var s = SettingsCollection[key];
            return (s == null || s.Value == null) ? defValue : s.Value;
        }

        private static string[] GetStringList(string key, string[] defValue)
        {
            var s = SettingsCollection[key];

            if (s == null || s.Value == null)
                return defValue;

            var arr = s.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < arr.Length; i++)
                arr[i] = arr[i].Trim();

            return arr;
        }

        private static bool GetBoolean(string key, bool defValue)
        {
            bool aux;
            var s = SettingsCollection[key];
            if ((s == null || s.Value == null) || !bool.TryParse(s.Value, out aux))
                aux = defValue;

            return aux;
        }

        private static int GetInt32(string key, int defValue)
        {
            int aux;
            var s = SettingsCollection[key];
            if ((s == null || s.Value == null) || !int.TryParse(s.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out aux))
                aux = defValue;

            return aux;
        }

        private static T GetEnum<T>(string key, T defValue)
        {
            object aux;

            var s = SettingsCollection[key];
            if ((s == null || s.Value == null))
                aux = defValue;
            else
            {
                int value;
                if (!int.TryParse(s.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    aux = defValue;
                else
                    aux = value;
            }

            return (T)aux;
        }

        private static int GetSQLOutputFlag()
        {
            var names = Enum.GetNames(typeof(SQLOutput));
            var values = Enum.GetValues(typeof(SQLOutput));

            var result = 0;

            for (var i = 0; i < names.Length; ++i)
            {
                if (GetBoolean(names[i], false))
                    result += (1 << (int)values.GetValue(i));
            }

            return result;
        }

        public static bool DumpFormatWithText()
        {
            return DumpFormat != DumpFormatType.SqlOnly && DumpFormat != DumpFormatType.SniffDataOnly;
        }
    }
}
