using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL.SQLStore.Stores
{
    public sealed class StartPositionStore
    {
        public string GetCommand(Race race, Class clss, int map, int zone, Vector3 pos)
        {
            var builder = new CommandBuilder("playercreateinfo");

            builder.AddColumnValue("race", (int)race);
            builder.AddColumnValue("class", (int)clss);
            builder.AddColumnValue("map", map);
            builder.AddColumnValue("zone", zone);
            builder.AddColumnValue("position_x", pos.X);
            builder.AddColumnValue("position_y", pos.Y);
            builder.AddColumnValue("position_z", pos.Z);

            return builder.BuildInsert();
        }
    }
}
