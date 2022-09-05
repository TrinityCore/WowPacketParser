using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public class Settings
    {
        static Settings _instance;
        public static Settings Instance
        {
            get => _instance;
        }
        private Settings() { }
        public void Initialize(IConfiguration Configuration = null)
        {
            _instance = new Settings()
            {
                _configuration = Configuration,
                Conf = new Configuration(_configuration)
            };
        }
    

        private IConfiguration _configuration;

        private Configuration Conf = new Configuration();

        public string[] Filters { get => Conf.GetStringList("Filters", new string[0]); }
        public string[] IgnoreFilters
        {
            get => Conf.GetStringList("IgnoreFilters", new string[0]);
        }
        public string[] IgnoreByEntryFilters
        {
            get => Conf.GetStringList("IgnoreByEntryFilters", new string[0]);
        }
        public string[] MapFilters
        {
            get => Conf.GetStringList("MapFilters", new string[0]);
        }
        public string[] AreaFilters
        {
            get => Conf.GetStringList("AreaFilters", new string[0]);
        }
        public int FilterPacketsNum
        {
            get => Conf.GetInt("FilterPacketsNum", 0);
        }
        public ClientVersionBuild ClientBuild
        {
            get => Conf.GetEnum("ClientBuild", ClientVersionBuild.Zero);
        }
        public LocaleConstant ClientLocale
        {
            get => Conf.GetEnum("ClientLocale", LocaleConstant.enUS);
        }
        public TargetedDatabase TargetedDatabase
        {
            get => Conf.GetEnum("TargetedDatabase", TargetedDatabase.WrathOfTheLichKing);
        }
        public DumpFormatType DumpFormat
        {
            get => Conf.GetEnum("DumpFormat", DumpFormatType.Text);
        }   
        public ulong SQLOutputFlag { get => GetSQLOutputFlag(); }
        public bool SQLOrderByKey
        {
            get => Conf.GetBoolean("SqlOrderByKey", false);
        }
        public bool SaveTempSpawns
        {
            get => Conf.GetBoolean("SaveTempSpawns", true);
        }
        public bool SkipOnlyVerifiedBuildUpdateRows
        {
            get => Conf.GetBoolean("SkipOnlyVerifiedBuildUpdateRows", false);
        }
        public bool SkipRowsWithFallbackValues
        {
            get => Conf.GetBoolean("SkipRowsWithFallbackValues", true);
        }
        public bool IgnoreZeroValues
        {
            get => Conf.GetBoolean("IgnoreZeroValues", false);
        }
        public bool ForceInsertQueries
        {
            get => Conf.GetBoolean("ForceInsertQueries", false);
        }
        public bool RecalcDiscount
        {
            get => Conf.GetBoolean("RecalcDiscount", false);
        }
        public bool ForcePhaseZero
        {
            get => Conf.GetBoolean("ForcePhaseZero", false);
        }
        public string SQLFileName
        {
            get => Conf.GetString("SQLFileName", string.Empty);
        }
        public bool SplitSQLFile
        {
            get => Conf.GetBoolean("SplitSQLFile", false);
        }
        public bool ShowEndPrompt
        {
            get => Conf.GetBoolean("ShowEndPrompt", false);
        }
        public bool LogErrors
        {
            get => Conf.GetBoolean("LogErrors", false);
        }
        public bool LogPacketErrors
        {
            get => Conf.GetBoolean("LogPacketErrors", false);
        }
        public ParsedStatus OutputFlag
        {
            get => Conf.GetEnum("OutputFlag", ParsedStatus.All);
        }
        public bool DebugReads
        {
            get => Conf.GetBoolean("DebugReads", false);
        }
        public bool ParsingLog
        {
            get => Conf.GetBoolean("ParsingLog", false);
        }
        public bool DevMode
        {
            get => Conf.GetBoolean("DevMode", false);
        }
        public int Threads
        {
            get => Conf.GetInt("Threads", 8);
        }
        public bool ParseAllHotfixes
        {
            get => Conf.GetBoolean("ParseAllHotfixes", false);
        }

        public bool SSHEnabled
        {
            get => Conf.GetBoolean("SSHEnabled", false);
        }
        public string SSHHost
        {
            get => Conf.GetString("SSHHost", "localhost");
        }
        public string SSHUsername
        {
            get => Conf.GetString("SSHUsername", string.Empty);
        }
        public string SSHPassword
        {
            get => Conf.GetString("SSHPassword", string.Empty);
        }
        public int SSHPort
        {
            get => Conf.GetInt("SSHPort", 22);
        }
        public int SSHLocalPort
        {
            get => Conf.GetInt("SSHLocalPort", 3307);
        }

        public bool DBEnabled
        {
            get => Conf.GetBoolean("DBEnabled", false);
        }
        public string Server
        {
            get => Conf.GetString("Server", "localhost");
        }
        public string Port
        {
            get => Conf.GetString("Port", "3306");
        }
        public string Username
        {
            get => Conf.GetString("Username", "root");
        }
        public string Password
        {
            get => Conf.GetString("Password", string.Empty);
        }
        public string WPPDatabase
        {
            get => Conf.GetString("WPPDatabase", "WPP");
        }
        public string TDBDatabase
        {
            get => Conf.GetString("TDBDatabase", "world");
        }
        public string HotfixesDatabase
        {
            get => Conf.GetString("HotfixesDatabase", "hotfixes");
        }
        public string CharacterSet
        {
            get => Conf.GetString("CharacterSet", "utf8");
        }

        // DB2
        public string DBCPath
        {
            get => Conf.GetString("DBCPath", $@"\dbc");
        }
        public string DBCLocale
        {
            get => Conf.GetString("DBCLocale", "enUS");
        }
        public string HotfixCachePath
        {
            get => Conf.GetString("HotfixCachePath", $@"\cache\DBCache.bin");
        }
        public bool UseDBC
        {
            get => Conf.GetBoolean("UseDBC", false);
        }
        public bool ParseSpellInfos
        {
            get => Conf.GetBoolean("ParseSpellInfos", false);
        }

        private ulong GetSQLOutputFlag()
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

        public bool DumpFormatWithText()
        {
            return DumpFormat != DumpFormatType.SqlOnly &&
                   DumpFormat != DumpFormatType.SniffDataOnly &&
                   DumpFormat != DumpFormatType.UniversalProto;
        }

        public bool DumpFormatWithTextToFile()
        {
            return DumpFormat != DumpFormatType.SqlOnly &&
                   DumpFormat != DumpFormatType.SniffDataOnly &&
                   DumpFormat != DumpFormatType.UniversalProto &&
                   DumpFormat != DumpFormatType.UniversalProtoWithText;
        }

        public bool DumpFormatWithSQL()
        {
            return DumpFormat == DumpFormatType.SniffDataOnly ||
                   DumpFormat == DumpFormatType.SqlOnly ||
                   DumpFormat == DumpFormatType.Text;
        }

        internal void Remove(string key)
        {
            Conf.Remove(key);
        }

        internal void Add(string key, string value)
        {
            Conf.Add(key,value);
        }
    }
}
