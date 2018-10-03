using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapLink)]
    public class UiMapLinkEntry
    {
        [HotfixArray(2)]
        public float[] UiMin { get; set; }
        [HotfixArray(2)]
        public float[] UiMax { get; set; }
        public int ID { get; set; }
        public int ParentUiMapID { get; set; }
        public int OrderIndex { get; set; }
        public int ChildUiMapID { get; set; }
    }
}
