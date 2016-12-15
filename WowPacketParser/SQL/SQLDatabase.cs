using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using MySql.Data.MySqlClient;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL
{
    public static class SQLDatabase
    {
        /// <summary>
        /// Represents a dictionary of ID-Name dictionaries accessed by <see cref="StoreNameType"/>.
        /// </summary>
        public static readonly Dictionary<StoreNameType, Dictionary<int, string>> NameStores = new Dictionary<StoreNameType, Dictionary<int, string>>();

        /// <summary>
        /// Represents a dictionary of spawn masks accessed by the map id.
        /// </summary>
        public static readonly Dictionary<int, int> MapSpawnMaskStores = new Dictionary<int, int>();

        public static Dictionary<string, List<int>> BroadcastMaleTexts { get; } = new Dictionary<string, List<int>>();
        public static Dictionary<string, List<int>> BroadcastFemaleTexts { get; } = new Dictionary<string, List<int>>();

        private static readonly StoreNameType[] ObjectTypes =
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
            StoreNameType.Player,
            StoreNameType.Achievement
        };

        /// <summary>
        /// Loads names of the objects from the database.
        /// </summary>
        public static void GrabNameData()
        {
            if (!SQLConnector.Connected())
                throw new DataException("Cannot get DB data without an active DB connection.");

            foreach (var objectType in ObjectTypes)
                NameStores.Add(objectType, GetDict<int, string>(
                    $"SELECT `Id`, `Name` FROM `object_names` WHERE `ObjectType`='{objectType}';"));
        }

        /// <summary>
        /// Loads data necessary for the SQL generation from the database.
        /// </summary>
        public static void LoadSQL()
        {
            if (!SQLConnector.Connected())
                throw new DataException("Cannot get DB data without an active DB connection.");

            var startTime = DateTime.Now;

            LoadBroadcastText();

            var endTime = DateTime.Now;
            var span = DateTime.Now.Subtract(startTime);
            Trace.WriteLine($"SQL loaded in {span.ToFormattedString()}.");
        }

        /// <summary>
        /// Loads the broadcast texts form the database.
        /// </summary>
        private static void LoadBroadcastText()
        {
            string query =
                $"SELECT ID, MaleText, FemaleText FROM {Settings.HotfixesDatabase}.broadcast_text;";
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return;

                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader["Id"]);
                    var maleText = Convert.ToString(reader["MaleText"]);
                    var femaleText = Convert.ToString(reader["FemaleText"]);

                    if (!BroadcastMaleTexts.ContainsKey(maleText))
                        BroadcastMaleTexts[maleText] = new List<int>();
                    BroadcastMaleTexts[maleText].Add(id);

                    if (!BroadcastFemaleTexts.ContainsKey(femaleText))
                        BroadcastFemaleTexts[femaleText] = new List<int>();
                    BroadcastFemaleTexts[femaleText].Add(id);
                }
            }
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

        private static object[] GetValues(MySqlDataReader reader, int fieldCount)
        {
            var values = new object[fieldCount];
            var count = reader.GetValues(values);
            if (count != fieldCount)
                throw new InvalidConstraintException(
                    "Number of fields from DB is different of the number of fields with DBFieldName attribute");
            return values;
        }

        public static RowList<T> Get<T>(DataBag<T> conditionList, string database = null)
            where T : IDataModel, new()
        {
            var cond = new RowList<T>();
            cond.AddRange(conditionList.Select(c => c.Item1));
            return Get(cond, database);
        }

        public static RowList<T> Get<T>(RowList<T> rowList = null, string database = null)
            where T : IDataModel, new()
        {
            // TODO: Add new config option "Verify data against DB"
            if (!SQLConnector.Enabled)
                return null;

            var result = new RowList<T>();

            using (var reader = SQLConnector.ExecuteQuery(new SQLSelect<T>(rowList, database).Build()))
            {
                if (reader == null)
                    return null;

                var fields = SQLUtil.GetFields<T>();
                var fieldsCount = fields.Select(f => f.Item3.First().Count).Sum();

                while (reader.Read())
                {
                    var instance = (T)Activator.CreateInstance(typeof(T));
                    var values = GetValues(reader, fieldsCount);

                    var i = 0;
                    foreach (var field in fields)
                    {
                        if (values[i] is DBNull && field.Item2.FieldType == typeof(string))
                            field.Item2.SetValue(instance, string.Empty);
                        else if (field.Item2.FieldType.BaseType == typeof(Enum))
                            field.Item2.SetValue(instance, Enum.Parse(field.Item2.FieldType, values[i].ToString()));
                        else if (field.Item2.FieldType.BaseType == typeof(Array))
                        {
                            var arr = Array.CreateInstance(field.Item2.FieldType.GetElementType(), field.Item3.First().Count);

                            for (var j = 0; j < arr.Length; j++)
                            {
                                var elemType = arr.GetType().GetElementType();

                                if (elemType.IsEnum)
                                    arr.SetValue(Enum.Parse(elemType, values[i + j].ToString()), j);
                                else if (Nullable.GetUnderlyingType(elemType) != null) //is nullable
                                    arr.SetValue(Convert.ChangeType(values[i + j], Nullable.GetUnderlyingType(elemType)), j);
                                else
                                    arr.SetValue(Convert.ChangeType(values[i + j], elemType), j);
                            }
                            field.Item2.SetValue(instance, arr);
                        }
                        else if (field.Item2.FieldType == typeof(bool))
                            field.Item2.SetValue(instance, Convert.ToBoolean(values[i]));
                        else if (Nullable.GetUnderlyingType(field.Item2.FieldType) != null) // is nullable
                        {
                            var uType = Nullable.GetUnderlyingType(field.Item2.FieldType);
                            field.Item2.SetValue(instance,
                                uType.IsEnum
                                    ? Enum.Parse(uType, values[i].ToString())
                                    : Convert.ChangeType(values[i], Nullable.GetUnderlyingType(field.Item2.FieldType)));
                        }
                        else
                            field.Item2.SetValue(instance, values[i]);

                        i += field.Item3.First().Count;
                    }

                    result.Add(instance);
                }
            }

            return result;
        }
    }
}
