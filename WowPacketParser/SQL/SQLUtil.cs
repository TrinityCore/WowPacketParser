using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public static class SQLUtil
    {
        /// <summary>
        /// Defines that values in insert queries should be spaced
        /// </summary>
        public const string CommaSeparator = ", ";

        /// <summary>
        /// Adds back quotes to a string. For SQL table and field names.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddBackQuotes(string str)
        {
            return "`" + str + "`";
        }

        /// <summary>
        /// Adds "straight" quotes to a string. For SQL text.
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Modified string</returns>
        public static string AddQuotes(string str)
        {
            return "'" + str + "'";
        }

        /// <summary>
        /// Escapes a SQL string
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Modified string</returns>
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
                str = String.Empty;
            return AddQuotes(EscapeString(str.ToString()));
        }

        /// <summary>
        /// Converts an int to a hex string.
        /// </summary>
        public static string Hexify(int n)
        {
            return "0x" + n.ToString("X");
        }

        /// <summary>
        /// Converts an uint to a hex string.
        /// </summary>
        public static string Hexify(uint n)
        {
            return "0x" + n.ToString("X");
        }

        /// <summary>
        /// "Modifies" any value to be used in SQL data
        /// </summary>
        /// <param name="value">Any value (string, number, enum, ...)</param>
        /// <param name="isFlag">If set to true the value, "0x" will be append to value</param>
        /// <param name="noQuotes">If value is a string and this is set to true, value will not be 'quoted' (SQL variables)</param>
        /// <returns></returns>
        public static object ToSQLValue(object value, bool isFlag = false, bool noQuotes = false)
        {
            //if (value == null)
            //    return value; // mhmmm

            if (value is string && !noQuotes)
                value = Stringify(value);

            if (value is bool)
                value = value.Equals(true) ? 1 : 0;

            if (value is Enum)
            {
                var enumType = value.GetType();
                var undertype = Enum.GetUnderlyingType(enumType);
                value = Convert.ChangeType(value, undertype);
            }

            if (value is int && isFlag)
                value = Hexify((int)value);

            if (value is uint && isFlag)
                value = Hexify((uint)value);

            return value;
        }

        /// <summary>
        /// <para>Compare two dictionaries (of the same types) and creates SQL inserts
        ///  or updates accordingly.</para>
        /// <remarks>Second dictionary can be null (only inserts queries will be produced)</remarks>
        /// </summary>
        /// <typeparam name="T">Type of the primary key (uint)</typeparam>
        /// <typeparam name="TK">Type of the WDB struct (field names and types must match DB field name and types)</typeparam>
        /// <param name="dict1">Dictionary retrieved from  parser</param>
        /// <param name="dict2">Dictionary retrieved from  DB</param>
        /// <param name="tableName">The name of the table in DB</param>
        /// <param name="storeType">Are we dealing with Spells, Quests, Units, ...?</param>
        /// <param name="primaryKeyName">The name of the primary key, usually "entry"</param>
        /// <returns>A string containing full SQL queries</returns>
        public static string CompareDicts<T, TK>(IDictionary<T, TK> dict1, IDictionary<T, TK> dict2, string tableName, StoreNameType storeType, string primaryKeyName = "entry")
        {
            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

            var fi = typeof(TK).GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var elem1 in dict1)
            {
                if (dict2 != null && dict2.ContainsKey(elem1.Key)) // update
                {
                    var row = new QueryBuilder.SQLUpdateRow();

                    foreach (var field in fi)
                    {
                        var elem2 = dict2[elem1.Key];

                        var val1 = field.GetValue(elem1.Value);
                        var val2 = field.GetValue(elem2);

                        if (!Utilities.EqualValues(val1, val2))
                            row.AddValue(field.Name, val1);
                    }

                    row.AddWhere(primaryKeyName, Convert.ToUInt32(elem1.Key));
                    row.Comment = StoreGetters.GetName(storeType, Convert.ToInt32(elem1.Key), false);
                    row.Table = tableName;

                    if (row.ValueCount != 0)
                        rowsUpd.Add(row);
                }
                else // insert new
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue(primaryKeyName, elem1.Key);
                    row.Comment = StoreGetters.GetName(storeType, Convert.ToInt32(elem1.Key), false);

                    foreach (var field in fi)
                        row.AddValue(field.Name, field.GetValue(elem1.Value));
                    rowsIns.Add(row);
                }
            }

            var result = new QueryBuilder.SQLInsert(tableName, rowsIns).Build() +
                         new QueryBuilder.SQLUpdate(rowsUpd).Build();

            return result;
        }
    }
}
