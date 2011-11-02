using System;
using System.Text;
using WowPacketParser.DBC.DBCStructures;

namespace WowPacketParser.Misc
{
    public static class Extensions
    {
        public static byte ToByte(this bool boolean)
        {
            return (byte)(boolean ? 1 : 0);
        }

        public static bool HasAnyFlag(this Enum value, Enum flag)
        {
            var uFlag = ((IConvertible)flag).ToUInt64(null);
            var uThis = ((IConvertible)value).ToUInt64(null);

            return (uThis & uFlag) != 0;
        }

        public static bool MatchesFilters(this string value, string[] filters)
        {
            foreach (var filter in filters)
                if (value.Contains(filter))
                    return true;

            return false;
        }

        public static string AsHex(this Packet packet)
        {
            var n = Environment.NewLine;
            var hexDump = new StringBuilder();
            var length = packet.GetLength();
            var stream = packet.GetStream(0);

            var header = "|-------------------------------------------------|------------------" +
                         "---------------|" + n +
                         "| 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F | 0 1 2 3 4 5 6 7 8 9 A B C D E F |" +
                         n + "|-------------------------------------------------|------------------" +
                         "---------------|" + n;

            hexDump.Append(header);

            var end = length;
            for (var i = 0; i < end; i += 16)
            {
                var text = new StringBuilder();
                var hex = new StringBuilder();
                hex.Append("| ");

                for (var j = 0; j < 16; j++)
                {
                    if (j + i < end)
                    {
                        var val = stream[j + i];
                        hex.Append(stream[j + i].ToString("X2"));
                        hex.Append(" ");

                        if (val >= 32 && val <= 127)
                            text.Append((char)val);
                        else
                            text.Append(".");

                        text.Append(" ");
                    }
                    else
                    {
                        hex.Append("   ");
                        text.Append("  ");
                    }
                }

                hex.Append("| ");
                hex.Append(text + "|");
                hex.Append(n);
                hexDump.Append(hex.ToString());
            }

            hexDump.Append("|-------------------------------------------------|------------------" +
                           "---------------|");

            return hexDump.ToString();
        }

        // TODO: Merge the next 6 methods and move them to somewhere else

        public static string GetExistingSpellName(int spellId)
        {
            if (!DBC.DBCStore.DBC.Enabled) // Could use a more general solution here
                return String.Empty;
            SpellEntry spell;
            if (spellId <= 0)
                return String.Empty;
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
            if (!DBC.DBCStore.DBC.Enabled) // Could use a more general solution here
                return String.Empty;
            if (mapId < 0)
                return String.Empty;
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
            if (!DBC.DBCStore.DBC.Enabled) // Could use a more general solution here
                return String.Empty;
            if (dungeonId <= 0)
                return String.Empty;
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
            if (!DBC.DBCStore.DBC.Enabled) // Could use a more general solution here
                return String.Empty;
            if (id <= 0)
                return String.Empty;

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
