using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Settings
    {
        private static readonly Configuration Conf = new Configuration();

        public static readonly string[] Filters = Conf.GetStringList("Filters", new string[0]);
        public static readonly string[] IgnoreFilters = Conf.GetStringList("IgnoreFilters", new string[0]);
        public static readonly string[] IgnoreByEntryFilters = Conf.GetStringList("IgnoreByEntryFilters", new string[0]);
        public static readonly string[] MapFilters = Conf.GetStringList("MapFilters", new string[0]);
        public static readonly string[] AreaFilters = Conf.GetStringList("AreaFilters", new string[0]);
        public static readonly int FilterPacketsNum = Conf.GetInt("FilterPacketsNum", 0);
        public static readonly ClientVersionBuild ClientBuild = Conf.GetEnum("ClientBuild", ClientVersionBuild.Zero);
        public static readonly DumpFormatType DumpFormat = Conf.GetEnum("DumpFormat", DumpFormatType.Text);
        public static readonly UInt64 SQLOutputFlag = GetSQLOutputFlag();
        public static readonly UInt64 HotfixSQLOutputFlag = GetHotfixSQLOutputFlag();
        public static readonly bool SQLOrderByKey = Conf.GetBoolean("SqlOrderByKey", false);
        public static readonly string SQLFileName = Conf.GetString("SQLFileName", string.Empty);
        public static readonly bool ShowEndPrompt = Conf.GetBoolean("ShowEndPrompt", false);
        public static readonly bool LogErrors = Conf.GetBoolean("LogErrors", false);
        public static readonly bool LogPacketErrors = Conf.GetBoolean("LogPacketErrors", false);
        public static readonly ParsedStatus OutputFlag = Conf.GetEnum("OutputFlag", ParsedStatus.All);
        public static readonly bool DebugReads = Conf.GetBoolean("DebugReads", false);
        public static readonly bool ParsingLog = Conf.GetBoolean("ParsingLog", false);
        public static readonly bool DevMode = Conf.GetBoolean("DevMode", false);
        public static readonly int Threads = Conf.GetInt("Threads", 8);

        public static readonly bool SSHEnabled = Conf.GetBoolean("SSHEnabled", false);
        public static readonly string SSHHost = Conf.GetString("SSHHost", "localhost");
        public static readonly string SSHUsername = Conf.GetString("SSHUsername", string.Empty);
        public static readonly string SSHPassword = Conf.GetString("SSHPassword", string.Empty);
        public static readonly int SSHPort = Conf.GetInt("SSHPort", 22);
        public static readonly int SSHLocalPort = Conf.GetInt("SSHLocalPort", 3307);

        public static readonly bool DBEnabled = Conf.GetBoolean("DBEnabled", false);
        public static readonly string Server = Conf.GetString("Server", "localhost");
        public static readonly string Port = Conf.GetString("Port", "3306");
        public static readonly string Username = Conf.GetString("Username", "root");
        public static readonly string Password = Conf.GetString("Password", string.Empty);
        public static readonly string WPPDatabase = Conf.GetString("WPPDatabase", "WPP");
        public static readonly string TDBDatabase = Conf.GetString("TDBDatabase", "world");
        public static readonly string HotfixesDatabase = Conf.GetString("HotfixesDatabase", "hotfixes");
        public static readonly string CharacterSet = Conf.GetString("CharacterSet", "utf8");

        private static UInt64 GetSQLOutputFlag()
        {
            var names = Enum.GetNames(typeof(SQLOutput));
            var values = Enum.GetValues(typeof(SQLOutput));

            var result = 0ul;

            for (var i = 0; i < names.Length; ++i)
            {
                if (Conf.GetBoolean(names[i], false))
                    result += (1ul << (int)values.GetValue(i));
            }

            return result;
        }

        private static UInt64 GetHotfixSQLOutputFlag()
        {
            var names = Enum.GetNames(typeof(HotfixSQLOutput));
            var values = Enum.GetValues(typeof(HotfixSQLOutput));

            var result = 0ul;

            for (var i = 0; i < names.Length; ++i)
            {
                if (Conf.GetBoolean(names[i], false))
                    result += (1ul << (int)values.GetValue(i));
            }

            return result;
        }

        public static bool DumpFormatWithText()
        {
            return DumpFormat != DumpFormatType.SqlOnly && DumpFormat != DumpFormatType.SniffDataOnly;
        }

        public static bool DumpFormatWithSQL()
        {
            return DumpFormat == DumpFormatType.SniffDataOnly ||
                   DumpFormat == DumpFormatType.SqlOnly ||
                   DumpFormat == DumpFormatType.Text;
        }
    }
}
