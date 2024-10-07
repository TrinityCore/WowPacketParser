using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_aura_restrictions")]
    public sealed record SpellAuraRestrictionsHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public int? DifficultyID;

        [DBFieldName("CasterAuraState")]
        public int? CasterAuraState;

        [DBFieldName("TargetAuraState")]
        public int? TargetAuraState;

        [DBFieldName("ExcludeCasterAuraState")]
        public int? ExcludeCasterAuraState;

        [DBFieldName("ExcludeTargetAuraState")]
        public int? ExcludeTargetAuraState;

        [DBFieldName("CasterAuraSpell")]
        public int? CasterAuraSpell;

        [DBFieldName("TargetAuraSpell")]
        public int? TargetAuraSpell;

        [DBFieldName("ExcludeCasterAuraSpell")]
        public int? ExcludeCasterAuraSpell;

        [DBFieldName("ExcludeTargetAuraSpell")]
        public int? ExcludeTargetAuraSpell;

        [DBFieldName("CasterAuraType")]
        public int? CasterAuraType;

        [DBFieldName("TargetAuraType")]
        public int? TargetAuraType;

        [DBFieldName("ExcludeCasterAuraType")]
        public int? ExcludeCasterAuraType;

        [DBFieldName("ExcludeTargetAuraType")]
        public int? ExcludeTargetAuraType;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
