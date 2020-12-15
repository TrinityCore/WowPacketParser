using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
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
        public uint UsablePlayerConditionID { get; set; }
    }
}
