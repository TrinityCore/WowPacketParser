using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapLink)]
    public class UiMapLinkEntry
    {
        [HotfixArray(2, true)]
        public float[] UiMin { get; set; }
        [HotfixArray(2, true)]
        public float[] UiMax { get; set; }
        public uint ID { get; set; }
        public int ParentUiMapID { get; set; }
        public int OrderIndex { get; set; }
        public int ChildUiMapID { get; set; }
        public int OverrideHighlightFileDataID { get; set; }
        public int OverrideHighlightAtlasID { get; set; }
        public int Flags { get; set; }
    }
}
