using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactAppearanceSet)]
    public class ArtifactAppearanceSetEntry
    {
        public string Name { get; set; }
        public string Name2 { get; set; }
        public ushort UiCameraID { get; set; }
        public ushort AltHandUICameraID { get; set; }
        public byte ArtifactID { get; set; }
        public byte DisplayIndex { get; set; }
        public byte AttachmentPoint { get; set; }
        public byte Flags { get; set; }
        public uint ID { get; set; }
    }
}
