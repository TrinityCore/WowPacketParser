using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AdventureMapPOI, HasIndexInData = false)]
    public class AdventureMapPOIEntry
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [HotfixArray(2)]
        public float[] WorldPosition { get; set; }
        public sbyte Type { get; set; }
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
