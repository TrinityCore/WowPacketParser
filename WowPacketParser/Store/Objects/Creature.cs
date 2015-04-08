using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature")]
    public class Creature
    {
        [DBFieldName("Type")]
        public CreatureType Type;

        [DBFieldName("ItemID", 3)]
        public uint[] ItemID;

        [DBFieldName("Mount")]
        public uint Mount;

        [DBFieldName("DisplayID", 4)]
        public uint[] DisplayID;

        [DBFieldName("DisplayIDProbability", 4)]
        public float[] DisplayIDProbability;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("FemaleName")]
        public string FemaleName;

        [DBFieldName("SubName")]
        public string SubName;

        [DBFieldName("FemaleSubName")]
        public string FemaleSubName;

        [DBFieldName("Rank")]
        public uint Rank;

        [DBFieldName("Rank")]
        public uint InhabitType;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
