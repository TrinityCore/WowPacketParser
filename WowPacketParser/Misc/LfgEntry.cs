using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public struct LfgEntry
    {
        public LfgEntry(int full)
        {
            Full = full;
        }

        public readonly int Full;

        public LfgType GetLfgType()
        {
            return (LfgType)((Full & 0xFF000000) >> 24);
        }

        public int GetInstanceId()
        {
            return (Full & 0x00FFFFFF) >> 0;
        }

        public override string ToString()
        {
            return "Full: 0x" + Full.ToString("X4") + " Type: " + GetLfgType() + " Instance: " +
                Extensions.DungeonLine(GetInstanceId());
        }
    }
}
