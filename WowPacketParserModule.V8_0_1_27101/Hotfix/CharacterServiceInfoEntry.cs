using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharacterServiceInfo, HasIndexInData = false)]
    public class CharacterServiceInfoEntry
    {
        public string FlowTitle { get; set; }
        public string PopupTitle { get; set; }
        public string PopupDescription { get; set; }
        public int BoostType { get; set; }
        public int IconFileDataID { get; set; }
        public int Priority { get; set; }
        public uint Flags { get; set; }
        public int ProfessionLevel { get; set; }
        public int BoostLevel { get; set; }
        public int Expansion { get; set; }
        public int PopupUITextureKitID { get; set; }
    }
}
