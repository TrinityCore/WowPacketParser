using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.SQL;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class Waypoint
    {
        public ICollection<WaypointData> WaypointData;
        public ICollection<WaypointData> SplineWaypointData;

        public SplineFlag434 SplineFlags;
        public MovementFlag MovementFlags;
    }

    [DBTableName("waypoint_data")]
    public class WaypointData
    {
        [DBFieldName("PointId")]
        public uint PointId;

        //[DBFieldName("Position")]
        public Vector3 Position;

        [DBFieldName("Time")]
        public string Time;
    }
}
