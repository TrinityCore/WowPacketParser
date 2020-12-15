using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureType, HasIndexInData = false)]
    public class CreatureTypeEntry
    {
        public string Name { get; set; }
        public byte Flags { get; set; }
    }
}
