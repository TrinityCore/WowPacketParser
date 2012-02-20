using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettingsUI
{
    static public class SettingsManager
    {
        public static string Filters;
        public static string IgnoreFilters;
        public static string IgnoreByEntryFilters;
        public static string AreaFilters;
        public static string SQLFileName;

        public static int FilterPacketsNum;
        public static int FilterPacketNumLow;
        public static int FilterPacketNumHigh;
        public static int ClientBuild;
        public static int ThreadsRead;
        public static int ThreadsParse;
        public static int SQLOutput;

        public static bool ShowEndPrompt;
        public static bool LogErrors;
        
        public static bool SplitOutput;
        public static bool DebugReads;
        public static bool ParsingLog;

        public static DumpFormat DumpFormat;
        public static StatsOutput StatsOutput;

        public static bool SSHEnabled;
        public static string SSHHost;
        public static string SSHUsername;
        public static string SSHPassword;
        public static int SSHPort;
        public static int SSHLocalPort;

        public static bool DBEnabled;
        public static string Server;
        public static int Port;
        public static string Username;
        public static string Password;
        public static string Database;
        public static CharacterSet CharacterSet;

        public void DefaultSettings()
        {
            
        }
    }
}
