using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Artifact, HasIndexInData = false)]
    public class ArtifactEntry
    {
        public string Name { get; set; }
        public uint BarConnectedColor { get; set; }
        public uint BarDisconnectedColor { get; set; }
        public uint TitleColor { get; set; }
        public ushort ClassUiTextureKitID { get; set; }
        public ushort SpecID { get; set; }
        public byte ArtifactCategoryID { get; set; }
        public byte Flags { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public uint UiModelSceneID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public uint SpellVisualKitID { get; set; }
    }
}
