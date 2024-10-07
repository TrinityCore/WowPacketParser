using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_tier_unlock")]
    public sealed record AzeriteTierUnlockHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemCreationContext")]
        public byte? ItemCreationContext;

        [DBFieldName("Tier")]
        public byte? Tier;

        [DBFieldName("AzeriteLevel")]
        public byte? AzeriteLevel;

        [DBFieldName("AzeriteTierUnlockSetID")]
        public uint? AzeriteTierUnlockSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
