using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_bonus_tree")]
    public sealed record ItemBonusTreeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("InventoryTypeSlotMask")]
        public int? InventoryTypeSlotMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
