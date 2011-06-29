using WowPacketParser.Misc;

namespace WowPacketParser.Storing.Stores
{
    public sealed class CreatureSpawnStore
    {
        public string GetCommand(uint entry, int map, int phaseMask, Vector3 position, float orient)
        {
            var builder = new CommandBuilder("creature");

            builder.AddColumnValue("id", entry);
            builder.AddColumnValue("map", map);
            builder.AddColumnValue("spawnMask", 1);
            builder.AddColumnValue("phaseMask", "0x" + phaseMask.ToString("X8"));
            builder.AddColumnValue("modelid", 0);
            builder.AddColumnValue("equipment_id", 0);
            builder.AddColumnValue("position_x", position.X);
            builder.AddColumnValue("position_y", position.Y);
            builder.AddColumnValue("position_z", position.Z);
            builder.AddColumnValue("orientation", orient);
            builder.AddColumnValue("spawntimesecs", 120);
            builder.AddColumnValue("spawndist", 0.0f);
            builder.AddColumnValue("currentwaypoint", 0);
            builder.AddColumnValue("curhealth", 1);
            builder.AddColumnValue("curmana", 0);
            builder.AddColumnValue("DeathState", 0);
            builder.AddColumnValue("MovementType", 0);

            return builder.BuildInsert(true);
        }
    }
}
