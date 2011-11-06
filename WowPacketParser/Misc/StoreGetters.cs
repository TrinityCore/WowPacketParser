using System;
using WowPacketParser.DBC.DBCStructures;
using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Misc
{
    public static class StoreGetters
    {
        public static string GetExistingDBCString(StoreNameType type, int entry)
        {
            if (entry <= 0 || !DBC.DBCStore.DBC.Enabled)
                return string.Empty;

            switch (type)
            {
                case StoreNameType.Spell:
                {
                    SpellEntry spell;
                    if (DBC.DBCStore.DBC.Spell.TryGetValue((uint)entry, out spell))
                        return spell.GetSpellName();
                    break;                    
                }
                case StoreNameType.Map:
                {
                    MapEntry map;
                    if (DBC.DBCStore.DBC.Map.TryGetValue((uint)entry, out map))
                        return map.GetMapName();
                    break;
                }
                case StoreNameType.LFGDungeon:
                {
                    LFGDungeonsEntry dungeon;
                    if (DBC.DBCStore.DBC.LFGDungeons.TryGetValue((uint)entry, out dungeon))
                        return dungeon.GetName();
                    break;
                }
                case StoreNameType.Battleground:
                {
                    BattlemasterListEntry data;
                    if (DBC.DBCStore.DBC.BattlemasterList.TryGetValue((uint)entry, out data))
                        return data.GetName();
                    break;
                }
                default:
                    return string.Empty;
            }
            return "-Unknown-";
        }

        public static string GetExistingDatabaseString(StoreNameType type, int entry)
        {
            if (entry <= 0 || !SQLConnector.Enabled)
                return string.Empty;

            string name = "-Unknown-";

            switch (type)
            {
                case StoreNameType.Unit:
                {
                    if (SQLDatabase.UnitNames.TryGetValue((uint)entry, out name))
                        return name;
                    break;
                }
                case StoreNameType.GameObject:
                {
                    if (SQLDatabase.GameObjectNames.TryGetValue((uint)entry, out name))
                        return name;
                    break;
                }
                case StoreNameType.Item:
                {
                    if (SQLDatabase.ItemNames.TryGetValue((uint)entry, out name))
                        return name;
                    break;
                }
                case StoreNameType.Quest:
                {
                    if (SQLDatabase.QuestNames.TryGetValue((uint)entry, out name))
                        return name;
                    break;
                }
                default:
                    return string.Empty;
            }
            return name;
        }

        public static string GetName(StoreNameType type, int entry)
        {
            if (type != StoreNameType.Map && entry <= 0)
                return "0"; // map can be 0

            var name = (type == StoreNameType.Unit || type == StoreNameType.GameObject
                 || type == StoreNameType.Item || type == StoreNameType.Quest)
                              ? GetExistingDatabaseString(type, entry)
                              : GetExistingDBCString(type, entry);

            if (!String.IsNullOrEmpty(name))
                return entry + " (" + name + ")";

            return entry.ToString();
        }
    }
}
