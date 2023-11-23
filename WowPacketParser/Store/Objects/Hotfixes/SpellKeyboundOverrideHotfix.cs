using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_keybound_override")]
    public sealed record SpellKeyboundOverrideHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Function")]
        public string Function;

        [DBFieldName("Type")]
        public sbyte? Type;

        [DBFieldName("Data")]
        public int? Data;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_keybound_override")]
    public sealed record SpellKeyboundOverrideHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Function")]
        public string Function;

        [DBFieldName("Type")]
        public sbyte? Type;

        [DBFieldName("Data")]
        public int? Data;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
