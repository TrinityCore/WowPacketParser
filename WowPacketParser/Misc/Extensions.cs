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

        public static string GetExistingSpellName(int spellId)
        {
            if (!DBC.DBCStore.DBC.Enabled()) // Could use a more general solution here
                return string.Empty;
            SpellEntry spell;
            if (spellId <= 0)
                return string.Empty;
            try
            {
                DBC.DBCStore.DBC.Spell.TryGetValue((uint)spellId, out spell);
            }
            catch(Exception)
            {
                return "-Unknown-";
            }
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
            if (mapId <= 0)
                return string.Empty;
            MapEntry map;
            try
            {
                DBC.DBCStore.DBC.Map.TryGetValue((uint)mapId, out map);
            }
            catch(Exception)
            {
                return "-Unknown-";
            }
            return map.GetMapName();
        }

        public static string MapLine(int mapId)
        {
            if (mapId == 0)
                return "0";
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
            try
            {
                DBC.DBCStore.DBC.LFGDungeons.TryGetValue((uint)dungeonId, out dungeon);
            }
            catch(Exception)
            {
                return "-Unknown-";
            }
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
    }
}
