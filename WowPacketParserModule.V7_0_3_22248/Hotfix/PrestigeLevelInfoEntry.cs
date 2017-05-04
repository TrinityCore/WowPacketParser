using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PrestigeLevelInfo, HasIndexInData = false)]
    public class PrestigeLevelInfoEntry
    {
        public uint IconID { get; set; }
        public string PrestigeText { get; set; }
        public byte PrestigeLevel { get; set; }
        public byte Flags { get; set; }
    }
}
