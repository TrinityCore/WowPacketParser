using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_model")]
    public sealed record ChrModelHotfix440: IDataModel
    {
        [DBFieldName("FaceCustomizationOffsetX")]
        public float? FaceCustomizationOffsetX;

        [DBFieldName("FaceCustomizationOffsetY")]
        public float? FaceCustomizationOffsetY;

        [DBFieldName("FaceCustomizationOffsetZ")]
        public float? FaceCustomizationOffsetZ;

        [DBFieldName("CustomizeOffsetX")]
        public float? CustomizeOffsetX;

        [DBFieldName("CustomizeOffsetY")]
        public float? CustomizeOffsetY;

        [DBFieldName("CustomizeOffsetZ")]
        public float? CustomizeOffsetZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Sex")]
        public sbyte? Sex;

        [DBFieldName("DisplayID")]
        public int? DisplayID;

        [DBFieldName("CharComponentTextureLayoutID")]
        public int? CharComponentTextureLayoutID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SkeletonFileDataID")]
        public int? SkeletonFileDataID;

        [DBFieldName("ModelFallbackChrModelID")]
        public int? ModelFallbackChrModelID;

        [DBFieldName("TextureFallbackChrModelID")]
        public int? TextureFallbackChrModelID;

        [DBFieldName("HelmVisFallbackChrModelID")]
        public int? HelmVisFallbackChrModelID;

        [DBFieldName("CustomizeScale")]
        public float? CustomizeScale;

        [DBFieldName("CustomizeFacing")]
        public float? CustomizeFacing;

        [DBFieldName("CameraDistanceOffset")]
        public float? CameraDistanceOffset;

        [DBFieldName("BarberShopCameraOffsetScale")]
        public float? BarberShopCameraOffsetScale;

        [DBFieldName("BarberShopCameraRotationFacing")]
        public float? BarberShopCameraRotationFacing;

        [DBFieldName("BarberShopCameraRotationOffset")]
        public float? BarberShopCameraRotationOffset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
