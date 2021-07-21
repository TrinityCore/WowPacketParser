using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrModel)]
    public class ChrModelEntry
    {
        [HotfixArray(3)]
        public float[] FaceCustomizationOffset { get; set; }
        [HotfixArray(3)]
        public float[] CustomizeOffset { get; set; }
        public uint ID { get; set; }
        public int Sex { get; set; }
        public int DisplayID { get; set; }
        public int CharComponentTextureLayoutID { get; set; }
        public int Flags { get; set; }
        public int SkeletonFileDataID { get; set; }
        public int ModelFallbackChrModelID { get; set; }
        public int TextureFallbackChrModelID { get; set; }
        public int HelmVisFallbackChrModelID { get; set; }
        public float CustomizeScale { get; set; }
        public float CustomizeFacing { get; set; }
        public float CameraDistanceOffset { get; set; }
        public float BarberShopCameraOffsetScale { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_1_0_39185, false)]
        public float Field91038312015 { get; set; }
        public float BarberShopCameraRotationOffset { get; set; }
    }
}
