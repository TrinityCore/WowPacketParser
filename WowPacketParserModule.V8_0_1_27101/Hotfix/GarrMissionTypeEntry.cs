using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrMissionType, HasIndexInData = false)]
    public class GarrMissionTypeEntry
    {
        public string Name { get; set; }
        public ushort UiTextureAtlasMemberID { get; set; }
        public ushort UiTextureKitID { get; set; }
    }
}
