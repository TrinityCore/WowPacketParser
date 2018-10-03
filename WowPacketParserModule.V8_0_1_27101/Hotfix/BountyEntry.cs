using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Bounty, HasIndexInData = false)]
    public class BountyEntry
    {
        public ushort QuestID { get; set; }
        public ushort FactionID { get; set; }
        public uint IconFileDataID { get; set; }
        public uint TurninPlayerConditionID { get; set; }
        public byte BountySetID { get; set; }
    }
}
