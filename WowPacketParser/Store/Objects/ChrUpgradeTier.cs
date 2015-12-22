using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("chr_upgrade_tier")]
    public sealed class ChrUpgradeTier : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TierIndex")]
        public uint? TierIndex;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("TalentTier")]
        public uint? TalentTier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
