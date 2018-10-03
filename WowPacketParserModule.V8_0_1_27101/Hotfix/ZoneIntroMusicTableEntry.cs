using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ZoneIntroMusicTable, HasIndexInData = false)]
    public class ZoneIntroMusicTableEntry
    {
        public string Name { get; set; }
        public uint SoundID { get; set; }
        public byte Priority { get; set; }
        public ushort MinDelayMinutes { get; set; }
    }
}
