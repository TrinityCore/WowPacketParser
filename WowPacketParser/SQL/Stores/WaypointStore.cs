using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Stores
{
    public class WaypointStore
    {
        public static string GetCommand(Waypoint wp)
        {
            var builder = new SQLCommandBuilder("waypoints");
            builder.AddColumnValue("entry",wp.NpcEntry);
            builder.AddColumnValue("pointid", wp.Id);
            builder.AddColumnValue("position_x", wp.X);
            builder.AddColumnValue("position_y", wp.Y);
            builder.AddColumnValue("position_z", wp.Z);
            return builder.BuildInsert();
        }
    }
}
