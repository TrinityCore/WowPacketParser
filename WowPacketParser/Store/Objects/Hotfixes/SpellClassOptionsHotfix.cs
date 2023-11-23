using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_class_options")]
    public sealed record SpellClassOptionsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("ModalNextSpell")]
        public uint? ModalNextSpell;

        [DBFieldName("SpellClassSet")]
        public byte? SpellClassSet;

        [DBFieldName("SpellClassMask", 4)]
        public int?[] SpellClassMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_class_options")]
    public sealed record SpellClassOptionsHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("ModalNextSpell")]
        public uint? ModalNextSpell;

        [DBFieldName("SpellClassSet")]
        public byte? SpellClassSet;

        [DBFieldName("SpellClassMask", 4)]
        public int?[] SpellClassMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
