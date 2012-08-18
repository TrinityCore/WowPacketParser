using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using PacketParser.Enums;
using PacketDumper.Enums;
using PacketParser.Misc;

namespace PacketDumper.Misc
{
    public static class Settings
    {
        static Settings()
        {
            ParserSettings.SSHTunnel.Enabled = GetBoolean("SSHEnabled", false);
            ParserSettings.SSHTunnel.Host = GetString("SSHHost", "localhost");
            ParserSettings.SSHTunnel.Username = GetString("SSHUsername", string.Empty);
            ParserSettings.SSHTunnel.Password = GetString("SSHPassword", string.Empty);
            ParserSettings.SSHTunnel.Port = GetInt32("SSHPort", 22);
            ParserSettings.SSHTunnel.LocalPort = GetInt32("SSHLocalPort", 3307);

            ParserSettings.MySQL.Enabled = GetBoolean("DBEnabled", false);
            ParserSettings.MySQL.Server = GetString("Server", "localhost");
            ParserSettings.MySQL.Port = GetString("Port", "3306");
            ParserSettings.MySQL.Username = GetString("Username", "root");
            ParserSettings.MySQL.Password = GetString("Password", string.Empty);
            ParserSettings.MySQL.PacketParserDB = GetString("PacketParserDB", "WPP");
            ParserSettings.MySQL.TDBDB = GetString("TDBDatabase", "world");
            ParserSettings.MySQL.CharacterSet = GetString("CharacterSet", "utf8");

            ParserSettings.ReadDebugValues = GetBoolean("DebugReads", false);
        }
        private static readonly KeyValueConfigurationCollection SettingsCollection = GetConfiguration();

        public static readonly string[] ReaderFilterOpcode = GetStringList("ReaderFilterOpcode", new string[0]);
        public static readonly string[] ReaderFilterIgnoreOpcode = GetStringList("ReaderFilterIgnoreOpcode", new string[0]);
        public static readonly int ReaderFilterPacketsNum = GetInt32("ReaderFilterPacketsNum", 0);
        public static readonly int ReaderFilterPacketNumLow = GetInt32("ReaderFilterPacketNumLow", 0);
        public static readonly int ReaderFilterPacketNumHigh = GetInt32("ReaderFilterPacketNumHigh", 0);

        public static readonly string[] TextOutputFilterIgnoreEntry = GetStringList("TextOutputFilterIgnoreEntry", new string[0]);
        public static readonly string[] SpawnDumpFilterArea = GetStringList("SpawnDumpFilterArea", new string[0]);

        public static readonly ClientVersionBuild ClientBuild = GetEnum("ClientBuild", ClientVersionBuild.Zero);
        public static readonly string PacketFileType = GetString("PacketFileType", string.Empty);
        public static readonly string RawOutputType = GetString("RawOutputType", string.Empty);
        public static readonly bool SplitRawOutput = GetBoolean("SplitRawOutput", false);
        public static readonly bool TextOutput = GetBoolean("TextOutput", false);
        public static readonly SQLOutputFlags SQLOutput = GetEnum("SQLOutput", SQLOutputFlags.None);
        public static readonly string SQLFileName = GetString("SQLFileName", string.Empty);
        public static readonly bool ShowEndPrompt = GetBoolean("ShowEndPrompt", false);
        public static readonly bool LogPacketErrors = GetBoolean("LogPacketErrors", false);
        public static readonly bool LogEnumErrors = GetBoolean("LogEnumErrors", false);
        public static readonly bool ParsingLog = GetBoolean("ParsingLog", false);

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
    }
}
