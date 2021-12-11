using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("override_spell_data")]
    public sealed record OverrideSpellData : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID", 10)]
        public uint?[] SpellID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("PlayerActionbarFileDataID")]
        public uint? PlayerActionbarFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
