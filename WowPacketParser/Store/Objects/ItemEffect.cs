using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_effect")]
    public sealed class ItemEffect
    {
        [DBFieldName("ItemID")]
        public uint ItemID;

        [DBFieldName("OrderIndex")]
        public uint OrderIndex;

        [DBFieldName("SpellID")]
        public uint SpellID;

        [DBFieldName("Trigger")]
        public uint Trigger;

        [DBFieldName("Charges")]
        public uint Charges;

        [DBFieldName("Cooldown")]
        public int Cooldown;

        [DBFieldName("Category")]
        public uint Category;

        [DBFieldName("CategoryCooldown")]
        public int CategoryCooldown;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
