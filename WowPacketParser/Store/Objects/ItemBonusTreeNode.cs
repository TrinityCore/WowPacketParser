using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_bonus_tree_node")]
    public sealed class ItemBonusTreeNode
    {
        [DBFieldName("BonusTreeID")]
        public uint BonusTreeID;

        [DBFieldName("BonusTreeModID")]
        public uint BonusTreeModID;

        [DBFieldName("SubTreeID")]
        public uint SubTreeID;

        [DBFieldName("BonusListID")]
        public uint BonusListID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
