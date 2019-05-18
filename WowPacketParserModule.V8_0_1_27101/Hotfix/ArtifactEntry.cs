using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Artifact)]
    public class ArtifactEntry
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public ushort UiTextureKitID { get; set; }
        public int UiNameColor { get; set; }
        public int UiBarOverlayColor { get; set; }
        public int UiBarBackgroundColor { get; set; }
        public ushort ChrSpecializationID { get; set; }
        public byte Flags { get; set; }
        public byte ArtifactCategoryID { get; set; }
        public uint UiModelSceneID { get; set; }
        public uint SpellVisualKitID { get; set; }
    }
}
