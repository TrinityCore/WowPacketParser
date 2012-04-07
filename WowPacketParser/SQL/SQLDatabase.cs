using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public static class SQLDatabase
    {
        public static readonly Dictionary<StoreNameType, Dictionary<int, string>> NameStores =
            new Dictionary<StoreNameType, Dictionary<int, string>>();

        private static readonly StoreNameType[] ObjectTypes = new[]
        {
            StoreNameType.Spell,
            StoreNameType.Map,
            StoreNameType.LFGDungeon,
            StoreNameType.Battleground,
            StoreNameType.Unit,
            StoreNameType.GameObject,
            StoreNameType.Item,
            StoreNameType.Quest,
            StoreNameType.Zone,
            StoreNameType.Area,
            StoreNameType.Player
        };

        public static void GrabNameData()
        {
            if (!SQLConnector.Connected())
                throw new DataException("Cannot get DB data without an active DB connection.");

            foreach (var objectType in ObjectTypes)
                NameStores.Add(objectType, GetDict<int, string>(string.Format("SELECT `Id`, `Name` FROM `ObjectNames` WHERE `ObjectType`='{0}';", objectType)));
        }

        // Returns a dictionary from a DB query with two parameters (e.g <creature_entry, creature_name>)
        // TODO: Drop this and use the GetDict<T, TK> method below
        private static Dictionary<T, TK> GetDict<T, TK>(string query)
        {
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return null;

                var dict = new Dictionary<T, TK>();

                while (reader.Read())
                    dict.Add((T)reader.GetValue(0), (TK)reader.GetValue(1));

                return dict;
            }
        }

        // Returns a dictionary from a DB query with any number of parameters
        // TODO: Drop this and use the GetDict<T, TK> method below
        public static Dictionary<T, dynamic> GetDict<T>(string query)
        {
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return null;

                var dict = new Dictionary<T, dynamic>();

                while (reader.Read())
                {
                    var pk = (T)reader.GetValue(0);

                    var objs = new object[30];
                    var fieldCount = reader.GetValues(objs);
                    var obj = objs.ToTuple(fieldCount);

                    dict.Add(pk, obj);
                }

                return dict;
            }
        }

        /// <summary>
        /// Gets from `world` database a dictionary of the given struct. 
        /// Structs fields name and type must match the name and type of the DB columns
        /// </summary>
        /// <typeparam name="T">Type of the elements of the list of entries (usually uint)</typeparam>
        /// <typeparam name="TK">Type of the struct</typeparam>
        /// <param name="entries">List of entries to select from DB</param>
        /// <param name="primaryKeyName"> </param>
        /// <returns>Dictionary of structs of type TK</returns>
        public static Dictionary<T, TK> GetDict<T, TK>(List<T> entries, string primaryKeyName = "entry")
        {
            if (entries.Count == 0)
                return null;

            // TODO: Add new config option "Verify data against DB"
            if (!SQLConnector.Enabled)
                return null;

            var tableAttrs = (DBTableNameAttribute[])typeof(TK).GetCustomAttributes(typeof(DBTableNameAttribute), false);
            if (tableAttrs.Length <= 0)
                return null;
            var tableName = tableAttrs[0].Name;

            var fields = Utilities.GetFieldsAndAttribute<TK, DBFieldNameAttribute>();

            var fieldCount = 1;
            var fieldNames = new StringBuilder();
            fieldNames.Append(primaryKeyName + ",");
            foreach (var field in fields)
            {
                fieldNames.Append(field.Item2.ToString());
                fieldNames.Append(",");
                fieldCount += field.Item2.Count;
            }

            var query = string.Format("SELECT {0} FROM {1}.{2} WHERE {3} IN ({4})",
                fieldNames.ToString().TrimEnd(','), Settings.TDBDatabase, tableName, primaryKeyName, String.Join(",", entries));

            var dict = new Dictionary<T, TK>(entries.Count);

            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return null;

                while (reader.Read())
                {
                    var instance = (TK)Activator.CreateInstance(typeof(TK));

                    var values = new object[fieldCount];
                    var count = reader.GetValues(values);
                    if (count != fieldCount)
                        throw new InvalidConstraintException(
                            "Number of fields from DB is different of the number of fields with DBFieldName attribute");

                    var i = 1;
                    foreach (var field in fields)
                    {
                        if (values[i] is DBNull && field.Item1.FieldType == typeof(string))
                        {
                            field.Item1.SetValueDirect(__makeref(instance), string.Empty);
                        }
                        else if (field.Item1.FieldType.BaseType == typeof(Enum))
                        {
                            field.Item1.SetValueDirect(__makeref(instance), Enum.Parse(field.Item1.FieldType, values[i].ToString()));
                        }
                        else if (field.Item1.FieldType.BaseType == typeof(Array))
                        {
                            var arr = Array.CreateInstance(field.Item1.FieldType.GetElementType(), field.Item2.Count);

                            for (var j = 0; j < arr.Length; j++)
                            {
                                var elemType = arr.GetType().GetElementType();
                                var val = Convert.ChangeType(values[i + j], elemType);

                                arr.SetValue(val, j);
                            }

                            field.Item1.SetValueDirect(__makeref(instance), arr);
                        }
                        else if (field.Item1.FieldType == typeof(bool))
                        {
                            field.Item1.SetValueDirect(__makeref(instance), Convert.ToBoolean(values[i]));
                        }
                        else
                        {
                            field.Item1.SetValueDirect(__makeref(instance), values[i]);
                        }

                        i += field.Item2.Count;
                    }

                    dict.Add((T)values[0], instance);
                }
            }

            return dict;
        }
    }
}
