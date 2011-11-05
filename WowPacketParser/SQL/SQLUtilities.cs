namespace WowPacketParser.SQL
{
    public static class SQLUtilities
    {
        public static string AddBackQuotes(string collumn)
        {
            return "`" + collumn + "`";
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
    }
}
