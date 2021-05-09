using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AdventureMapPoi, HasIndexInData = false)]
    public class AdventureMapPoiEntry
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [HotfixArray(2, true)]
        public float WorldPosition { get; set; }
        public byte Type { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint QuestID { get; set; }
        public uint LfgDungeonID { get; set; }
        public int RewardItemID { get; set; }
        public uint UiTextureAtlasMemberID { get; set; }
        public uint UiTextureKitID { get; set; }
        public int MapID { get; set; }
        public uint AreaTableID { get; set; }
    }
}
