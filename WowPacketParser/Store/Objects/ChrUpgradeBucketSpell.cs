using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("chr_upgrade_bucket_spell")]
    public sealed class ChrUpgradeBucketSpell : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BucketID")]
        public uint? BucketID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
