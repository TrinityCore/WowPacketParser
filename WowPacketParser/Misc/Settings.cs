using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Settings
    {
        private static readonly NameValueCollection SettingsCollection = ConfigurationManager.AppSettings;

        public static readonly string[] Filters = GetStringList("Filters", new string[0]);
        public static readonly string[] IgnoreFilters = GetStringList("IgnoreFilters", new string[0]);
        public static readonly string[] IgnoreByEntryFilters = GetStringList("IgnoreByEntryFilters", new string[0]);
        public static readonly string[] AreaFilters = GetStringList("AreaFilters", new string[0]);
        public static readonly int FilterPacketNumLow = GetInt32("FilterPacketNumLow", 0);
        public static readonly int FilterPacketNumHigh = GetInt32("FilterPacketNumHigh", 0);
        public static readonly int FilterPacketsNum = GetInt32("FilterPacketsNum", 0);
        public static readonly ClientVersionBuild ClientBuild = GetEnum("ClientBuild", ClientVersionBuild.Zero);
        public static readonly int ThreadsRead = GetInt32("Threads.Read", 0);
        public static readonly int ThreadsParse = GetInt32("Threads.Parse", 0);
        public static readonly DumpFormatType DumpFormat = GetEnum("DumpFormat", DumpFormatType.Text);
        public static readonly StatsOutputFlags StatsOutput = GetEnum("StatsOutput", StatsOutputFlags.Local);
        public static readonly SQLOutputFlags SQLOutput = GetEnum("SQLOutput", SQLOutputFlags.None);
        public static readonly string SQLFileName = GetString("SQLFileName", string.Empty);
        public static readonly bool ShowEndPrompt = GetBoolean("ShowEndPrompt", false);
        public static readonly bool LogErrors = GetBoolean("LogErrors", false);
        public static readonly bool DebugReads = GetBoolean("DebugReads", false);
        public static readonly bool SplitOutput = GetBoolean("SplitOutput", false);
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

        private static string GetString(string key, string defValue)
        {
            var aux = SettingsCollection[key];
            return aux ?? defValue;
        }

        private static string[] GetStringList(string key, string[] defValue)
        {
            var s = SettingsCollection[key];
            return s == null ? defValue : s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static bool GetBoolean(string key, bool defValue)
        {
            bool aux;
            var s = SettingsCollection[key];
            if (s == null || !bool.TryParse(s, out aux))
                aux = defValue;

            return aux;
        }

        private static int GetInt32(string key, int defValue)
        {
            int aux;
            var s = SettingsCollection[key];
            if (s == null || !int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out aux))
                aux = defValue;

            return aux;
        }

        public static float GetFloat(string key, float defValue)
        {
            float aux;
            var s = SettingsCollection[key];
            if (s == null || !float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out aux))
                aux = defValue;

            return aux;
        }

        private static T GetEnum<T>(string key, T defValue)
        {
            object aux;

            var s = SettingsCollection[key];
            if (s == null)
                aux = defValue;
            else
            {
                int value;
                if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    aux = defValue;
                else
                    aux = value;
            }

            return (T)aux;
        }
    }
}
