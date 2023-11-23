using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("override_spell_data")]
    public sealed record OverrideSpellDataHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Spells", 10)]
        public int?[] Spells;

        [DBFieldName("PlayerActionBarFileDataID")]
        public int? PlayerActionBarFileDataID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("override_spell_data")]
    public sealed record OverrideSpellDataHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Spells", 10)]
        public int?[] Spells;

        [DBFieldName("PlayerActionbarFileDataID")]
        public int? PlayerActionbarFileDataID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
