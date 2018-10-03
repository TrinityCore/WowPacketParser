using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LfgDungeonExpansion, HasIndexInData = false)]
    public class LfgDungeonExpansionEntry
    {
        public byte ExpansionLevel { get; set; }
        public ushort RandomID { get; set; }
        public byte HardLevelMin { get; set; }
        public byte HardLevelMax { get; set; }
        public int TargetLevelMin { get; set; }
        public int TargetLevelMax { get; set; }
        public ushort LfgID { get; set; }
    }
}
