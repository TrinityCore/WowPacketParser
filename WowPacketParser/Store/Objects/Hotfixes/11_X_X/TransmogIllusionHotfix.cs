using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_illusion")]
    public sealed record TransmogIllusionHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UnlockConditionID")]
        public int? UnlockConditionID;

        [DBFieldName("TransmogCost")]
        public int? TransmogCost;

        [DBFieldName("SpellItemEnchantmentID")]
        public int? SpellItemEnchantmentID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
