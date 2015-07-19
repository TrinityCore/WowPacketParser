using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_class_options")]
    public sealed class SpellClassOptions
    {
        [DBFieldName("ModalNextSpell")]
        public uint ModalNextSpell;

        [DBFieldName("SpellClassMask", 4)]
        public uint[] SpellClassMask;

        [DBFieldName("SpellClassSet")]
        public uint SpellClassSet;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
