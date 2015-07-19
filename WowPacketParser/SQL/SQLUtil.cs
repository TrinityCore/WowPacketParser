using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

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
        /// Remove back quotes from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveBackQuotes(string str)
        {
            return str.Replace("`", "");
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
            str = MySqlHelper.DoubleQuoteString(str);
            str = str.Replace(Environment.NewLine, @"\n");
            str = str.Replace("’’", "’"); // french is gay ... 

            // prevent double escaping
            return str.Replace("\"\"", "\"");
        }

        /// <summary>
        /// Replaces the last entry of a given character by some other char
        /// Useful when replacing comma by semicolon
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldChar"></param>
        /// <param name="newChar"></param>
        /// <returns></returns>
        public static void ReplaceLast(this StringBuilder str, char oldChar, char newChar)
        {
            for (int i = str.Length - 1; i > 0; i--)
                if (str[i] == oldChar)
                {
                    str[i] = newChar;
                    break;
                }
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
        /// <remarks>Use DBTableName and DBFieldName attributes to specify table and field names, in TK</remarks>
        /// </summary>
        /// <typeparam name="T">Type of the primary key (uint)</typeparam>
        /// <typeparam name="TK">Type of the WDB struct (field types must match DB field)</typeparam>
        /// <param name="dict1">Dictionary retrieved from  parser</param>
        /// <param name="dict2">Dictionary retrieved from  DB</param>
        /// <param name="storeType">Are we dealing with Spells, Quests, Units, ...?</param>
        /// <param name="primaryKeyName">The name of the primary key, usually "entry"</param>
        /// <returns>A string containing full SQL queries</returns>
        public static string CompareDicts<T, TK>(StoreDictionary<T, TK> dict1, StoreDictionary<T, TK> dict2, StoreNameType storeType, string primaryKeyName = "entry")
        {
            var tableAttrs = (DBTableNameAttribute[])typeof(TK).GetCustomAttributes(typeof(DBTableNameAttribute), false);
            if (tableAttrs.Length <= 0)
                return string.Empty;
            var tableName = tableAttrs[0].Name;

            var fields = Utilities.GetFieldsAndAttribute<TK, DBFieldNameAttribute>();
            if (fields == null)
                return string.Empty;

            fields.RemoveAll(field => field.Item2.Name == null);

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

            foreach (var elem1 in Settings.SQLOrderByKey ? dict1.OrderBy(blub => blub.Key).ToList() : dict1.ToList())
            {
                if (dict2 != null && dict2.ContainsKey(elem1.Key)) // update
                {
                    var row = new QueryBuilder.SQLUpdateRow();

                    foreach (var field in fields)
                    {
                        var elem2 = dict2[elem1.Key];

                        var val1 = field.Item1.GetValue(elem1.Value.Item1);
                        var val2 = field.Item1.GetValue(elem2.Item1);

                        var arr1 = val1 as Array;
                        if (arr1 != null)
                        {
                            var arr2 = (Array) val2;

                            var isString = arr1.GetType().GetElementType() == typeof(string);

                            for (var i = 0; i < field.Item2.Count; i++)
                            {
                                var value1 = i >= arr1.Length ? (isString ? (object) string.Empty : 0) : arr1.GetValue(i);
                                var value2 = i >= arr2.Length ? (isString ? (object) string.Empty : 0) : arr2.GetValue(i);

                                if (!Utilities.EqualValues(value1, value2))
                                    row.AddValue(field.Item2.Name + (field.Item2.StartAtZero ? i : i + 1), value1);
                            }

                            continue;
                        }

                        if ((val2 is Array) && val1 == null)
                            continue;

                        if (!Utilities.EqualValues(val1, val2))
                            row.AddValue(field.Item2.Name, val1);
                    }

                    var key = Convert.ToUInt32(elem1.Key);

                    row.AddWhere(primaryKeyName, key);
                    row.Comment = StoreGetters.GetName(storeType, (int)key, false);
                    row.Table = tableName;

                    if (row.ValueCount == 0)
                        continue;

                    var lastField = fields[fields.Count - 1];
                    if (lastField.Item2.Name == "VerifiedBuild")
                    {
                        var buildvSniff = (int)lastField.Item1.GetValue(elem1.Value.Item1);
                        var buildvDB = (int)lastField.Item1.GetValue(dict2[elem1.Key].Item1);

                        if (buildvDB > buildvSniff) // skip update if DB already has a VerifiedBuild higher than this one
                            continue;
                    }

                    rowsUpd.Add(row);
                }
                else // insert new
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue(primaryKeyName, elem1.Key);
                    row.Comment = StoreGetters.GetName(storeType, Convert.ToInt32(elem1.Key), false);

                    foreach (var field in fields)
                    {
                        if (field.Item1.FieldType.BaseType == typeof(Array))
                        {
                            var arr = (Array)field.Item1.GetValue(elem1.Value.Item1);
                            if (arr == null)
                                continue;

                            for (var i = 0; i < arr.Length; i++)
                                row.AddValue(field.Item2.Name + (field.Item2.StartAtZero ? i : i + 1), arr.GetValue(i));

                            continue;
                        }

                        var val = field.Item1.GetValue(elem1.Value.Item1);
                        if (val == null && field.Item1.FieldType == typeof (string))
                            val = string.Empty;

                        row.AddValue(field.Item2.Name, val);
                    }
                    rowsIns.Add(row);
                }
            }

            var result = new QueryBuilder.SQLInsert(tableName, rowsIns, deleteDuplicates: false).Build() +
                         new QueryBuilder.SQLUpdate(rowsUpd).Build();

            return result;
        }

        /// <summary>
        /// <para>Compare two dictionaries (of the same types) and creates SQL inserts
        ///  or updates accordingly.</para>
        /// <remarks>Second dictionary can be null (only inserts queries will be produced)</remarks>
        /// <remarks>Use DBTableName and DBFieldName attributes to specify table and field names, in TK</remarks>
        /// </summary>
        /// <typeparam name="T">Type of the first primary key</typeparam>
        /// /// <typeparam name="TG">Type of the second primary key</typeparam>
        /// <typeparam name="TK">Type of the WDB struct (field names and types must match DB field name and types)</typeparam>
        /// <param name="dict1">Dictionary retrieved from  parser</param>
        /// <param name="dict2">Dictionary retrieved from  DB</param>
        /// <param name="storeType1">(T) Are we dealing with Spells, Quests, Units, ...?</param>
        /// <param name="storeType2">(TG) Are we dealing with Spells, Quests, Units, ...?</param>
        /// <param name="primaryKeyName1">The name of the first primary key</param>
        /// <param name="primaryKeyName2">The name of the second primary key</param>
        /// <returns>A string containing full SQL queries</returns>
        public static string CompareDicts<T, TG, TK>(StoreDictionary<Tuple<T, TG>, TK> dict1, StoreDictionary<Tuple<T, TG>, TK> dict2, StoreNameType storeType1, StoreNameType storeType2, string primaryKeyName1, string primaryKeyName2)
        {
            var tableAttrs = (DBTableNameAttribute[])typeof(TK).GetCustomAttributes(typeof(DBTableNameAttribute), false);
            if (tableAttrs.Length <= 0)
                return string.Empty;
            var tableName = tableAttrs[0].Name;

            var fields = Utilities.GetFieldsAndAttribute<TK, DBFieldNameAttribute>();
            if (fields == null)
                return string.Empty;

            fields.RemoveAll(field => field.Item2.Name == null);

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

            foreach (var elem1 in Settings.SQLOrderByKey ? dict1.OrderBy(blub => blub.Key).ToList() : dict1.ToList())
            {
                if (dict2 != null && dict2.ContainsKey(elem1.Key)) // update
                {
                    var row = new QueryBuilder.SQLUpdateRow();

                    foreach (var field in fields)
                    {
                        var elem2 = dict2[elem1.Key];

                        var val1 = field.Item1.GetValue(elem1.Value.Item1);
                        var val2 = field.Item1.GetValue(elem2.Item1);

                        var arr1 = val1 as Array;
                        if (arr1 != null)
                        {
                            var arr2 = (Array)val2;

                            var isString = arr1.GetType().GetElementType() == typeof(string);

                            for (var i = 0; i < field.Item2.Count; i++)
                            {
                                var value1 = i >= arr1.Length ? (isString ? (object)string.Empty : 0) : arr1.GetValue(i);
                                var value2 = i >= arr2.Length ? (isString ? (object)string.Empty : 0) : arr2.GetValue(i);

                                if (!Utilities.EqualValues(value1, value2))
                                    row.AddValue(field.Item2.Name + (field.Item2.StartAtZero ? i : i + 1), value1);
                            }

                            continue;
                        }

                        if (!Utilities.EqualValues(val1, val2))
                            row.AddValue(field.Item2.Name, val1);
                    }

                    var key1 = Convert.ToUInt32(elem1.Key.Item1);
                    var key2 = Convert.ToUInt32(elem1.Key.Item2);

                    row.AddWhere(primaryKeyName1, key1);
                    row.AddWhere(primaryKeyName2, key2);

                    var key1Name = storeType1 != StoreNameType.None ?
                        StoreGetters.GetName(storeType1, (int) key1, false) :
                        elem1.Key.Item1.ToString();
                    var key2Name = storeType2 != StoreNameType.None ?
                        StoreGetters.GetName(storeType2, (int) key2, false) :
                        elem1.Key.Item2.ToString();

                    row.Comment = key1Name + " - " + key2Name;
                    row.Table = tableName;

                    if (row.ValueCount == 0)
                        continue;

                    var lastField = fields[fields.Count - 1];
                    if (lastField.Item2.Name == "VerifiedBuild")
                    {
                        var buildvSniff = (int)lastField.Item1.GetValue(elem1.Value.Item1);
                        var buildvDB = (int)lastField.Item1.GetValue(dict2[elem1.Key].Item1);

                        if (buildvDB > buildvSniff) // skip update if DB already has a VerifiedBuild higher than this one
                            continue;
                    }

                    rowsUpd.Add(row);
                }
                else // insert new
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue(primaryKeyName1, elem1.Key.Item1);
                    row.AddValue(primaryKeyName2, elem1.Key.Item2);

                    var key1 = Convert.ToUInt32(elem1.Key.Item1);
                    var key2 = Convert.ToUInt32(elem1.Key.Item2);

                    var key1Name = storeType1 != StoreNameType.None ?
                        StoreGetters.GetName(storeType1, (int) key1, false) :
                        elem1.Key.Item1.ToString();
                    var key2Name = storeType2 != StoreNameType.None ?
                        StoreGetters.GetName(storeType2, (int) key2, false) :
                        elem1.Key.Item2.ToString();

                    row.Comment = key1Name + " - " + key2Name;

                    foreach (var field in fields)
                    {
                        if (field.Item1.FieldType.BaseType == typeof(Array))
                        {
                            var arr = (Array)field.Item1.GetValue(elem1.Value.Item1);
                            if (arr == null)
                                continue;

                            for (var i = 0; i < arr.Length; i++)
                                row.AddValue(field.Item2.Name + (field.Item2.StartAtZero ? i : i + 1), arr.GetValue(i));

                            continue;
                        }

                        var val = field.Item1.GetValue(elem1.Value.Item1);
                        if (val == null && field.Item1.FieldType == typeof(string))
                            val = string.Empty;

                        row.AddValue(field.Item2.Name, val);
                    }
                    rowsIns.Add(row);
                }
            }

            var result = new QueryBuilder.SQLInsert(tableName, rowsIns, deleteDuplicates: false, primaryKeyNumber: 2).Build() +
                         new QueryBuilder.SQLUpdate(rowsUpd).Build();

            return result;
        }

        /// <summary>
        /// <para>Compare two dictionaries (of the same types) and creates SQL inserts
        ///  or updates accordingly.</para>
        /// <remarks>Second dictionary can be null (only inserts queries will be produced)</remarks>
        /// <remarks>Use DBTableName and DBFieldName attributes to specify table and field names, in TK</remarks>
        /// </summary>
        /// <typeparam name="T">Type of the first primary key</typeparam>
        /// /// <typeparam name="TG">Type of the second primary key</typeparam>
        /// <typeparam name="TK">Type of the WDB struct (field names and types must match DB field name and types)</typeparam>
        /// <typeparam name="TH"></typeparam>
        /// <param name="dict1">Dictionary retrieved from  parser</param>
        /// <param name="dict2">Dictionary retrieved from  DB</param>
        /// <param name="storeType1">(T) Are we dealing with Spells, Quests, Units, ...?</param>
        /// <param name="storeType2">(TG) Are we dealing with Spells, Quests, Units, ...?</param>
        /// <param name="storeType3">(TH) Are we dealing with Spells, Quests, Units, ...?</param>
        /// <param name="primaryKeyName1">The name of the first primary key</param>
        /// <param name="primaryKeyName2">The name of the second primary key</param>
        /// <param name="primaryKeyName3">The name of the third primary key</param>
        /// <returns>A string containing full SQL queries</returns>
        public static string CompareDicts<T, TG, TH, TK>(StoreDictionary<Tuple<T, TG, TH>, TK> dict1, StoreDictionary<Tuple<T, TG, TH>, TK> dict2, StoreNameType storeType1, StoreNameType storeType2, StoreNameType storeType3, string primaryKeyName1, string primaryKeyName2, string primaryKeyName3)
        {
            var tableAttrs = (DBTableNameAttribute[])typeof(TK).GetCustomAttributes(typeof(DBTableNameAttribute), false);
            if (tableAttrs.Length <= 0)
                return string.Empty;
            var tableName = tableAttrs[0].Name;

            var fields = Utilities.GetFieldsAndAttribute<TK, DBFieldNameAttribute>();
            if (fields == null)
                return string.Empty;

            fields.RemoveAll(field => field.Item2.Name == null);

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

            foreach (var elem1 in Settings.SQLOrderByKey ? dict1.OrderBy(blub => blub.Key).ToList() : dict1.ToList())
            {
                if (dict2 != null && dict2.ContainsKey(elem1.Key)) // update
                {
                    var row = new QueryBuilder.SQLUpdateRow();

                    foreach (var field in fields)
                    {
                        var elem2 = dict2[elem1.Key];

                        var val1 = field.Item1.GetValue(elem1.Value.Item1);
                        var val2 = field.Item1.GetValue(elem2.Item1);

                        var arr1 = val1 as Array;
                        if (arr1 != null)
                        {
                            var arr2 = (Array)val2;

                            var isString = arr1.GetType().GetElementType() == typeof(string);

                            for (var i = 0; i < field.Item2.Count; i++)
                            {
                                var value1 = i >= arr1.Length ? (isString ? (object)string.Empty : 0) : arr1.GetValue(i);
                                var value2 = i >= arr2.Length ? (isString ? (object)string.Empty : 0) : arr2.GetValue(i);

                                if (!Utilities.EqualValues(value1, value2))
                                    row.AddValue(field.Item2.Name + (field.Item2.StartAtZero ? i : i + 1), value1);
                            }

                            continue;
                        }

                        if (!Utilities.EqualValues(val1, val2))
                            row.AddValue(field.Item2.Name, val1);
                    }

                    var key1 = Convert.ToUInt32(elem1.Key.Item1);
                    var key2 = Convert.ToUInt32(elem1.Key.Item2);
                    var key3 = Convert.ToUInt32(elem1.Key.Item3);

                    row.AddWhere(primaryKeyName1, key1);
                    row.AddWhere(primaryKeyName2, key2);
                    row.AddWhere(primaryKeyName3, key3);

                    var key1Name = storeType1 != StoreNameType.None ?
                        StoreGetters.GetName(storeType1, (int)key1, false) :
                        elem1.Key.Item1.ToString();
                    var key2Name = storeType2 != StoreNameType.None ?
                        StoreGetters.GetName(storeType2, (int)key2, false) :
                        elem1.Key.Item2.ToString();
                    var key3Name = storeType3 != StoreNameType.None ?
                        StoreGetters.GetName(storeType3, (int)key3, false) :
                        elem1.Key.Item2.ToString();

                    row.Comment = key1Name + " - " + key2Name + " - " + key3Name;
                    row.Table = tableName;

                    if (row.ValueCount == 0)
                        continue;

                    var lastField = fields[fields.Count - 1];
                    if (lastField.Item2.Name == "VerifiedBuild")
                    {
                        var buildvSniff = (int)lastField.Item1.GetValue(elem1.Value.Item1);
                        var buildvDB = (int)lastField.Item1.GetValue(dict2[elem1.Key].Item1);

                        if (buildvDB > buildvSniff) // skip update if DB already has a VerifiedBuild higher than this one
                            continue;
                    }

                    rowsUpd.Add(row);
                }
                else // insert new
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue(primaryKeyName1, elem1.Key.Item1);
                    row.AddValue(primaryKeyName2, elem1.Key.Item2);
                    row.AddValue(primaryKeyName3, elem1.Key.Item3);

                    var key1 = Convert.ToUInt32(elem1.Key.Item1);
                    var key2 = Convert.ToUInt32(elem1.Key.Item2);
                    var key3 = Convert.ToUInt32(elem1.Key.Item3);

                    var key1Name = storeType1 != StoreNameType.None ?
                        StoreGetters.GetName(storeType1, (int)key1, false) :
                        elem1.Key.Item1.ToString();
                    var key2Name = storeType2 != StoreNameType.None ?
                        StoreGetters.GetName(storeType2, (int)key2, false) :
                        elem1.Key.Item2.ToString();
                    var key3Name = storeType3 != StoreNameType.None ?
                        StoreGetters.GetName(storeType3, (int)key3, false) :
                        elem1.Key.Item2.ToString();

                    row.Comment = key1Name + " - " + key2Name + " - " + key3Name;

                    foreach (var field in fields)
                    {
                        if (field.Item1.FieldType.BaseType == typeof(Array))
                        {
                            var arr = (Array)field.Item1.GetValue(elem1.Value.Item1);
                            if (arr == null)
                                continue;

                            for (var i = 0; i < arr.Length; i++)
                                row.AddValue(field.Item2.Name + (field.Item2.StartAtZero ? i : i + 1), arr.GetValue(i));

                            continue;
                        }

                        var val = field.Item1.GetValue(elem1.Value.Item1);
                        if (val == null && field.Item1.FieldType == typeof(string))
                            val = string.Empty;

                        row.AddValue(field.Item2.Name, val);
                    }
                    rowsIns.Add(row);
                }
            }

            var result = new QueryBuilder.SQLInsert(tableName, rowsIns, deleteDuplicates: false, primaryKeyNumber: 3).Build() +
                         new QueryBuilder.SQLUpdate(rowsUpd).Build();

            return result;
        }
    }
}
