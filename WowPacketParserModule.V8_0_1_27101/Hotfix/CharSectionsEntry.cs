using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharSections, HasIndexInData = false)]
    public class CharSectionsEntry
    {
        public sbyte RaceID { get; set; }
        public sbyte SexID { get; set; }
        public sbyte BaseSection { get; set; }
        public sbyte VariationIndex { get; set; }
        public sbyte ColorIndex { get; set; }
        public ushort Flags { get; set; }
        [HotfixArray(3)]
        public int[] MaterialResourcesID { get; set; }
    }
}
