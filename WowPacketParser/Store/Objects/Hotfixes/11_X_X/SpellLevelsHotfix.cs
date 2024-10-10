using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_levels")]
    public sealed record SpellLevelsHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("MaxLevel")]
        public short? MaxLevel;

        [DBFieldName("MaxPassiveAuraLevel")]
        public byte? MaxPassiveAuraLevel;

        [DBFieldName("BaseLevel")]
        public int? BaseLevel;

        [DBFieldName("SpellLevel")]
        public int? SpellLevel;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
