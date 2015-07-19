using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_learn_spell")]
    public sealed class SpellLearnSpell
    {
        [DBFieldName("LearnSpellID")]
        public uint LearnSpellID;

        [DBFieldName("SpellID")]
        public uint SpellID;

        [DBFieldName("OverridesSpellID")]
        public uint OverridesSpellID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
