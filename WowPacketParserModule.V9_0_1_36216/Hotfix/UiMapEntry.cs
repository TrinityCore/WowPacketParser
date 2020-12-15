using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.UiMap)]
    public class UiMapEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public int ParentUiMapID { get; set; }
        public int Flags { get; set; }
        public int System { get; set; }
        public int Type { get; set; }
        public int BountySetID { get; set; }
        public uint BountyDisplayLocation { get; set; }
        public int VisibilityPlayerConditionID { get; set; }
        public sbyte HelpTextPosition { get; set; }
        public int BkgAtlasID { get; set; }
        public int AlternateUiMapGroup { get; set; }
        public int ContentTuningID { get; set; }
    }
}
