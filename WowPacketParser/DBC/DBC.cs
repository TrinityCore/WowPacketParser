using DBFileReaderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WowPacketParser.DBC.Structures.TheWarWithin;
using WowPacketParser.Misc;

namespace WowPacketParser.DBC
{
    public static class DBC
    {
        public static Storage<AreaTableEntry> AreaTable { get; set; }
        public static Storage<AchievementEntry> Achievement { get; set; }
        public static Storage<AnimationDataEntry> AnimationData { get; set; }
        public static Storage<BroadcastTextEntry> BroadcastText { get; set; }
        public static Storage<CreatureEntry> Creature { get; set; }
        public static Storage<CreatureDifficultyEntry> CreatureDifficulty { get; set; }
        public static Storage<CreatureFamilyEntry> CreatureFamily { get; set; }
        public static Storage<CreatureDisplayInfoEntry> CreatureDisplayInfo { get; set; }
        public static Storage<CriteriaTreeEntry> CriteriaTree { get; set; }
        public static Storage<CriteriaEntry> Criteria { get; set; }
        public static Storage<DifficultyEntry> Difficulty { get; set; }
        public static Storage<FactionEntry> Faction { get; set; }
        public static Storage<FactionTemplateEntry> FactionTemplate { get; set; }
        public static Storage<ItemEntry> Item { get; set; }
        public static Storage<ItemSparseEntry> ItemSparse { get; set; }
        public static Storage<MapEntry> Map { get; set; }
        public static Storage<MapDifficultyEntry> MapDifficulty { get; set; }
        public static Storage<QuestLineXQuestEntry> QuestLineXQuest { get; set; }
        public static Storage<PhaseEntry> Phase { get; set; }
        public static Storage<PhaseXPhaseGroupEntry> PhaseXPhaseGroup { get; set; }
        public static Storage<SpellEffectEntry> SpellEffect { get; set; }
        public static Storage<SpellNameEntry> SpellName { get; set; }

