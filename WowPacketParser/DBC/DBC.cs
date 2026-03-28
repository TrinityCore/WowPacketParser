using DBFileReaderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WowPacketParser.DBC.Structures.Midnight;
using WowPacketParser.Misc;

namespace WowPacketParser.DBC
{
    public static class DBC
    {
        internal static Storage<AreaTableEntry> AreaTable { get; set; }
        internal static Storage<AchievementEntry> Achievement { get; set; }
        internal static Storage<AnimationDataEntry> AnimationData { get; set; }
        internal static Storage<BroadcastTextEntry> BroadcastText { get; set; }
        internal static Storage<BroadcastTextDurationEntry> BroadcastTextDuration { get; set; }
        internal static Storage<ConversationLineEntry> ConversationLine { get; set; }
        internal static Storage<CreatureEntry> Creature { get; set; }
        internal static Storage<CreatureDifficultyEntry> CreatureDifficulty { get; set; }
        internal static Storage<CreatureFamilyEntry> CreatureFamily { get; set; }
        internal static Storage<CreatureDisplayInfoEntry> CreatureDisplayInfo { get; set; }
        internal static Storage<CriteriaTreeEntry> CriteriaTree { get; set; }
        internal static Storage<CriteriaEntry> Criteria { get; set; }
        internal static Storage<DifficultyEntry> Difficulty { get; set; }
        internal static Storage<FactionEntry> Faction { get; set; }
        internal static Storage<FactionTemplateEntry> FactionTemplate { get; set; }
        internal static Storage<ItemEntry> Item { get; set; }
        internal static Storage<ItemSparseEntry> ItemSparse { get; set; }
        internal static Storage<MapEntry> Map { get; set; }
        internal static Storage<MapDifficultyEntry> MapDifficulty { get; set; }
        internal static Storage<QuestLineXQuestEntry> QuestLineXQuest { get; set; }
        internal static Storage<PhaseEntry> Phase { get; set; }
        internal static Storage<PhaseXPhaseGroupEntry> PhaseXPhaseGroup { get; set; }
        internal static Storage<SpellEffectEntry> SpellEffect { get; set; }
        internal static Storage<SpellNameEntry> SpellName { get; set; }

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

        public static async Task Load()
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

            await Task.WhenAll(typeof(DBC).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Select(dbc => Task.Run(() =>
            {
                Type type = dbc.PropertyType.GetGenericArguments()[0];

                if (!type.IsClass)
                    return;

                var startTime = DateTime.Now;
                var attr = type.GetCustomAttribute<DBFileAttribute>();
                if (attr == null)
                    return;

                var instanceType = dbc.PropertyType;
                var countGetter = instanceType.GetProperty("Count").GetGetMethod();
                dynamic instance = Activator.CreateInstance(instanceType, $"{GetDBCPath(attr.FileName)}.db2");
                var recordCount = (int)countGetter.Invoke(instance, Array.Empty<object>());

                try
                {
                    var db2Reader = new DBReader($"{GetDBCPath(attr.FileName)}.db2");

                    if (hotfixReader != null)
                        hotfixReader.ApplyHotfixes(instance, db2Reader);

                    dbc.SetValue(dbc.GetValue(null), instance);
                }
                catch (TargetInvocationException tie)
                {
                    if (tie.InnerException is ArgumentException)
                        throw new ArgumentException(
                            $"Failed to load {attr.FileName}.db2: {tie.InnerException.Message}");
                    throw;
                }

                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);

                Trace.WriteLine($"{attr.FileName,-33} {span,-28} {recordCount,-19}");
            })));

