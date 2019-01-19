using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapXMapArt, HasIndexInData = false)]
    public class UiMapXMapArtEntry
    {
        public int PhaseID { get; set; }
        public int UiMapArtID { get; set; }
        public int UiMapID { get; set; }
    }
}
