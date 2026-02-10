using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_modified_appearance")]
    public sealed record ItemModifiedAppearanceHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public uint? ItemID;

        [DBFieldName("ItemAppearanceModifierID")]
        public int? ItemAppearanceModifierID;

        [DBFieldName("ItemAppearanceID")]
        public int? ItemAppearanceID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("TransmogSourceTypeEnum")]
        public byte? TransmogSourceTypeEnum;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
