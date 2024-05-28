using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_levels")]
    public sealed record SpellLevelsHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("BaseLevel")]
        public short? BaseLevel;

        [DBFieldName("MaxLevel")]
        public short? MaxLevel;

        [DBFieldName("SpellLevel")]
        public short? SpellLevel;

        [DBFieldName("MaxPassiveAuraLevel")]
        public byte? MaxPassiveAuraLevel;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
