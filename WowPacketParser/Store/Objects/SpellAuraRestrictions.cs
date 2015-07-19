using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_aura_restrictions")]
    public sealed class SpellAuraRestrictions
    {
        [DBFieldName("CasterAuraState")]
        public uint CasterAuraState;

        [DBFieldName("TargetAuraState")]
        public uint TargetAuraState;

        [DBFieldName("ExcludeCasterAuraState")]
        public uint ExcludeCasterAuraState;

        [DBFieldName("ExcludeTargetAuraState")]
        public uint ExcludeTargetAuraState;

        [DBFieldName("CasterAuraSpell")]
        public uint CasterAuraSpell;

        [DBFieldName("TargetAuraSpell")]
        public uint TargetAuraSpell;

        [DBFieldName("ExcludeCasterAuraSpell")]
        public uint ExcludeCasterAuraSpell;

        [DBFieldName("ExcludeTargetAuraSpell")]
        public uint ExcludeTargetAuraSpell;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
