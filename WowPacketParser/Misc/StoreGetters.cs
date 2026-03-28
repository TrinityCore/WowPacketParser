using System;
using System.Collections.Concurrent;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Misc
{
    public static class StoreGetters
    {
        public static readonly ConcurrentDictionary<WowGuid, string> NameDict = new ConcurrentDictionary<WowGuid, string>();

        public static string GetName(StoreNameType type, int entry, bool withEntry = true)
        {
            var entryStr = entry.ToString(CultureInfo.InvariantCulture);

            string name = string.Empty;
            if (Settings.UseDBC)
            {
                switch (type)
                {
                    case StoreNameType.Achievement:
                        if (DBC.DBC.Achievement.TryGetValue(entry, out var achievement))
                            name = achievement.Title;
                        break;
                    case StoreNameType.Area:
                        if (DBC.DBC.AreaTable.TryGetValue(entry, out var area))
                            name = area.AreaName;
                        break;
                    case StoreNameType.Unit:
                        if (DBC.DBC.Creature.TryGetValue(entry, out var creature))
                            name = creature.Name;
                        break;
                    case StoreNameType.CreatureFamily:
                        if (DBC.DBC.CreatureFamily.TryGetValue(entry, out var creatureFamily))
                            name = creatureFamily.Name;
                        break;
                    case StoreNameType.Criteria:
                        if (DBC.DBC.CriteriaStores.TryGetValue((ushort)entry, out var criteriaName))
                            name = criteriaName;
                        break;
                    case StoreNameType.Difficulty:
                        if (DBC.DBC.Difficulty.TryGetValue(entry, out var difficulty))
                            name = difficulty.Name;
                        break;
                    case StoreNameType.Faction:
                        if (DBC.DBC.FactionStores.TryGetValue((uint)entry, out var faction))
                            name = faction.Name;
                        break;
                    case StoreNameType.Item:
                        if (DBC.DBC.ItemSparse.TryGetValue(entry, out var item))
                            name = item.Display;
                        break;
                    case StoreNameType.Map:
                        if (DBC.DBC.Map.TryGetValue(entry, out var map))
                            name = map.MapName;
                        break;
                    case StoreNameType.Spell:
                        if (DBC.DBC.SpellName.TryGetValue(entry, out var spell))
                            name = spell.Name;
                        break;
                    case StoreNameType.Zone:
                        if (DBC.DBC.Zones.TryGetValue((uint)entry, out var zone))
                            name = zone;
                        break;
                    case StoreNameType.Phase:
                        if (DBC.DBC.Phase.TryGetValue(entry, out var phase))
                            name = ((DBCPhaseFlags)phase.Flags).ToString();
                        break;
                }
                if (name != string.Empty)
                {
                    if (withEntry)
                        return entry + " (" + name + ")";
                    return name;
                }
            }

            if (!SQLConnector.Enabled)
                return entryStr;

            if (type != StoreNameType.Map && entry == 0)
                return "0"; // map can be 0

            if (!SQLDatabase.NameStores.TryGetValue(type, out var store))
                return entryStr;

            if (!store.TryGetValue(entry, out name))
                if (!withEntry)
                    return "-Unknown-";

            if (!String.IsNullOrEmpty(name))
            {
                if (withEntry)
                    return entry + " (" + name + ")";
                return name;
            }

            return entryStr;
        }

        public static void AddName(WowGuid guid, string name)
        {
            NameDict.TryAdd(guid, name);
        }

        public static string GetName(WowGuid guid)
        {
            string name;

            if (NameDict.TryGetValue(guid, out name))
                return name;

            return null;
        }
    }
}
