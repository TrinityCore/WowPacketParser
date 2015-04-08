using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("override_spell_data")]
    public sealed class OverrideSpellData
    {
        [DBFieldName("SpellID", 10)]
        public uint[] SpellID;

        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("PlayerActionbarFileDataID")]
        public uint PlayerActionbarFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
