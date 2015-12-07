using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_difficulty")]
    public sealed class CreatureDifficulty : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreatureID")]
        public uint? CreatureID;

        [DBFieldName("FactionID")]
        public uint? FactionID;

        [DBFieldName("Expansion")]
        public int? Expansion;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel")]
        public int? MaxLevel;

        [DBFieldName("Flags", 5)]
        public uint?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
