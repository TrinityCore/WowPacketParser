using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_effect_group_size")]
    public sealed class SpellEffectGroupSize
    {
        [DBFieldName("SpellEffectID")]
        public uint SpellEffectID;

        [DBFieldName("Size")]
        public float Size;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
