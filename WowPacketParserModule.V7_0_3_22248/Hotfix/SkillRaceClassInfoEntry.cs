using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SkillRaceClassInfo, HasIndexInData = false)]
    public class SkillRaceClassInfoEntry
    {
        public int RaceMask { get; set; }
        public ushort SkillID { get; set; }
        public ushort Flags { get; set; }
        public ushort SkillTierID { get; set; }
        public byte Availability { get; set; }
        public byte MinLevel { get; set; }
        public int ClassMask { get; set; }
    }
}