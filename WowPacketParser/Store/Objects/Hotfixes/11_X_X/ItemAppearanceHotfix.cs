using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_appearance")]
    public sealed record ItemAppearanceHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DisplayType")]
        public sbyte? DisplayType;

        [DBFieldName("ItemDisplayInfoID")]
        public int? ItemDisplayInfoID;

        [DBFieldName("DefaultIconFileDataID")]
        public int? DefaultIconFileDataID;

        [DBFieldName("UiOrder")]
        public int? UiOrder;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
