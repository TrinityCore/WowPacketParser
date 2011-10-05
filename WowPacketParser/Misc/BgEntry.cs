using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public struct BgEntry
    {
        public BgEntry(int full)
        {
            Full = full;
        }

        public readonly int Full;

        public int GetBgType()
        {
            return (Full & 0x00FF0000) >> 16;
        }

        public int GetUnk()
        {
            return (Full & 0x0000FF00) >> 8;
        }

        public int GetArenaType()
        {
            return (Full & 0x000000FF) >> 0;
        }

        public override string ToString()
        {
            var text = "Type: " + Extensions.BattlegroundLine(GetBgType()) + " Unk: " + GetUnk();
            if (GetArenaType() > 0) // Arenas
                text += " ArenaType: " + GetArenaType();

            return text;
        }
    }
}
