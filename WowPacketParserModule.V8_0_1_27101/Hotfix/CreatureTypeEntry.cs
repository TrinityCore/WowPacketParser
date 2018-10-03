using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureType, HasIndexInData = false)]
    public class CreatureTypeEntry
    {
        public string Name { get; set; }
        public byte Flags { get; set; }
    }
}
