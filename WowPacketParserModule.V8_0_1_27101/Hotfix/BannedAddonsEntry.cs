using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BannedAddOns, HasIndexInData = false)]
    public class BannedAddOnsEntry
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public byte Flags { get; set; }
    }
}
