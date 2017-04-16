using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DBFilesClient.NET;
using WowPacketParser.DBC.Structures;
using WowPacketParser.Misc;
using WowPacketParser.Loading;

namespace WowPacketParser.DBC
{
    public static class DBC
    {
        public static Storage<AreaTableEntry> AreaTable = new Storage<AreaTableEntry>(GetPath() + "AreaTable.db2");
        public static Storage<AchievementEntry> Achievement = new Storage<AchievementEntry>(GetPath() + "Achievement.db2");
        public static Storage<BroadcastTextEntry> BroadcastText = new Storage<BroadcastTextEntry>(GetPath() + "BroadcastText.db2");
        public static Storage<CreatureEntry> Creature = new Storage<CreatureEntry> (GetPath() + "Creature.db2");
        public static Storage<CreatureDifficultyEntry> CreatureDifficulty = new Storage<CreatureDifficultyEntry>(GetPath() + "CreatureDifficulty.db2");
        public static Storage<CreatureFamilyEntry> CreatureFamily = new Storage<CreatureFamilyEntry>(GetPath() + "CreatureFamily.db2");
        public static Storage<CreatureDisplayInfoEntry> CreatureDisplayInfo = new Storage<CreatureDisplayInfoEntry>(GetPath() + "CreatureDisplayInfo.db2");
        public static Storage<CriteriaTreeEntry> CriteriaTree = new Storage<CriteriaTreeEntry>(GetPath() + "CriteriaTree.db2");
        public static Storage<DifficultyEntry> Difficulty = new Storage<DifficultyEntry>(GetPath() + "Difficulty.db2");
        public static Storage<FactionEntry> Faction = new Storage<FactionEntry>(GetPath() + "Faction.db2");
        public static Storage<FactionTemplateEntry> FactionTemplate = new Storage<FactionTemplateEntry>(GetPath() + "FactionTemplate.db2");
        public static Storage<ItemEntry> Item = new Storage<ItemEntry>(GetPath() + "Item.db2");
        public static Storage<ItemSparseEntry> ItemSparse = new Storage<ItemSparseEntry>(GetPath() + "ItemSparse.db2");
        public static Storage<MapEntry> Map = new Storage<MapEntry>(GetPath() + "Map.db2");
        public static Storage<MapDifficultyEntry> MapDifficulty = new Storage<MapDifficultyEntry>(GetPath() + "MapDifficulty.db2");
        public static Storage<PhaseXPhaseGroupEntry> PhaseXPhaseGroup = new Storage<PhaseXPhaseGroupEntry>(GetPath() + "PhaseXPhaseGroup.db2");
        public static Storage<SoundKitEntry> SoundKit = new Storage<SoundKitEntry>(GetPath() + "SoundKit.db2");
        public static Storage<SpellEntry> Spell = new Storage<SpellEntry>(GetPath() + "Spell.db2");
        public static Storage<SpellEffectEntry> SpellEffect = new Storage<SpellEffectEntry>(GetPath() + "SpellEffect.db2");

        private static string GetPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Settings.DBCPath + @"\" + BinaryPacketReader.GetLocale() + @"\";
        }

        public static async void Load()
        {
            if (!Directory.Exists(GetPath()))
            {
                Trace.WriteLine($"DBC folder \"{ GetPath() }\" not found");
                return;
            }
            else
                Trace.WriteLine($"DBC folder \"{ GetPath() }\" found");

            Trace.WriteLine("File name                           LoadTime             Record count");
            Trace.WriteLine("---------------------------------------------------------------------");

            Parallel.ForEach(Assembly.GetAssembly(typeof(DBC)).GetTypes(), type =>
            {
                if (!type.IsClass)
                    return;

                var startTime = DateTime.Now;
                var attr = type.GetCustomAttribute<DBFileAttribute>();
                if (attr == null)
                    return;

                var times = new List<long>();
                var recordCount = 0;
                var instanceType = typeof(Storage<>).MakeGenericType(type);
                var countGetter = instanceType.GetProperty("Count").GetGetMethod();
                var instance = Activator.CreateInstance(instanceType, $"{ GetPath() + attr.FileName }.db2", true);

                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);

                if (recordCount == 0)
                    recordCount = (int)countGetter.Invoke(instance, new object[] { });

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
                        ushort criteriaTreeID = criteriaTree.Value.Parent > 0 ? criteriaTree.Value.Parent : (ushort)criteriaTree.Key;

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
                            FactionStores.Add((uint)factionTemplate.Key, Faction[factionTemplate.Value.Faction].Name);
                    }
                }
            }), Task.Run(() =>
            {
                if (SpellEffect != null)
                    foreach (var effect in SpellEffect)
                    {
                        var tuple = Tuple.Create(effect.Value.SpellID, effect.Value.EffectIndex);
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

        public static HashSet<ushort> GetPhaseGroups(HashSet<ushort> phases)
        {
            if (!phases.Any())
                return new HashSet<ushort>();

            HashSet<ushort> phaseGroups = new HashSet<ushort>();

            foreach (var phaseGroup in Phases)
            {
                bool valid = true;

                foreach (var phase in phaseGroup.Value)
                {
                    if (!phases.Contains(phase))
                        valid = false;
                }

                if (valid)
                {
                    Trace.WriteLine($"PhaseGroup: { phaseGroup.Key } Phases: { string.Join(" - ", phaseGroup.Value) }");
                    phaseGroups.Add(phaseGroup.Key);
                }
            }

            return phaseGroups;
        }

        public static readonly Dictionary<uint, string> Zones = new Dictionary<uint, string>();
        public static readonly Dictionary<int, int> MapSpawnMaskStores = new Dictionary<int, int>();
        public static readonly Dictionary<ushort, string> CriteriaStores = new Dictionary<ushort, string>();
        public static readonly Dictionary<uint, string> FactionStores = new Dictionary<uint, string>();
        public static readonly Dictionary<Tuple<uint, uint>, SpellEffectEntry> SpellEffectStores = new Dictionary<Tuple<uint, uint>, SpellEffectEntry>();
        public static readonly Dictionary<ushort, List<ushort>> Phases = new Dictionary<ushort, List<ushort>>();
    }
}
