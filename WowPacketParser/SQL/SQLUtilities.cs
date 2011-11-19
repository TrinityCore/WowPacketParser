using System.Text;

namespace WowPacketParser.SQL
{
    public static class SQLUtilities
    {
        public static string AddBackQuotes(string str)
        {
            return "`" + str + "`";
        }

        public static string AddQuotes(string str)
        {
            return "'" + str + "'";
        }

        public static string EscapeString(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        public static StringBuilder ReplaceLast(this StringBuilder str, char oldChar, char newChar)
        {
            for (int i = str.Length - 1; i > 0; i--)
                if (str[i] == oldChar)
                {
                    str[i] = newChar;
                    break;
                }
            return str;
        }
    }
}
