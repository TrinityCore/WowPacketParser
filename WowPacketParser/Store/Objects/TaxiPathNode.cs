using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("taxi_path_node")]
    public sealed class TaxiPathNode
    {
        [DBFieldName("PathID")]
        public uint PathID;

        [DBFieldName("NodeIndex")]
        public uint NodeIndex;

        [DBFieldName("MapID")]
        public uint MapID;

        [DBFieldName("LocX")]
        public float LocX;

        [DBFieldName("LocY")]
        public float LocY;

        [DBFieldName("LocZ")]
        public float LocZ;

        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("Delay")]
        public uint Delay;

        [DBFieldName("ArrivalEventID")]
        public uint ArrivalEventID;

        [DBFieldName("DepartureEventID")]
        public float DepartureEventID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
