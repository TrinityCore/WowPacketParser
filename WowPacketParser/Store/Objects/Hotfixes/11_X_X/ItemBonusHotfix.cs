using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_bonus")]
    public sealed record ItemBonusHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Value", 4)]
        public int?[] Value;

        [DBFieldName("ParentItemBonusListID")]
        public ushort? ParentItemBonusListID;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
