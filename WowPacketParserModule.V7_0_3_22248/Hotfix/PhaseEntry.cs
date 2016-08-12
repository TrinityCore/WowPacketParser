using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Phase, HasIndexInData = false)]
    public class PhaseEntry
    {
        public ushort Flags { get; set; }
    }
}