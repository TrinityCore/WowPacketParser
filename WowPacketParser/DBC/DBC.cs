using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Reflection;
using DBFilesClient.NET;
using WowPacketParser.DBC.Structures;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.DBC
{
    public static class DBC
    {
        public static Storage<AreaTableEntry> AreaTable = new Storage<AreaTableEntry>(GetPath(), "AreaTable.db2");
        public static Storage<AchievementEntry> Achievement = new Storage<AchievementEntry>(GetPath(), "Achievement.db2");
        public static Storage<CreatureFamilyEntry> CreatureFamily = new Storage<CreatureFamilyEntry>(GetPath(), "CreatureFamily.db2");
        public static Storage<CreatureDisplayInfoEntry> CreatureDisplayInfo = new Storage<CreatureDisplayInfoEntry>(GetPath(), "CreatureDisplayInfo.db2");
        public static Storage<CriteriaTreeEntry> CriteriaTree = new Storage<CriteriaTreeEntry>(GetPath(), "CriteriaTree.db2");
        public static Storage<DifficultyEntry> Difficulty = new Storage<DifficultyEntry>(GetPath(), "Difficulty.db2");
        public static Storage<FactionEntry> Faction = new Storage<FactionEntry>(GetPath(), "Faction.db2");
        public static Storage<FactionTemplateEntry> FactionTemplate = new Storage<FactionTemplateEntry>(GetPath(), "FactionTemplate.db2");
        public static Storage<ItemEntry> Item = new Storage<ItemEntry>(GetPath(), "Item.db2");
        public static Storage<ItemSparseEntry> ItemSparse = new Storage<ItemSparseEntry>(GetPath(), "Item-sparse.db2");
        public static Storage<MapEntry> Map = new Storage<MapEntry>(GetPath(), "Map.db2");
        public static Storage<MapDifficultyEntry> MapDifficulty = new Storage<MapDifficultyEntry>(GetPath(), "MapDifficulty.db2");
        public static Storage<SoundKitEntry> SoundKit = new Storage<SoundKitEntry>(GetPath(), "SoundKit.db2");
        public static Storage<SpellEntry> Spell = new Storage<SpellEntry>(GetPath(), "Spell.db2");

        private static string GetPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Settings.DBCPath;
        }

        public static  void Load()
        {
            if (!Directory.Exists(GetPath()))
            {
                Trace.WriteLine($"DBC folder \"{ GetPath() }\" not found");
                return;
            }
            else
                Trace.WriteLine($"DBC folder \"{ GetPath() }\" found");

            Console.WriteLine("File name                        Average time to load     Minimum time       Maximum time       Record count");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");

            foreach (var type in Assembly.GetAssembly(typeof(DBC)).GetTypes())
            {
                if (!type.IsClass)
                    continue;

                var attr = type.GetCustomAttribute<DBFileNameAttribute>();
                if (attr == null)
                    continue;

                var times = new List<long>();
                var recordCount = 0;

                for (var i = 1; i <= 10; ++i)
                {
                    var instanceType = typeof(Storage<>).MakeGenericType(type);

                    var countGetter = instanceType.GetProperty("Count").GetGetMethod();
                    var stopwatch = Stopwatch.StartNew();
                    var instance = Activator.CreateInstance(instanceType, GetPath(), $"{ attr.FileName }.db2");
                    stopwatch.Stop();

                    times.Add(stopwatch.ElapsedTicks);

                    if (recordCount == 0)
                        recordCount = (int)countGetter.Invoke(instance, new object[] { });
                }

                Console.WriteLine($"{ attr.FileName.PadRight(33) }{ TimeSpan.FromTicks((long)times.Average()).ToString().PadRight(25) }{ TimeSpan.FromTicks(times.Min()).ToString().PadRight(19) }{ TimeSpan.FromTicks(times.Max()).ToString().PadRight(19) }{ recordCount }");
            }

            if (AreaTable != null)
                foreach (var db2Info in AreaTable)
                {
                    if (db2Info.Value.ParentAreaID != 0 && !Zones.ContainsKey(db2Info.Value.ParentAreaID))
                        Zones.Add(db2Info.Value.ParentAreaID, db2Info.Value.ZoneName);
                }

            if (MapDifficulty != null)
            {
                foreach (var mapDifficulty in MapDifficulty)
                {
                    int difficultyID = 1 << mapDifficulty.Value.DifficultyID;

                    if (MapSpawnMaskStores.ContainsKey(mapDifficulty.Value.MapID))
                        MapSpawnMaskStores[mapDifficulty.Value.MapID] |= difficultyID;
                    else
                        MapSpawnMaskStores.Add(mapDifficulty.Value.MapID, difficultyID);
                }
            }

            if (CriteriaTree != null && Achievement != null)
            {
                ICollection<AchievementEntry> achievementLists = Achievement.Values;
                var achievements = achievementLists.GroupBy(achievement => achievement.CriteriaTree)
                    .ToDictionary(group => group.Key, group => group.ToList());

                foreach (var criteriaTree in CriteriaTree)
                {
                    string result = "";
                    List<AchievementEntry> achievementList;
                    ushort criteriaTreeID = criteriaTree.Value.Parent > 0 ? criteriaTree.Value.Parent : (ushort)criteriaTree.Key;

                    if (achievements.TryGetValue(criteriaTreeID, out achievementList))
                        foreach (var achievement in achievementList)
                            result = $"AchievementID: {achievement.ID} Description: \"{ achievement.Description }\"";

                    if (!CriteriaStores.ContainsKey((ushort)criteriaTree.Value.CriteriaID))
                    {
                        if (criteriaTree.Value.Description != string.Empty)
                            result += $" - CriteriaDescription: \"{criteriaTree.Value.Description }\"";

                        CriteriaStores.Add((ushort)criteriaTree.Value.CriteriaID, result);
                    }
                    else
                        CriteriaStores[(ushort)criteriaTree.Value.CriteriaID] += $" / CriteriaDescription: \"{ criteriaTree.Value.Description }\"";
                }
            }

            if (Faction != null && FactionTemplate != null)
            {
                foreach (var factionTemplate in FactionTemplate)
                {
                    if (Faction.ContainsKey(factionTemplate.Value.Faction))
                        FactionStores.Add((uint)factionTemplate.Key, Faction[factionTemplate.Value.Faction].Name);
                }
            }
        }

        public static readonly Dictionary<uint, string> Zones = new Dictionary<uint, string>();
        public static readonly Dictionary<int, int> MapSpawnMaskStores = new Dictionary<int, int>();
        public static Dictionary<ushort, string> CriteriaStores = new Dictionary<ushort, string>();
        public static Dictionary<uint, string> FactionStores = new Dictionary<uint, string>();
    }
}
