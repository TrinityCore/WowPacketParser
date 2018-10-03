using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WorldElapsedTimer, HasIndexInData = false)]
    public class WorldElapsedTimerEntry
    {
        public string Name { get; set; }
        public byte Type { get; set; }
        public byte Flags { get; set; }
    }
}
