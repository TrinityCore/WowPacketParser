using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_empower_stage")]
    public sealed record SpellEmpowerStageHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Stage")]
        public int? Stage;

        [DBFieldName("DurationMs")]
        public int? DurationMs;

        [DBFieldName("SpellEmpowerID")]
        public uint? SpellEmpowerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
