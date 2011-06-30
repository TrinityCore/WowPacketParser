using WowPacketParser.Misc;

namespace WowPacketParser.SQL.SQLStore.Stores
{
    public sealed class GameObjectSpawnStore
    {
        public string GetCommand(uint entry, int map, int phaseMask, Vector3 pos, float orient)
        {
            var builder = new CommandBuilder("gameobject");

            builder.AddColumnValue("id", entry);
            builder.AddColumnValue("map", map);
            builder.AddColumnValue("spawnMask", 1);
            builder.AddColumnValue("phaseMask", phaseMask);
            builder.AddColumnValue("position_x", pos.X);
            builder.AddColumnValue("position_y", pos.Y);
            builder.AddColumnValue("position_z", pos.Z);
            builder.AddColumnValue("orientation", orient);
            builder.AddColumnValue("rotation0", 0);
            builder.AddColumnValue("rotation1", 0);
            builder.AddColumnValue("rotation2", 0);
            builder.AddColumnValue("rotation3", 0);
            builder.AddColumnValue("spawntimesecs", 120);
            builder.AddColumnValue("animprogress", 0);
            builder.AddColumnValue("state", 1);

            return builder.BuildInsert();
        }
    }
}
