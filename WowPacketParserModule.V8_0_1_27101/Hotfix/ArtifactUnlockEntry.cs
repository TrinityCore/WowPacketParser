using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactUnlock, HasIndexInData = false)]
    public class ArtifactUnlockEntry
    {
        public uint PowerID { get; set; }
        public byte PowerRank { get; set; }
        public ushort ItemBonusListID { get; set; }
        public uint PlayerConditionID { get; set; }
        public byte ArtifactID { get; set; }
    }
}
