using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_scaling")]
    public sealed record SpellScalingHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("MinScalingLevel")]
        public uint? MinScalingLevel;

        [DBFieldName("MaxScalingLevel")]
        public uint? MaxScalingLevel;

        [DBFieldName("ScalesFromItemLevel")]
        public short? ScalesFromItemLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
