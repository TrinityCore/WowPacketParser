using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketParser.Misc
{
    public static class ParserSettings
    {
        public static class MySQL
        {
            public static bool Enabled = false;
            public static string Username;
            public static string Password;
            public static string Server;
            public static string Port;
            public static string CharacterSet;
            public static string PacketParserDB;
            public static string TDBDB;
        }

        public static class SSHTunnel
        {
            public static bool Enabled = false;
            public static string Username;
            public static string Password;
            public static string Host;
            public static int Port;
            public static int LocalPort;
        }

        public static bool LogEnumErrors = false;
        public static bool ReadDebugValues = false;
    }
}
