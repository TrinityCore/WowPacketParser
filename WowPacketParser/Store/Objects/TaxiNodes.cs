using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("taxi_nodes")]
    public sealed class TaxiNodes
    {
        [DBFieldName("MapID")]
        public uint MapID;

        [DBFieldName("PosX")]
        public float PosX;

        [DBFieldName("PosY")]
        public float PosY;

        [DBFieldName("PosZ")]
        public float PosZ;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("MountCreatureID", 2)]
        public uint[] MountCreatureID;

        [DBFieldName("ConditionID")]
        public uint ConditionID;

        [DBFieldName("LearnableIndex")]
        public uint LearnableIndex;

        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("MapOffsetX")]
        public float MapOffsetX;

        [DBFieldName("MapOffsetY")]
        public float MapOffsetY;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
