using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SkillRaceClassInfo, HasIndexInData = false)]
    public class SkillRaceClassInfoEntry
    {
        public long RaceMask { get; set; }
        public short SkillID { get; set; }
        public int ClassMask { get; set; }
        public ushort Flags { get; set; }
        public sbyte Availability { get; set; }
        public sbyte MinLevel { get; set; }
        public short SkillTierID { get; set; }
    }
}
