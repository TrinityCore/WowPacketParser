using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLevelSelector, HasIndexInData = false)]
    public class ItemLevelSelectorEntry
    {
        public ushort MinItemLevel { get; set; }
        public ushort ItemLevelSelectorQualitySetID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public ushort Unknown1 { get; set; }
    }
}
