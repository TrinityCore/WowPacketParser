using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_x_bonus_tree")]
    public sealed record ItemXBonusTreeHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemBonusTreeID")]
        public ushort? ItemBonusTreeID;

        [DBFieldName("ItemID")]
        public uint? ItemID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
