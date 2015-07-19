using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_rune_cost")]
    public sealed class SpellRuneCost
    {
        [DBFieldName("Blood")]
        public uint Blood;

        [DBFieldName("Unholy")]
        public uint Unholy;

        [DBFieldName("Frost")]
        public uint Frost;

        [DBFieldName("Chromatic")]
        public uint Chromatic;

        [DBFieldName("RunicPower")]
        public uint RunicPower;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
