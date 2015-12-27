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


            switch (type)
            {
                case StoreNameType.Item:
                    if (DBC.DBC.ItemSparse.ContainsKey((uint)entry))
                        return DBC.DBC.ItemSparse[(uint)entry].Name;
                    break;
                case StoreNameType.Spell:
                    if (DBC.DBC.Spell.ContainsKey((uint)entry))
                        return DBC.DBC.Spell[(uint)entry].SpellName;
                    break;
                case StoreNameType.Achievement:
                    if (DBC.DBC.Achievement.ContainsKey((uint)entry))
                        return DBC.DBC.Achievement[(uint)entry].Title;
                    break;
                case StoreNameType.Criteria:
                    if (DBC.DBC.CriteriaStores.ContainsKey((uint) entry))
                        return DBC.DBC.CriteriaStores[(uint) entry];
                    break;
                case StoreNameType.Map:
                    if (DBC.DBC.Map.ContainsKey((uint)entry))
                        return DBC.DBC.Map[(uint)entry].MapName_lang;
                    break;
                case StoreNameType.Area:
                    if (DBC.DBC.AreaTable.ContainsKey((uint)entry))
                        return DBC.DBC.AreaTable[(uint)entry].AreaName;
                    break;
                case StoreNameType.Zone:
                    if (DBC.DBC.Zones.ContainsKey((uint)entry))
                        return DBC.DBC.Zones[(uint)entry];
                    break;
                case StoreNameType.Difficulty:
                    if (DBC.DBC.Difficulty.ContainsKey((uint)entry))
                        return DBC.DBC.Difficulty[(uint)entry].NameLang;
                    break;
                case StoreNameType.CreatureFamily:
                    if (DBC.DBC.CreatureFamily.ContainsKey((uint)entry))
                        return DBC.DBC.CreatureFamily[(uint)entry].Name_lang;
                    break;
                case StoreNameType.Faction:
                    if (DBC.DBC.FactionStores.ContainsKey((uint)entry))
                        return DBC.DBC.FactionStores[(uint)entry];
                    break;
            }

            if (!SQLConnector.Enabled)
                return entryStr;

            if (type != StoreNameType.Map && entry == 0)
                return "0"; // map can be 0

            if (!SQLDatabase.NameStores.ContainsKey(type))
                return entryStr;

            string name;
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
