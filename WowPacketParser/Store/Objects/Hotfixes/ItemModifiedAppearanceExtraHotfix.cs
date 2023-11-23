using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_modified_appearance_extra")]
    public sealed record ItemModifiedAppearanceExtraHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("UnequippedIconFileDataID")]
        public int? UnequippedIconFileDataID;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("DisplayWeaponSubclassID")]
        public sbyte? DisplayWeaponSubclassID;

        [DBFieldName("DisplayInventoryType")]
        public sbyte? DisplayInventoryType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_modified_appearance_extra")]
    public sealed record ItemModifiedAppearanceExtraHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("UnequippedIconFileDataID")]
        public int? UnequippedIconFileDataID;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("DisplayWeaponSubclassID")]
        public sbyte? DisplayWeaponSubclassID;

        [DBFieldName("DisplayInventoryType")]
        public sbyte? DisplayInventoryType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
