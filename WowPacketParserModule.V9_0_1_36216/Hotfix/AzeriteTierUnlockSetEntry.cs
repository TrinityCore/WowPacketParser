using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteTierUnlockSet, HasIndexInData = false)]
    public class AzeriteTierUnlockSetEntry
    {
        public int Flags { get; set; }
    }
}
