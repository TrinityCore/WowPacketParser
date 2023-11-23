using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_model")]
    public sealed record ChrModelHotfix1000: IDataModel
    {
        [DBFieldName("FaceCustomizationOffset", 3)]
        public float?[] FaceCustomizationOffset;

        [DBFieldName("CustomizeOffset", 3)]
        public float?[] CustomizeOffset;

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

        [DBFieldName("BarberShopCameraHeightOffsetScale")]
        public float? BarberShopCameraHeightOffsetScale;

        [DBFieldName("BarberShopCameraRotationOffset")]
        public float? BarberShopCameraRotationOffset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_model")]
    public sealed record ChrModelHotfix340: IDataModel
    {
        [DBFieldName("FaceCustomizationOffset", 3)]
        public float?[] FaceCustomizationOffset;

        [DBFieldName("CustomizeOffset", 3)]
        public float?[] CustomizeOffset;

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

        [DBFieldName("BarberShopCameraHeightOffsetScale")]
        public float? BarberShopCameraHeightOffsetScale;

        [DBFieldName("BarberShopCameraRotationOffset")]
        public float? BarberShopCameraRotationOffset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("chr_model")]
    public sealed record ChrModelHotfix341: IDataModel
    {
        [DBFieldName("FaceCustomizationOffset", 3)]
        public float?[] FaceCustomizationOffset;

        [DBFieldName("CustomizeOffset", 3)]
        public float?[] CustomizeOffset;

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

        [DBFieldName("BarberShopCameraHeightOffsetScale")]
        public float? BarberShopCameraHeightOffsetScale;

        [DBFieldName("BarberShopCameraRotationOffset")]
        public float? BarberShopCameraRotationOffset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
