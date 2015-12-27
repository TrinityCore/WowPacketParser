using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using DBFilesClient.NET;
using WowPacketParser.DBC.Structures;
using WowPacketParser.Misc;

namespace WowPacketParser.DBC
{
    public static class DBC
    {
        public static DBCStorage<AreaTableEntry> AreaTable = new DBCStorage<AreaTableEntry>();
        public static DB2Storage<AchievementEntry> Achievement = new DB2Storage<AchievementEntry>();
        public static DBCStorage<CreatureFamilyEntry> CreatureFamily = new DBCStorage<CreatureFamilyEntry>();
        public static DB2Storage<CriteriaTreeEntry> CriteriaTree = new DB2Storage<CriteriaTreeEntry>();
        public static DBCStorage<DifficultyEntry> Difficulty = new DBCStorage<DifficultyEntry>();
        public static DB2Storage<ItemEntry> Item = new DB2Storage<ItemEntry>();
        [DataStoreFileName("Item-sparse")]
        public static DB2Storage<ItemSparseEntry> ItemSparse = new DB2Storage<ItemSparseEntry>();
        public static DBCStorage<MapEntry> Map = new DBCStorage<MapEntry>();
        public static DBCStorage<MapDifficultyEntry> MapDifficulty = new DBCStorage<MapDifficultyEntry>();
        public static DBCStorage<SpellEntry> Spell = new DBCStorage<SpellEntry>();

        /*
        private static string GetExpansionPath(ClientVersionBuild build)
        {
            switch (ClientVersion.GetExpansion(build))
            {
                case ClientType.WarlordsOfDraenor:
                   return "\\Warlords of Draenor";
                case ClientType.Cataclysm:
                    return "\\Cataclysm";
                case ClientType.WrathOfTheLichKing:
                    return "\\Wrath Of The Lich King";
            }

            return "\\";
        }
        */

        private static string GetLocationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static void Load()
        {
            string path = GetLocationPath() + Settings.DBCPath;

            if (!Directory.Exists($"{path}"))
            {
                Trace.WriteLine($"DBC folder \"{path}\" not found");
                return;
            }

            Trace.WriteLine($"DBC folder \"{path}\" found");

            foreach (var dbc in typeof(DBC).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var startTime = DateTime.Now;
                if (!dbc.FieldType.IsGenericType)
                    continue;

                string extension;
                if (dbc.FieldType.GetGenericTypeDefinition() == typeof(DBCStorage<>))
                    extension = "dbc";
                else if (dbc.FieldType.GetGenericTypeDefinition() == typeof(DB2Storage<>))
                    extension = "db2";
                else
                    continue;

                string name = dbc.Name;

                DataStoreFileNameAttribute[] attributes = dbc.GetCustomAttributes(typeof(DataStoreFileNameAttribute), false) as DataStoreFileNameAttribute[];
                if (attributes.Length == 1)
                    name = attributes[0].FileName;

                try
                {
                    using (var strm = new FileStream($"{path}\\{name}.{extension}", FileMode.Open))
                        dbc.FieldType.GetMethod("Load", new Type[] { typeof(FileStream) }).Invoke(dbc.GetValue(null), new object[]{ strm });
                }
                catch (DirectoryNotFoundException)
                {
                    throw new DirectoryNotFoundException($"Could not open {dbc.Name}.{extension}!");
                }
                catch (TargetInvocationException tie)
                {
                    if (tie.InnerException is ArgumentException)
                        throw new ArgumentException($"Failed to load {dbc.Name}.{extension}: {tie.InnerException.Message}");
                    throw;
                }

                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);

                Trace.WriteLine($"Initialized {dbc.Name}.{extension} data stores in {span.ToFormattedString()}");
            }

            if (AreaTable != null)
                foreach (var dbcInfo in AreaTable)
                {
                    if (dbcInfo.ParentAreaID != 0 && !Zones.ContainsKey(dbcInfo.ParentAreaID))
                        Zones.Add(dbcInfo.ParentAreaID, dbcInfo.ZoneName);
                }

            if (MapDifficulty != null)
            {
                foreach (var mapDifficulty in MapDifficulty)
                {
                    int difficultyID = 1 << mapDifficulty.DifficultyID;

                    if (MapSpawnMaskStores.ContainsKey(mapDifficulty.MapID))
                        MapSpawnMaskStores[mapDifficulty.MapID] |= difficultyID;
                    else
                        MapSpawnMaskStores.Add(mapDifficulty.MapID, difficultyID);
                }
            }

            if (CriteriaTree != null && Achievement != null)
            {
                ICollection<AchievementEntry> achievementLists = Achievement;
                var achievements = achievementLists.GroupBy(achievement => achievement.CriteriaTree)
                    .ToDictionary(group => group.Key, group => group.ToList());

                foreach (var criteriaTree in CriteriaTree)
                {
                    string result = "";
                    List<AchievementEntry> achievementList;
                    uint criteriaTreeID = criteriaTree.Parent > 0 ? criteriaTree.Parent : criteriaTree.ID;
                    if (achievements.TryGetValue(criteriaTreeID, out achievementList))
                        foreach (var achievement in achievementList)
                            result = $"Achievement: ID: {achievement.ID} \"{achievement.Description}\" - ";

                    result += $"Criteria: \"{criteriaTree.Description}\"";

                    if (!CriteriaStores.ContainsKey(criteriaTree.CriteriaID))
                        CriteriaStores.Add(criteriaTree.CriteriaID, result);
                    else
                        CriteriaStores[criteriaTree.CriteriaID] += $" / Criteria: \"{criteriaTree.Description}\"";
                }
            }
        }

        public static readonly Dictionary<uint, string> Zones = new Dictionary<uint, string>();
        public static readonly Dictionary<int, int> MapSpawnMaskStores = new Dictionary<int, int>();
        public static Dictionary<uint, string> CriteriaStores = new Dictionary<uint, string>();
    }
}
