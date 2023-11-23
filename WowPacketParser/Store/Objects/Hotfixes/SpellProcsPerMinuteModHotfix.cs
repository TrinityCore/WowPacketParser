using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_procs_per_minute_mod")]
    public sealed record SpellProcsPerMinuteModHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("Param")]
        public int? Param;

        [DBFieldName("Coeff")]
        public float? Coeff;

        [DBFieldName("SpellProcsPerMinuteID")]
        public uint? SpellProcsPerMinuteID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_procs_per_minute_mod")]
    public sealed record SpellProcsPerMinuteModHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("Param")]
        public short? Param;

        [DBFieldName("Coeff")]
        public float? Coeff;

        [DBFieldName("SpellProcsPerMinuteID")]
        public int? SpellProcsPerMinuteID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
