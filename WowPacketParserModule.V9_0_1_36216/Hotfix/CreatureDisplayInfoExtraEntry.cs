using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfoExtra)]
    public class CreatureDisplayInfoExtraEntry
    {
        public uint ID { get; set; }
        public sbyte DisplayRaceID { get; set; }
        public sbyte DisplaySexID { get; set; }
        public sbyte DisplayClassID { get; set; }
        public sbyte Flags { get; set; }
        public int BakeMaterialResourcesID { get; set; }
        public int HDBakeMaterialResourcesID { get; set; }
    }
}
