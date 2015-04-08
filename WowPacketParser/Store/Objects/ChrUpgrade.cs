using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("chr_upgrade_tier")]
    public sealed class ChrUpgradeTier
    {
        [DBFieldName("TierIndex")]
        public uint TierIndex;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("TalentTier")]
        public uint TalentTier;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("chr_upgrade_bucket")]
    public sealed class ChrUpgradeBucket
    {
        [DBFieldName("TierID")]
        public uint TierID;

        [DBFieldName("SpecializationID")]
        public uint SpecializationID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("chr_upgrade_bucket_spell")]
    public sealed class ChrUpgradeBucketSpell
    {
        [DBFieldName("BucketID")]
        public uint BucketID;

        [DBFieldName("SpellID")]
        public uint SpellID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
