using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
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
        public uint LevelRangeMin { get; set; }
        public uint LevelRangeMax { get; set; }
        public int BountySetID { get; set; }
        public uint BountyDisplayLocation { get; set; }
        public int VisibilityPlayerConditionID { get; set; }
        public sbyte HelpTextPosition { get; set; }
        public int BkgAtlasID { get; set; }
    }
}
