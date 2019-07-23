using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using MySql.Data.MySqlClient;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.DBC.Structures.BattleForAzeroth;

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

        public static Dictionary<string, List<int>> BroadcastTexts { get; } = new Dictionary<string, List<int>>();
        public static Dictionary<string, List<int>> BroadcastText1s { get; } = new Dictionary<string, List<int>>();
        public static List<POIData> POIs { get; } = new List<POIData>();

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

        public struct POIData
        {
            public uint ID;
            public float PositionX;
            public float PositionY;
            public uint Icon;
            public uint Flags;
            public uint Importance;
            public string Name;
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
            LoadPointsOfinterest();

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
                "SELECT ID, Text, Text1, EmoteID1, EmoteID2, EmoteID3, EmoteDelay1, EmoteDelay2, EmoteDelay3, EmotesID, LanguageID, Flags, ConditionID, SoundEntriesID1, SoundEntriesID2 " +
                $"FROM {Settings.HotfixesDatabase}.broadcast_text;";
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return;

                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader["Id"]);
                    var text = Convert.ToString(reader["Text"]);
                    var text1 = Convert.ToString(reader["Text1"]);

                    if (!BroadcastTexts.ContainsKey(text))
                        BroadcastTexts[text] = new List<int>();
                    BroadcastTexts[text].Add(id);

                    if (!BroadcastText1s.ContainsKey(text1))
                        BroadcastText1s[text1] = new List<int>();
                    BroadcastText1s[text1].Add(id);

                    if (!Settings.UseDBC)
                        continue;

                    var broadcastText = new BroadcastTextEntry()
                    {
                        Text = text,
                        Text1 = text1,

                    };
                    broadcastText.EmoteID = new ushort[3];
                    broadcastText.EmoteID[0] = Convert.ToUInt16(reader["EmoteID1"]);
                    broadcastText.EmoteID[1] = Convert.ToUInt16(reader["EmoteID2"]);
                    broadcastText.EmoteID[2] = Convert.ToUInt16(reader["EmoteID3"]);
                    broadcastText.EmoteDelay = new ushort[3];
                    broadcastText.EmoteDelay[0] = Convert.ToUInt16(reader["EmoteDelay1"]);
                    broadcastText.EmoteDelay[1] = Convert.ToUInt16(reader["EmoteDelay2"]);
                    broadcastText.EmoteDelay[2] = Convert.ToUInt16(reader["EmoteDelay3"]);
                    broadcastText.EmotesID = Convert.ToUInt16(reader["EmotesID"]);
                    broadcastText.LanguageID = Convert.ToByte(reader["LanguageID"]);
                    broadcastText.Flags = Convert.ToByte(reader["Flags"]);
                    broadcastText.ConditionID = Convert.ToUInt32(reader["ConditionID"]);
                    broadcastText.SoundEntriesID = new uint[2];
                    broadcastText.SoundEntriesID[0] = Convert.ToUInt32(reader["SoundEntriesID1"]);
                    broadcastText.SoundEntriesID[1] = Convert.ToUInt32(reader["SoundEntriesID2"]);

                    if (!DBC.DBC.BroadcastText.ContainsKey(id))
                        DBC.DBC.BroadcastText.TryAdd(id, broadcastText);
                    else
                        DBC.DBC.BroadcastText[id] = broadcastText;
                }
            }
        }

        private static void LoadPointsOfinterest()
        {
            string query =
                "SELECT ID, PositionX, PositionY, Icon, Flags, Importance, Name " +
                $"FROM {Settings.TDBDatabase}.points_of_interest ORDER BY ID;";
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return;

                while (reader.Read())
                {
                    var poiData = new POIData()
                    {
                        ID = Convert.ToUInt32(reader["ID"]),
                        PositionX = Convert.ToSingle(reader["PositionX"]),
                        PositionY = Convert.ToSingle(reader["PositionY"]),
                        Icon = Convert.ToUInt32(reader["Icon"]),
                        Flags = Convert.ToUInt32(reader["Flags"]),
                        Importance = Convert.ToUInt32(reader["Importance"]),
                        Name = Convert.ToString(reader["Name"])
                    };

                    POIs.Add(poiData);
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
                        if (values[i] is DBNull)
                        {
                            if (field.Item2.FieldType == typeof(string))
                                field.Item2.SetValue(instance, string.Empty);
                            else if (field.Item3.Any(a => a.Nullable))
                                field.Item2.SetValue(instance, null);
                        }
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
                                    : Convert.ChangeType(values[i], uType));
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
