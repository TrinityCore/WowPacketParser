using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_bonus")]
    public sealed class ItemBonus
    {
        [DBFieldName("BonusListID")]
        public uint BonusListID;

        [DBFieldName("Type")]
        public uint Type;

        [DBFieldName("Value", 2)]
        public uint[] Value;

        [DBFieldName("Index")]
        public uint Index;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
