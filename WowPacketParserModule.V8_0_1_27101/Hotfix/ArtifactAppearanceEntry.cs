using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactAppearance)]
    public class ArtifactAppearanceEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public ushort ArtifactAppearanceSetID { get; set; }
        public byte DisplayIndex { get; set; }
        public uint UnlockPlayerConditionID { get; set; }
        public byte ItemAppearanceModifierID { get; set; }
        public int UiSwatchColor { get; set; }
        public float UiModelSaturation { get; set; }
        public float UiModelOpacity { get; set; }
        public byte OverrideShapeshiftFormID { get; set; }
        public uint OverrideShapeshiftDisplayID { get; set; }
        public uint UiItemAppearanceID { get; set; }
        public uint UiAltItemAppearanceID { get; set; }
        public byte Flags { get; set; }
        public ushort UiCameraID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public uint UsablePlayerConditionID { get; set; }
    }
}
