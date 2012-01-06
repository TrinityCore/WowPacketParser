using System;
using System.Configuration;
using System.Globalization;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Settings
    {
        public static readonly string[] Filters = _GetStringList("Filters", new string[0]);
        public static readonly string[] IgnoreFilters = _GetStringList("IgnoreFilters", new string[0]);
        public static readonly string[] IgnoreByEntryFilters = _GetStringList("IgnoreByEntryFilters", new string[0]);
        public static readonly int FilterPacketNumLow = _GetInt32("FilterPacketNumLow", 0);
        public static readonly int FilterPacketNumHigh = _GetInt32("FilterPacketNumHigh", 0);
        public static readonly int FilterPacketsNum = _GetInt32("FilterPacketsNum", 0);
        public static readonly ClientVersionBuild ClientBuild = _GetEnum<ClientVersionBuild>("ClientBuild", ClientVersionBuild.Zero);
        public static readonly int Threads = _GetInt32("Threads", 0);
        public static readonly DumpFormatType DumpFormat = _GetEnum<DumpFormatType>("DumpFormat", DumpFormatType.Text);
        public static readonly SQLOutputFlags SQLOutput = _GetEnum<SQLOutputFlags>("SQLOutput", SQLOutputFlags.None);
        public static readonly string SQLFileName = _GetString("SQLFileName", "");
        public static readonly bool ShowEndPrompt = _GetBoolean("ShowEndPrompt", false);
        public static readonly bool LogErrors = _GetBoolean("LogErrors", false);
        public static readonly bool DebugReads = _GetBoolean("DebugReads", false);

        public static readonly bool SSHEnabled = _GetBoolean("SSHEnabled", false);
        public static readonly string SSHHost = _GetString("SSHHost", "localhost");
        public static readonly string SSHUsername = _GetString("SSHUsername", "");
        public static readonly string SSHPassword = _GetString("SSHPassword", "");
        public static readonly int SSHPort = _GetInt32("SSHPort", 22);
        public static readonly int SSHLocalPort = _GetInt32("SSHLocalPort", 3307);

        public static readonly bool DBEnabled = _GetBoolean("DBEnabled", false);
        public static readonly string Server = _GetString("Server", "localhost");
        public static readonly string Port = _GetString("Port", "3306");
        public static readonly string Username = _GetString("Username", "root");
        public static readonly string Password = _GetString("Password", "");
        public static readonly string Database = _GetString("Database", "WPP");
        public static readonly string CharacterSet = _GetString("CharacterSet", "utf8");

        public static string _GetString(string key, string defValue)
        {
            string aux = ConfigurationManager.AppSettings[key];
            if (aux == null)
                return defValue;
            return aux;
        }

        public static string[] _GetStringList(string key, string[] defValue)
        {
            var s = ConfigurationManager.AppSettings[key];
            if (s == null)
                return defValue;
            else
                return s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool _GetBoolean(string key, bool defValue)
        {
            bool aux;
            var s = ConfigurationManager.AppSettings[key];
            if (s == null || !bool.TryParse(s, out aux))
                aux = defValue;

            return aux;
        }

        public static int _GetInt32(string key, int defValue)
        {
            int aux;
            var s = ConfigurationManager.AppSettings[key];
            if (s == null || !int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out aux))
                aux = defValue;

            return aux;
        }

        public static float _GetFloat(string key, float defValue)
        {
            float aux;
            var s = ConfigurationManager.AppSettings[key];
            if (s == null || !float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out aux))
                aux = defValue;

            return aux;
        }

        public static T _GetEnum<T>(string key, T defValue)
        {
            object aux;

            var s = ConfigurationManager.AppSettings[key];
            if (s == null)
                aux = defValue;
            else
            {
                int value;
                if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    aux = defValue;
                else
                    aux = (object)value;
            }

            return (T)aux;
        }
    }
}
