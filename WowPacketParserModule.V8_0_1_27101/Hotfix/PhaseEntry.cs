using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Phase, HasIndexInData = false)]
    public class PhaseEntry
    {
        public ushort Flags { get; set; }
    }
}
