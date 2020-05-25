using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteEssence)]
    public class AzeriteEssenceEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        public int SpecSetID { get; set; }
    }
}
