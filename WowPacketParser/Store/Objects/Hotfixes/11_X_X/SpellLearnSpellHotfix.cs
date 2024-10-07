using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_learn_spell")]
    public sealed record SpellLearnSpellHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("LearnSpellID")]
        public int? LearnSpellID;

        [DBFieldName("OverridesSpellID")]
        public int? OverridesSpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
