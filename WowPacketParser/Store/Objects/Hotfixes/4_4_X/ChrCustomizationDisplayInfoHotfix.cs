using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_display_info")]
    public sealed record ChrCustomizationDisplayInfoHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellShapeshiftFormID")]
        public int? SpellShapeshiftFormID;

        [DBFieldName("CreatureDisplayInfoID")]
        public int? CreatureDisplayInfoID;

        [DBFieldName("BarberShopMinCameraDistance")]
        public float? BarberShopMinCameraDistance;

        [DBFieldName("BarberShopHeightOffset")]
        public float? BarberShopHeightOffset;

        [DBFieldName("BarberShopCameraZoomOffset")]
        public float? BarberShopCameraZoomOffset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
