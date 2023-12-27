using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using WowPacketParser.DBC.Structures.Dragonflight;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;

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
        public static Dictionary<uint? /*CreatureId*/, List<CreatureEquipment>> CreatureEquipments { get; } = new();
        public static Dictionary<uint /*broadcastText*/, List<uint> /*npc_text ids*/> BroadcastToNPCTexts { get; } = new();
        public static Dictionary<int /*menuID*/, List<uint> /*npc_text ids*/> GossipMenuToNPCTexts { get; } = new();
        public static Dictionary<int /*worldStateID*/, string> WorldStateNames { get; } = new();
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
            StoreNameType.Achievement,
            StoreNameType.Sound
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
            {
                using (var command = SQLConnector.CreateCommand($"SELECT `Id`, `Name` FROM `object_names` WHERE `ObjectType`='{objectType}';"))
                {
                    if (command == null)
                        return;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ID = Convert.ToInt32(reader.GetValue(0));
                            string Name = Convert.ToString(reader.GetValue(1));

                            Dictionary<int, string> names;
                            if (!NameStores.TryGetValue(objectType, out names))
                            {
                                names = new Dictionary<int, string>();
                                NameStores.Add(objectType, names);
                            }

                            if (!names.ContainsKey(ID))
                                names.Add(ID, Name);
                        }
                    }
                }
            }
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
            LoadCreatureEquipment();
            LoadNPCTexts();
            LoadGossipMenuNPCTexts();
            LoadWorldStates();
            LoadNameData();

            var endTime = DateTime.Now;
            var span = DateTime.Now.Subtract(startTime);
            Trace.WriteLine($"SQL loaded in {span.ToFormattedString()}.");
        }

        /// <summary>
        /// Loads the broadcast texts form the database.
        /// </summary>
        private static void LoadBroadcastText()
        {
            var soundFieldName = Settings.TargetedDatabase >= TargetedDatabase.Shadowlands ? "Kit" : "Entries";
            string query =
                $"SELECT ID, Text, Text1, EmoteID1, EmoteID2, EmoteID3, EmoteDelay1, EmoteDelay2, EmoteDelay3, EmotesID, LanguageID, Flags, ConditionID, Sound{soundFieldName}ID1, Sound{soundFieldName}ID2 " +
                $"FROM {Settings.HotfixesDatabase}.broadcast_text;";

            if (Settings.TargetedDatabase == TargetedDatabase.WrathOfTheLichKing || Settings.TargetedDatabase == TargetedDatabase.Cataclysm)
                query = "SELECT ID, LanguageID, Text, Text1, EmoteID1, EmoteID2, EmoteID3, EmoteDelay1, EmoteDelay2, EmoteDelay3, SoundEntriesID, EmotesID, Flags " +
                $"FROM {Settings.TDBDatabase}.broadcast_text;";

            if (Settings.TargetedDatabase == TargetedDatabase.TheBurningCrusade)
                return;

            using (var command = SQLConnector.CreateCommand(query))
            {
                if (command == null)
                    return;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
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
                        broadcastText.LanguageID = Convert.ToInt32(reader["LanguageID"]);
                        broadcastText.Flags = Convert.ToByte(reader["Flags"]);
                        if (Settings.TargetedDatabase == TargetedDatabase.WrathOfTheLichKing || Settings.TargetedDatabase == TargetedDatabase.Cataclysm)
                        {
                            broadcastText.ConditionID = 0;
                            broadcastText.SoundEntriesID = new uint[2];
                            broadcastText.SoundEntriesID[0] = Convert.ToUInt32(reader["SoundEntriesID"]);
                            broadcastText.SoundEntriesID[1] = 0;
                        }
                        else
                        {
                            broadcastText.ConditionID = Convert.ToInt32(reader["ConditionID"]);
                            broadcastText.SoundEntriesID = new uint[2];
                            broadcastText.SoundEntriesID[0] = Convert.ToUInt32(reader[$"Sound{soundFieldName}ID1"]);
                            broadcastText.SoundEntriesID[1] = Convert.ToUInt32(reader[$"Sound{soundFieldName}ID2"]);
                        }

                        if (!DBC.DBC.BroadcastText.ContainsKey(id))
                            DBC.DBC.BroadcastText.Add(id, broadcastText);
                        else
                            DBC.DBC.BroadcastText[id] = broadcastText;
                    }
                }
            }
        }

        private static void LoadPointsOfinterest()
        {
            string query =
                "SELECT ID, PositionX, PositionY, Icon, Flags, Importance, Name " +
                $"FROM {Settings.TDBDatabase}.points_of_interest ORDER BY ID;";

            if (Settings.TargetedDatabase == TargetedDatabase.TheBurningCrusade)
                query = $"SELECT entry AS ID, x AS PositionX, y AS PositionY, icon AS Icon, flags AS Flags, data AS Importance, icon_name AS Name FROM {Settings.TDBDatabase}.points_of_interest ORDER BY entry;";

            using (var command = SQLConnector.CreateCommand(query))
            {
                if (command == null)
                    return;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
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
        }

        private static void LoadCreatureEquipment()
        {
            if (Settings.TargetedDatabase == TargetedDatabase.TheBurningCrusade)
                return;

            string columns = "CreatureID, ID, ItemID1, ItemID2, ItemID3, VerifiedBuild";
            if (Settings.TargetedDatabase >= TargetedDatabase.Legion)
                columns += ", AppearanceModID1, ItemVisual1, AppearanceModID2, ItemVisual2, AppearanceModID3, ItemVisual3";
            string query = $"SELECT {columns} FROM {Settings.TDBDatabase}.creature_equip_template";

            using (var command = SQLConnector.CreateCommand(query))
            {
                if (command == null)
                    return;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var equip = new CreatureEquipment
                        {
                            CreatureID = reader.GetUInt32("CreatureID"),
                            ID = reader.GetUInt32("ID"),
                            ItemID1 = reader.GetUInt32("ItemID1"),
                            ItemID2 = reader.GetUInt32("ItemID2"),
                            ItemID3 = reader.GetUInt32("ItemID3"),
                            VerifiedBuild = reader.GetInt32("VerifiedBuild")
                        };

                        if (Settings.TargetedDatabase >= TargetedDatabase.Legion)
                        {
                            equip.AppearanceModID1 = reader.GetUInt16("AppearanceModID1");
                            equip.ItemVisual1 = reader.GetUInt16("ItemVisual1");
                            equip.AppearanceModID2 = reader.GetUInt16("AppearanceModID2");
                            equip.ItemVisual2 = reader.GetUInt16("ItemVisual2");
                            equip.AppearanceModID3 = reader.GetUInt16("AppearanceModID3");
                            equip.ItemVisual3 = reader.GetUInt16("ItemVisual3");
                        }

                        // CreatureID never null
                        if (CreatureEquipments.TryGetValue(equip.CreatureID, out var equipList))
                        {
                            equipList.Add(equip);
                            continue;
                        }
                        CreatureEquipments.Add(equip.CreatureID, new List<CreatureEquipment>() { equip });
                    }
                }
            }
        }

        private static void LoadNPCTexts()
        {
            if (Settings.TargetedDatabase == TargetedDatabase.TheBurningCrusade)
                return;

            string columns = "ID, BroadcastTextID0, BroadcastTextID1, BroadcastTextID2, BroadcastTextID3, BroadcastTextID4, BroadcastTextID5, BroadcastTextID6, BroadcastTextID7";
            string query = $"SELECT {columns} FROM {Settings.TDBDatabase}.npc_text";

            using (var command = SQLConnector.CreateCommand(query))
            {
                if (command == null)
                    return;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var npcTextId = reader.GetUInt32(0);
                        for (int i = 1; i < 9; i++)
                        {
                            var broadcastTextId = reader.GetUInt32(i);
                            if (BroadcastToNPCTexts.TryGetValue(broadcastTextId, out var npcTextList))
                                npcTextList.Add(npcTextId);
                            else
                                BroadcastToNPCTexts.Add(broadcastTextId, new List<uint> { npcTextId });
                        }
                    }
                }
            }
        }

        private static void LoadGossipMenuNPCTexts()
        {
            if (Settings.TargetedDatabase == TargetedDatabase.TheBurningCrusade)
                return;

            string columns = "MenuID, TextID";
            string query = $"SELECT {columns} FROM {Settings.TDBDatabase}.gossip_menu";

            using (var command = SQLConnector.CreateCommand(query))
            {
                if (command == null)
                    return;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var menuId = reader.GetInt32(0);
                        var npcTextId = reader.GetUInt32(1);
                        if (GossipMenuToNPCTexts.TryGetValue(menuId, out var list))
                            list.Add(npcTextId);
                        else
                            GossipMenuToNPCTexts.Add(menuId, new List<uint> { npcTextId });
                    }
                }
            }
        }

        private static void LoadWorldStates()
        {
            if (Settings.TargetedDatabase != TargetedDatabase.Cataclysm && Settings.TargetedDatabase != TargetedDatabase.TheBurningCrusade && (Settings.TargetedDatabase < TargetedDatabase.Shadowlands || Settings.TargetedDatabase >= TargetedDatabase.Classic))
                return;

            string columns = "`ID`, `Comment`";
            string query = $"SELECT {columns} FROM {Settings.TDBDatabase}.world_state";

            if (Settings.TargetedDatabase == TargetedDatabase.TheBurningCrusade)
            {
                columns = "`Id`, `Name`";
                query = $"SELECT {columns} FROM {Settings.TDBDatabase}.worldstate_name";
            }

            using (var command = SQLConnector.CreateCommand(query))
            {
                if (command == null)
                    return;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var worldStateID = reader.GetInt32(0);
                        var comment = reader.GetString(1);
                        WorldStateNames.Add(worldStateID, comment);
                    }
                }
            }
        }

        private static void LoadNameData()
        {
            string questQuery = $"SELECT `ID`, `LogTitle` FROM {Settings.TDBDatabase}.quest_template;";
            if (Settings.TargetedDatabase == TargetedDatabase.TheBurningCrusade)
            {
                questQuery = $"SELECT `entry`, `Title` FROM {Settings.TDBDatabase}.quest_template;";
            }

            // Unit
            NameStores.Add(StoreNameType.Unit, GetDict<int, string>(
                    $"SELECT `entry`, `name` FROM {Settings.TDBDatabase}.creature_template;"));

            // GameObject
            NameStores.Add(StoreNameType.GameObject, GetDict<int, string>(
                    $"SELECT `entry`, `name` FROM {Settings.TDBDatabase}.gameobject_template;"));

            // Quest
            NameStores.Add(StoreNameType.Quest, GetDict<int, string>(questQuery));

            // Item - Cataclysm and above have ItemSparse.db2
            if (Settings.TargetedDatabase <= TargetedDatabase.WrathOfTheLichKing)
            {
                NameStores.Add(StoreNameType.Item, GetDict<int, string>(
                    $"SELECT `entry`, `name` FROM {Settings.TDBDatabase}.item_template;"));
            }

            // Phase - Before Cataclysm there was phasemask system
            if (Settings.TargetedDatabase > TargetedDatabase.Cataclysm)
            {
                NameStores.Add(StoreNameType.PhaseId, GetDict<int, string>(
                    $"SELECT `ID`, `Name` FROM {Settings.TDBDatabase}.phase_name;"));
            }
        }

        // Returns a dictionary from a DB query with two parameters (e.g <creature_entry, creature_name>)
        // TODO: Drop this and use the GetDict<T, TK> method below
        public static Dictionary<T, TK> GetDict<T, TK>(string query)
        {
            using (var command = SQLConnector.CreateCommand(query))
            {
                if (command == null)
                    return null;

                var dict = new Dictionary<T, TK>();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        dict.Add((T)Convert.ChangeType(reader.GetValue(0), typeof(T)), (TK)Convert.ChangeType(reader.GetValue(1), typeof(TK)));
                }

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

        public static RowList<T> Get<T>(IEnumerable<Tuple<T, TimeSpan?>> conditionList, string database = null)
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

            if (!SQLUtil.IsTableVisible<T>())
                return null;

            var result = new RowList<T>();

            using (var command = SQLConnector.CreateCommand(new SQLSelect<T>(rowList, database).Build()))
            {
                if (command == null)
                    return null;

                var fields = SQLUtil.GetFields<T>();
                var fieldsCount = fields.Select(f => f.Item3.First().Count).Sum();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var instance = (T)Activator.CreateInstance(typeof(T));
                        var values = GetValues(reader, fieldsCount);

                        var i = 0;
                        foreach (var field in fields)
                        {
                            SQLUtil.SetFieldValueByDB(instance, field, values, i);
                            i += field.Item3.First().Count;
                        }

                        result.Add(instance);
                    }
                }
            }

            return result;
        }

        public static RowList<CreatureDB> GetCreatures(RowList<CreatureDB> rowList = null, string database = null)
        {
            if (!SQLConnector.Enabled)
                return null;

            if (!SQLUtil.IsTableVisible<CreatureDB>())
                return null;

            var result = new RowList<CreatureDB>();

            using (var command = SQLConnector.CreateCommand(new SQLSelect<CreatureDB>(rowList, database).Build()))
            {
                if (command == null)
                    return null;

                var fields = SQLUtil.GetFields<CreatureDB>();
                var fieldsCount = fields.Select(f => f.Item3.First().Count).Sum();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var creature = new CreatureDB();

                        creature.DbGuid = reader.GetUInt32(0);
                        creature.ID = reader.GetUInt32(1);
                        creature.Map = reader.GetUInt32(2);
                        creature.PosX = reader.GetDecimal(3);
                        creature.PosY = reader.GetDecimal(4);
                        creature.PosZ = reader.GetDecimal(5);
                        creature.Orientation = reader.GetDecimal(6);

                        result.Add(creature);
                    }
                }
            }

            return result;
        }

        public static RowList<GameObjectDB> GetGameObjects(RowList<GameObjectDB> rowList = null, string database = null)
        {
            if (!SQLConnector.Enabled)
                return null;

            if (!SQLUtil.IsTableVisible<GameObjectDB>())
                return null;

            var result = new RowList<GameObjectDB>();

            using (var command = SQLConnector.CreateCommand(new SQLSelect<GameObjectDB>(rowList, database).Build()))
            {
                if (command == null)
                    return null;

                var fields = SQLUtil.GetFields<GameObjectDB>();
                var fieldsCount = fields.Select(f => f.Item3.First().Count).Sum();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var go = new GameObjectDB();

                        go.DbGuid = reader.GetUInt32(0);
                        go.ID = reader.GetUInt32(1);
                        go.Map = reader.GetUInt32(2);
                        go.PosX = reader.GetDecimal(3);
                        go.PosY = reader.GetDecimal(4);
                        go.PosZ = reader.GetDecimal(5);
                        go.Orientation = reader.GetDecimal(6);
                        go.Rot0 = reader.GetDecimal(7);
                        go.Rot1 = reader.GetDecimal(8);
                        go.Rot2 = reader.GetDecimal(9);
                        go.Rot3 = reader.GetDecimal(10);

                        result.Add(go);
                    }
                }
            }

            return result;
        }

        public static uint GetNPCTextIDByMenuIDAndBroadcastText(int menuId, uint broadcastTextID)
        {
            if (!BroadcastToNPCTexts.TryGetValue(broadcastTextID, out var npcTextsByBroadcast))
                return 0;
            if (!GossipMenuToNPCTexts.TryGetValue(menuId, out var npcTextsByMenuId))
                return npcTextsByBroadcast[0];

            return npcTextsByBroadcast.FirstOrDefault(n => npcTextsByMenuId.Contains(n));
        }
    }
}
