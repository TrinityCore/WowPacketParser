using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactAppearanceSet)]
    public class ArtifactAppearanceSetEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        public byte DisplayIndex { get; set; }
        public ushort UiCameraID { get; set; }
        public ushort AltHandUICameraID { get; set; }
        public sbyte ForgeAttachmentOverride { get; set; }
        public byte Flags { get; set; }
        public byte ArtifactID { get; set; }
    }
}
