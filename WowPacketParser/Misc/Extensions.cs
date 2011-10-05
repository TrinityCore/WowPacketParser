using System;
using WowPacketParser.DBC.DBCStructures;

namespace WowPacketParser.Misc
{
    public static class Extensions
    {
        public static byte ToByte(this bool boolean)
        {
            return (byte)(boolean ? 1 : 0);
        }

        public static bool HasAnyFlag(this Enum value, Enum toTest)
        {
            var val = ((IConvertible)value).ToUInt64(null);
            var test = ((IConvertible)toTest).ToUInt64(null);

            return (val & test) != 0;
        }

        // TODO: Merge the next 6 methods and move them to somewhere else

        public static string GetExistingSpellName(int spellId)
        {
            if (!DBC.DBCStore.DBC.Enabled()) // Could use a more general solution here
                return string.Empty;
            SpellEntry spell;
            if (spellId <= 0)
                return string.Empty;
            if (!DBC.DBCStore.DBC.Spell.TryGetValue((uint)spellId, out spell))
                return "-Unknown-";
            return spell.GetSpellName();
        }

        public static string SpellLine(int spellId)
        {
            if (spellId == 0)
                return "0";
            var name = GetExistingSpellName(spellId);
            if (!String.IsNullOrEmpty(name))
                return spellId + " (" + name + ")";
            return spellId.ToString();
        }

        public static string GetExistingMapName(int mapId)
        {
            if (!DBC.DBCStore.DBC.Enabled()) // Could use a more general solution here
                return string.Empty;
            if (mapId < 0)
                return string.Empty;
            MapEntry map;
            if (!DBC.DBCStore.DBC.Map.TryGetValue((uint)mapId, out map))
                return "-Unknown-";
            return map.GetMapName();
        }

        public static string MapLine(int mapId)
        {
            var name = GetExistingMapName(mapId);
            if (!String.IsNullOrEmpty(name))
                return mapId + " (" + name + ")";
            return mapId.ToString();
        }

        public static string GetExistingLFGDungeonsName(int dungeonId)
        {
            if (!DBC.DBCStore.DBC.Enabled()) // Could use a more general solution here
                return string.Empty;
            if (dungeonId <= 0)
                return string.Empty;
            LFGDungeonsEntry dungeon;
            if (!DBC.DBCStore.DBC.LFGDungeons.TryGetValue((uint)dungeonId, out dungeon))
                return "-Unknown-";
            return dungeon.GetName();
        }

        public static string DungeonLine(int dungeonId)
        {
            if (dungeonId == 0)
                return "0";
            var name = GetExistingLFGDungeonsName(dungeonId);
            if (!String.IsNullOrEmpty(name))
                return dungeonId + " (" + name + ")";
            return dungeonId.ToString();
        }

        public static string GetExistingBattlegroundName(int id)
        {
            if (!DBC.DBCStore.DBC.Enabled()) // Could use a more general solution here
                return string.Empty;
            if (id <= 0)
                return string.Empty;

            BattlemasterListEntry data;
            if (!DBC.DBCStore.DBC.BattlemasterList.TryGetValue((uint)id, out data))
                return "-Unknown-";
            return data.GetName();
        }

        public static string BattlegroundLine(int id)
        {
            if (id == 0)
                return "0";
            var name = GetExistingBattlegroundName(id);
            if (!String.IsNullOrEmpty(name))
                return id + " (" + name + ")";
            return id.ToString();
        }
    }
}
