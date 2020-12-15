using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactPowerPicker, HasIndexInData = false)]
    public class ArtifactPowerPickerEntry
    {
        public uint PlayerConditionID { get; set; }
    }
}
