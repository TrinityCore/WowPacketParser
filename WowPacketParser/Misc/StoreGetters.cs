using System;
using System.Collections.Concurrent;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.SQL;
using WowPacketParser.Store;

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
                        if (DBC.DBC.Achievement.ContainsKey(entry))
                            name = DBC.DBC.Achievement[entry].Title;
                        break;
                    case StoreNameType.Area:
                        if (DBC.DBC.AreaTable.ContainsKey(entry))
                            name = DBC.DBC.AreaTable[entry].AreaName;
                        break;
                    case StoreNameType.Unit:
                        if (DBC.DBC.Creature.ContainsKey(entry))
                            name = DBC.DBC.Creature[entry].Name;
                        break;
                    case StoreNameType.CreatureFamily:
                        if (DBC.DBC.CreatureFamily.ContainsKey(entry))
                            name = DBC.DBC.CreatureFamily[entry].Name;
                        break;
                    case StoreNameType.Criteria:
                        if (DBC.DBC.CriteriaStores.ContainsKey((ushort)entry))
                            name = DBC.DBC.CriteriaStores[(ushort)entry];
                        break;
                    case StoreNameType.Difficulty:
                        if (DBC.DBC.Difficulty.ContainsKey(entry))
                            name = DBC.DBC.Difficulty[entry].Name;
                        break;
                    case StoreNameType.Faction:
                        if (DBC.DBC.FactionStores.ContainsKey((uint)entry))
                            name = DBC.DBC.FactionStores[(uint)entry].Name;
                        break;
                    case StoreNameType.Item:
                        if (DBC.DBC.ItemSparse.ContainsKey(entry))
                            name = DBC.DBC.ItemSparse[entry].Display;
                        break;
                    case StoreNameType.Map:
                        if (DBC.DBC.Map.ContainsKey(entry))
                            name = DBC.DBC.Map[entry].MapName;
                        break;
                    case StoreNameType.Spell:
                        if (DBC.DBC.SpellName.ContainsKey(entry))
                            name = DBC.DBC.SpellName[entry].Name;
                        break;
                    case StoreNameType.Zone:
                        if (DBC.DBC.Zones.ContainsKey((uint)entry))
                            name = DBC.DBC.Zones[(uint)entry];
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

            if (!SQLDatabase.NameStores.ContainsKey(type))
                return entryStr;

            if (!SQLDatabase.NameStores[type].TryGetValue(entry, out name))
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
