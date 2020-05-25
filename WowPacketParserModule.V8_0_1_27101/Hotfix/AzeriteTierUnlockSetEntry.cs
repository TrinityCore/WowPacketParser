using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteTierUnlockSet, HasIndexInData = false)]
    public class AzeriteTierUnlockSetEntry
    {
        public int Flags { get; set; }
    }
}
