using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.GarrClassSpec, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class GarrClassSpecEntry
    {
        public string ClassSpec { get; set; }
        public string ClassSpecMale { get; set; }
        public string ClassSpecFemale { get; set; }
        public ushort UiTextureAtlasMemberID { get; set; }
        public ushort GarrFollItemSetID { get; set; }
        public byte FollowerClassLimit { get; set; }
        public byte Flags { get; set; }
    }
}
