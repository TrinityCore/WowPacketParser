using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemDisplayInfo, HasIndexInData = false)]
    public class ItemDisplayInfoEntry
    {
        public int ModelType1 { get; set; }
        public int ItemVisual { get; set; }
        public int ParticleColorID { get; set; }
        public uint ItemRangedDisplayInfoID { get; set; }
        public uint OverrideSwooshSoundKitID { get; set; }
        public int SheatheTransformMatrixID { get; set; }
        public int StateSpellVisualKitID { get; set; }
        public int SheathedSpellVisualKitID { get; set; }
        public uint UnsheathedSpellVisualKitID { get; set; }
        public int Flags { get; set; }
        [HotfixArray(2)]
        public uint[] ModelResourcesID { get; set; }
        [HotfixArray(2)]
        public int[] ModelMaterialResourcesID { get; set; }
        [HotfixArray(4)]
        public int[] GeosetGroup { get; set; }
        [HotfixArray(4)]
        public int[] AttachmentGeosetGroup { get; set; }
        [HotfixArray(2)]
        public int[] HelmetGeosetVis { get; set; }
    }
}