            await Task.WhenAll(Task.Run(() =>
            {
                if (AreaTable == null)
                    return;

                foreach (var db2Info in AreaTable)
                    if (db2Info.Value.ParentAreaID != 0 && !Zones.ContainsKey(db2Info.Value.ParentAreaID))
                        Zones.Add(db2Info.Value.ParentAreaID, db2Info.Value.ZoneName);
            }), Task.Run(() =>
            {
                if (MapDifficulty == null)
                    return;

                foreach (var mapDifficulty in MapDifficulty)
                {
                    if (!MapDifficultyStores.TryGetValue(mapDifficulty.Value.MapID, out var difficulty))
                        MapDifficultyStores.Add(mapDifficulty.Value.MapID, [mapDifficulty.Value.DifficultyID]);
                    else
                        difficulty.Add(mapDifficulty.Value.DifficultyID);
                }
            }), Task.Run(() =>
            {
                if (CriteriaTree == null || Achievement == null)
                    return;

                var achievements = Achievement.Select(kvp => kvp.Value)
                    .Where(achievement => achievement.CriteriaTree != 0)
                    .GroupBy(achievement => achievement.CriteriaTree)
                    .ToDictionary(
                        group => group.Key,
                        group => string.Join(' ', group.Select(achiev => $"AchievementID: {achiev.ID} Description: \"{achiev.Description}\"")));

                foreach (var criteriaTree in CriteriaTree)
                {
                    if (criteriaTree.Value.CriteriaID == 0)
                        continue;

                    string result = "";
                    uint criteriaTreeID = criteriaTree.Value.Parent > 0 ? criteriaTree.Value.Parent : (uint)criteriaTree.Key;

                    if (achievements.TryGetValue(criteriaTreeID, out var achievementList))
                        result += achievementList;

                    if (!CriteriaStores.ContainsKey((ushort)criteriaTree.Value.CriteriaID))
                    {
                        if (criteriaTree.Value.Description != string.Empty)
                            result += $" - CriteriaDescription: \"{criteriaTree.Value.Description }\"";

                        CriteriaStores.Add((ushort)criteriaTree.Value.CriteriaID, result);
                    }
                    else
                        CriteriaStores[(ushort)criteriaTree.Value.CriteriaID] += $" / CriteriaDescription: \"{ criteriaTree.Value.Description }\"";
                }
            }), Task.Run(() =>
            {
                if (Faction == null || FactionTemplate == null)
                    return;

                foreach (var factionTemplate in FactionTemplate)
                    if (Faction.TryGetValue(factionTemplate.Value.Faction, out var faction))
                        FactionStores.Add((uint)factionTemplate.Key, faction);
            }), Task.Run(() =>
            {
                if (SpellEffect == null)
                    return;

                foreach (var effect in SpellEffect)
                {
                    var tuple = Tuple.Create((uint)effect.Value.SpellID, (uint)effect.Value.EffectIndex);
                    SpellEffectStores[tuple] = effect.Value;
                }
            }), Task.Run(() =>
            {
                if (PhaseXPhaseGroup == null)
                    return;

                foreach (var phase in PhaseXPhaseGroup)
                {
                    if (!PhasesByGroup.TryGetValue(phase.Value.PhaseGroupID, out var phases))
                        PhasesByGroup.Add(phase.Value.PhaseGroupID, [phase.Value.PhaseID]);
                    else
                        phases.Add(phase.Value.PhaseID);

                    if (!PhaseGroupsByPhase.TryGetValue(phase.Value.PhaseID, out var phaseGroups))
                        PhaseGroupsByPhase.Add(phase.Value.PhaseID, [phase.Value.PhaseGroupID]);
                    else
                        phaseGroups.Add(phase.Value.PhaseGroupID);
                }
            }), Task.Run(() =>
            {
                if (BroadcastTextDuration == null)
                    return;

                foreach (var broadcastTextDuration in BroadcastTextDuration)
                {
                    if (!BroadcastTextDurations.TryGetValue(broadcastTextDuration.Value.BroadcastTextID, out var durations))
                        BroadcastTextDurations.Add(broadcastTextDuration.Value.BroadcastTextID, [broadcastTextDuration.Value.Locale]);
                    else
                        durations.Add(broadcastTextDuration.Value.Locale);
                }
            }));
        }

        public static HashSet<int> GetPhaseGroups(ICollection<ushort> phases)
        {
            if (phases.Count == 0)
                return new HashSet<int>();

            var phaseGroups = new HashSet<int>();

            foreach (var phase in phases)
                if (PhaseGroupsByPhase.TryGetValue(phase, out var phaseGroupsIds))
                    phaseGroups.UnionWith(phaseGroupsIds);

            return phaseGroups;
        }

        public static uint GetEmptyAnimStateID()
        {
            if (AnimationData != null)
                return (uint)AnimationData.Count;
            return 0;
        }

        public static readonly Dictionary<uint, string> Zones = new Dictionary<uint, string>();
        public static readonly Dictionary<int, List<int>> MapDifficultyStores = new Dictionary<int, List<int>>();
        public static readonly Dictionary<ushort, string> CriteriaStores = new Dictionary<ushort, string>();
        public static readonly Dictionary<uint, FactionEntry> FactionStores = new Dictionary<uint, FactionEntry>();
        public static readonly Dictionary<Tuple<uint, uint>, SpellEffectEntry> SpellEffectStores = new Dictionary<Tuple<uint, uint>, SpellEffectEntry>();
        public static readonly Dictionary<int, List<ushort>> PhasesByGroup = new Dictionary<int, List<ushort>>();
        private static readonly Dictionary<ushort, List<int>> PhaseGroupsByPhase = new Dictionary<ushort, List<int>>();
        public static readonly Dictionary<int, HashSet<int>> BroadcastTextDurations = new Dictionary<int, HashSet<int>>();
    }
}