        private static string GetDBCPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Settings.DBCPath, Settings.DBCLocale);
        }

        private static string GetDBCPath(string fileName)
        {
            return Path.Combine(GetDBCPath(), fileName);
        }

        private static string GetHotfixCachePath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Settings.HotfixCachePath);
        }

        public static async void Load()
        {
            if (!Directory.Exists(GetDBCPath()))
            {
                Trace.WriteLine($"DBC folder \"{ GetDBCPath() }\" not found");
                return;
            }
            else
                Trace.WriteLine($"DBC folder \"{ GetDBCPath() }\" found");

            HotfixReader hotfixReader = null;
            try
            {
                hotfixReader = new HotfixReader(GetHotfixCachePath());
                Trace.WriteLine($"Hotfix cache {GetHotfixCachePath()} found!");
            }
            catch (Exception)
            {
                Trace.WriteLine($"Hotfix cache {GetHotfixCachePath()} cannot be loaded, ignoring!");
            }

            Trace.WriteLine("File name                           LoadTime             Record count");
            Trace.WriteLine("---------------------------------------------------------------------");

            Parallel.ForEach(typeof(DBC).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), dbc =>
            {
                Type type = dbc.PropertyType.GetGenericArguments()[0];

                if (!type.IsClass)
                    return;

                var startTime = DateTime.Now;
                var attr = type.GetCustomAttribute<DBFileAttribute>();
                if (attr == null)
                    return;

                var times = new List<long>();
                var instanceType = typeof(Storage<>).MakeGenericType(type);
                var countGetter = instanceType.GetProperty("Count").GetGetMethod();
                dynamic instance = Activator.CreateInstance(instanceType, $"{ GetDBCPath(attr.FileName) }.db2");
                var recordCount = (int)countGetter.Invoke(instance, new object[] { });

                try
                {
                    var db2Reader = new DBReader($"{ GetDBCPath(attr.FileName) }.db2");

                    if (hotfixReader != null)
                        hotfixReader.ApplyHotfixes(instance, db2Reader);

                    dbc.SetValue(dbc.GetValue(null), instance);
                }
                catch (TargetInvocationException tie)
                {
                    if (tie.InnerException is ArgumentException)
                        throw new ArgumentException($"Failed to load {attr.FileName}.db2: {tie.InnerException.Message}");
                    throw;
                }

                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);

                Trace.WriteLine($"{ attr.FileName.PadRight(33) } { TimeSpan.FromTicks(span.Ticks).ToString().PadRight(28) } { recordCount.ToString().PadRight(19) }");
            });

            await Task.WhenAll(Task.Run(() =>
            {
                if (AreaTable != null)
                    foreach (var db2Info in AreaTable)
                    {
                        if (db2Info.Value.ParentAreaID != 0 && !Zones.ContainsKey(db2Info.Value.ParentAreaID))
                            Zones.Add(db2Info.Value.ParentAreaID, db2Info.Value.ZoneName);
                    }
            }), Task.Run(() =>
            {
                if (MapDifficulty != null)
                {
                    foreach (var mapDifficulty in MapDifficulty)
                    {
                        int difficultyID = 1 << mapDifficulty.Value.DifficultyID;

                        if (MapSpawnMaskStores.ContainsKey(mapDifficulty.Value.MapID))
                            MapSpawnMaskStores[mapDifficulty.Value.MapID] |= difficultyID;
                        else
                            MapSpawnMaskStores.Add(mapDifficulty.Value.MapID, difficultyID);

                        if (!MapDifficultyStores.ContainsKey(mapDifficulty.Value.MapID))
                            MapDifficultyStores.Add(mapDifficulty.Value.MapID, new List<int>() { mapDifficulty.Value.DifficultyID });
                        else
                            MapDifficultyStores[mapDifficulty.Value.MapID].Add(mapDifficulty.Value.DifficultyID);
                    }
                }
            }), Task.Run(() =>
            {
                if (CriteriaTree != null && Achievement != null)
                {
                    ICollection<AchievementEntry> achievementLists = Achievement.Values;
                    var achievements = achievementLists.GroupBy(achievement => achievement.CriteriaTree)
                        .ToDictionary(group => group.Key, group => group.ToList());

                    foreach (var criteriaTree in CriteriaTree)
                    {
                        string result = "";
                        uint criteriaTreeID = criteriaTree.Value.Parent > 0 ? criteriaTree.Value.Parent : (uint)criteriaTree.Key;

                        List<AchievementEntry> achievementList;
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
            }), Task.Run(() =>
            {
                if (Faction != null && FactionTemplate != null)
                {
                    foreach (var factionTemplate in FactionTemplate)
                    {
                        if (Faction.ContainsKey(factionTemplate.Value.Faction))
                            FactionStores.Add((uint)factionTemplate.Key, Faction[factionTemplate.Value.Faction]);
                    }
                }
            }), Task.Run(() =>
            {
                if (SpellEffect != null)
                    foreach (var effect in SpellEffect)
                    {
                        var tuple = Tuple.Create((uint)effect.Value.SpellID, (uint)effect.Value.EffectIndex);
                        SpellEffectStores[tuple] = effect.Value;
                    }
            }), Task.Run(() =>
            {
                if (PhaseXPhaseGroup != null)
                    foreach (var phase in PhaseXPhaseGroup)
                    {
                        if (!Phases.ContainsKey(phase.Value.PhaseGroupID))
                            Phases.Add(phase.Value.PhaseGroupID, new List<ushort>() { phase.Value.PhaseID });
                        else
                            Phases[phase.Value.PhaseGroupID].Add(phase.Value.PhaseID);
                    }
            }));
        }

        public static HashSet<int> GetPhaseGroups(ICollection<ushort> phases)
        {
            if (!phases.Any())
                return new HashSet<int>();

            HashSet<int> phaseGroups = new HashSet<int>();

            foreach (var phaseGroup in Phases)
            {
                foreach (var phase in phaseGroup.Value)
                {
                    if (phases.Contains(phase))
                    {
                        phaseGroups.Add(phaseGroup.Key);
                        break;
                    }
                }
            }

            return phaseGroups;
        }

        public static uint GetEmptyAnimStateID()
        {
            if (AnimationData != null)
                return (uint)AnimationData.Count;
            return 0;
        }

        public static readonly Dictionary<uint, string> Zones = new Dictionary<uint, string>();
        public static readonly Dictionary<int, int> MapSpawnMaskStores = new Dictionary<int, int>();
        public static readonly Dictionary<int, List<int>> MapDifficultyStores = new Dictionary<int, List<int>>();
        public static readonly Dictionary<ushort, string> CriteriaStores = new Dictionary<ushort, string>();
        public static readonly Dictionary<uint, FactionEntry> FactionStores = new Dictionary<uint, FactionEntry>();
        public static readonly Dictionary<Tuple<uint, uint>, SpellEffectEntry> SpellEffectStores = new Dictionary<Tuple<uint, uint>, SpellEffectEntry>();
        public static readonly Dictionary<int, List<ushort>> Phases = new Dictionary<int, List<ushort>>();
    }
}
