using System;
using System.Collections.Generic;
using System.Text;

namespace WowPacketParser.SQL
{
    public static class SQLUtil
    {
        public static readonly string CommaSeparator = ", ";

        public static string AddBackQuotes(string str)
        {
            return "`" + str + "`";
        }

        public static string AddQuotes(string str)
        {
            return "'" + str + "'";
        }

        /// <summary>
        /// Escapes a SQL string
        /// </summary>
        public static string EscapeString(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        /// <summary>
        /// Replaces the last entry of a given character by some other char
        /// Useful when replacing comma by semicolon
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldChar"></param>
        /// <param name="newChar"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Escapes and quotes a string
        /// </summary>
        public static string Stringify(object str)
        {
            if (str == null)
                str = string.Empty;
            return AddQuotes(EscapeString(str.ToString()));
        }

        /// <summary>
        /// Converts an int to a string hex.
        /// </summary>
        public static string Hexify(int n)
        {
            return "0x" + n.ToString("X");
        }

        /// <summary>
        /// Creates a SQL DELETE query:
        /// "DELETE FROM `tableName` WHERE `primaryKey` IN (entry1, entry2, ..., entryN);"
        /// </summary>
        public static string DeleteQuerySingle(ICollection<uint> entries, string primaryKey, string tableName)
        {
            var result = "DELETE FROM " + AddBackQuotes(tableName) + " WHERE " +
                         AddBackQuotes(primaryKey) + " IN (";

            var iter = 0;
            foreach (var entry in entries)
            {
                iter++;
                result += entry;
                if (entries.Count != iter)
                    result += CommaSeparator;
            }

            result += ");" + Environment.NewLine;

            return result;
        }

        /// <summary>
        /// Creates a SQL DELETE query:
        /// "DELETE FROM `tableName` WHERE (`primaryKey[0]`=entries1[0] AND `primaryKey[1]`=entries1[1]) OR ..."
        /// </summary>
        public static string DeleteQueryDouble(ICollection<uint[]> entries, string[] primaryKeys, string tableName)
        {
            var result = "DELETE FROM " + AddBackQuotes(tableName) + " WHERE ";

            var iter = 0;
            foreach (var tuple in entries)
            {
                iter++;
                result += "(" +
                          AddBackQuotes(primaryKeys[0]) + "=" + tuple[0] + " AND " +
                          AddBackQuotes(primaryKeys[1]) + "=" + tuple[1] + ")";
                if (entries.Count != iter)
                    result += " OR ";
            }

            result += ";" + Environment.NewLine;

            return result;
        }

        /// <summary>
        /// Creates the upper part of a SQL INSERT query:
        /// "INSERT INTO `tableName` (`column1`,`column2`, ..., `columnN`) VALUES"
        /// </summary>
        public static string InsertQueryHeader(ICollection<string> tableStructure, string tableName, string insertQuery = "INSERT INTO")
        {
            var result = insertQuery + " " + AddBackQuotes(tableName) + " (";
            var iter = 0;
            foreach (var column in tableStructure)
            {
                iter++;
                result += AddBackQuotes(column);
                if (tableStructure.Count != iter)
                    result += CommaSeparator;
            }
            result += ") VALUES" + Environment.NewLine;

            return result;
        }
    }
}
