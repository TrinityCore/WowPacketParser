using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
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
        /// <param name="tableName">Name of the table</param>
        /// <param name="entries">List of entries to select from DB</param>
        /// <param name="primaryKeyName"> </param>
        /// <returns>Dictionary of structs of type TK</returns>
        public static Dictionary<T, TK> GetDict<T, TK>(string tableName, List<T> entries, string primaryKeyName = "entry")
        {
            var fi = typeof(TK).GetFields(BindingFlags.Public | BindingFlags.Instance);

            var fieldNames = new StringBuilder();
            fieldNames.Append(primaryKeyName + ",");
            for (var i = 0; i < fi.Length; i++)
            {
                fieldNames.Append(fi[i].Name);
                if (i + 1 != fi.Length)
                    fieldNames.Append(",");
            }

            var query = string.Format("SELECT {0} FROM {1}.{2} WHERE {3} IN ({4})",
                fieldNames, Settings.TDBDatabase, tableName, primaryKeyName, String.Join(",", entries));

            var dict = new Dictionary<T, TK>(entries.Count);

            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return null;

                while (reader.Read())
                {
                    var structInstance = (TK)Activator.CreateInstance(typeof(TK));

                    var values = new object[fi.Length+1];
                    var fieldCount = reader.GetValues(values);

                    // first val is the key, so we start at 1
                    for (var i = 1; i < fieldCount; i++)
                    {
                        if (values[i] is DBNull && fi[i-1].FieldType == typeof(string))
                            fi[i-1].SetValueDirect(__makeref(structInstance), string.Empty);
                        else
                            fi[i-1].SetValueDirect(__makeref(structInstance), values[i]);
                    }

                    dict.Add((T)values[0], structInstance);
                }
            }

            return dict;
        }
    }
}
