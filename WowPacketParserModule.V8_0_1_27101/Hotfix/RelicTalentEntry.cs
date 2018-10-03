using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.RelicTalent, HasIndexInData = false)]
    public class RelicTalentEntry
    {
        public int Type { get; set; }
        public ushort ArtifactPowerID { get; set; }
        public byte ArtifactPowerLabel { get; set; }
        public int PVal { get; set; }
        public int Flags { get; set; }
    }
}
