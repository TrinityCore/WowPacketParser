using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BroadcastTextVOState, HasIndexInData = false)]
    public class BroadcastTextVOStateEntry
    {
        [HotfixArray(2)]
        public byte[] State { get; set; }
    }
}