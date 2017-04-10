using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactAppearance, ClientVersionBuild.V7_2_0_23826)]
    public class ArtifactAppearanceEntry
    {
        public string Name { get; set; }
        public uint SwatchColor { get; set; }
        public float ModelDesaturation { get; set; }
        public float ModelAlpha { get; set; }
        public uint ShapeshiftDisplayID { get; set; }
        public ushort ArtifactAppearanceSetID { get; set; }
        public ushort Unknown { get; set; }
        public byte DisplayIndex { get; set; }
        public byte AppearanceModID { get; set; }
        public byte Flags { get; set; }
        public byte ModifiesShapeshiftFormDisplay { get; set; }
        public uint ID { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint ItemAppearanceID { get; set; }
        public uint AltItemAppearanceID { get; set; }
    }
}
