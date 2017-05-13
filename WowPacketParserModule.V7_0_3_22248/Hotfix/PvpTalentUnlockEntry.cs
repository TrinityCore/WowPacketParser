using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTalentUnlock, HasIndexInData = false)]
    public class PvpTalentUnlockEntry
    {
        public uint TierID { get; set; }
        public uint ColumnIndex { get; set; }
        public uint HonorLevel { get; set; }
    }
}
