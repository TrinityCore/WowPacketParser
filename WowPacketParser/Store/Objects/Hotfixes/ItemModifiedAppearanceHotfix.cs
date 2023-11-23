using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_modified_appearance")]
    public sealed record ItemModifiedAppearanceHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("ItemAppearanceModifierID")]
        public int? ItemAppearanceModifierID;

        [DBFieldName("ItemAppearanceID")]
        public int? ItemAppearanceID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("TransmogSourceTypeEnum")]
        public byte? TransmogSourceTypeEnum;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_modified_appearance")]
    public sealed record ItemModifiedAppearanceHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("ItemAppearanceModifierID")]
        public int? ItemAppearanceModifierID;

        [DBFieldName("ItemAppearanceID")]
        public int? ItemAppearanceID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("TransmogSourceTypeEnum")]
        public sbyte? TransmogSourceTypeEnum;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
