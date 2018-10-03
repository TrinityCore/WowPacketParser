using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrItemLevelUpgradeData)]
    public class GarrItemLevelUpgradeDataEntry
    {
        public int ID { get; set; }
        public int Operation { get; set; }
        public int MinItemLevel { get; set; }
        public int MaxItemLevel { get; set; }
        public int FollowerTypeID { get; set; }
    }
}
