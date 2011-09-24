using WowPacketParser.Misc;
using WowPacketParser.SQL.Builder;


namespace WowPacketParser.SQL.Stores
{
    public sealed class CreatureSpawnStore
    {
        public string GetCommand(uint entry, int map, int phaseMask, Vector3 position, float orient)
        {
            var builder = new SQLInsert();
            builder.Table = "creature";

            // If map is Eastern Kingdoms, Kalimdor, Outland, Northrend or Ebon Hold use a lower respawn time
            // TODO: Rank and if npc is needed for quest kill should change spawntime aswell
            var spawnTimeSecs = (map == 0 || map == 1 || map == 530 || map == 571 || map == 609) ? 120 : 7200;
            var movementType = 0;
            var spawnDist = (movementType == 1) ? 5 : 0;

            builder.AddValue("guid", "@GUID+X");
            builder.AddValue("id", entry);
            builder.AddValue("map", map);
            builder.AddValue("spawnMask", 1);
            builder.AddValue("phaseMask", "0x" + phaseMask.ToString("X8"));
            builder.AddValue("modelid", 0);
            builder.AddValue("equipment_id", 0);
            builder.AddValue("position_x", position.X);
            builder.AddValue("position_y", position.Y);
            builder.AddValue("position_z", position.Z);
            builder.AddValue("orientation", orient);
            builder.AddValue("spawntimesecs", spawnTimeSecs);
            builder.AddValue("spawndist", spawnDist);
            builder.AddValue("currentwaypoint", 0);
            builder.AddValue("curhealth", 1);
            builder.AddValue("curmana", 0);
            builder.AddValue("MovementType", movementType);
            builder.AddValue("npcflag", 0); // TODO: Check if they match updated creature_template value ?
            builder.AddValue("unit_flags", 0);
            builder.AddValue("dynamicflags", 0);

            builder.AddWhere("guid", "@GUID+X");
            builder.AddWhere("id", entry);

            return builder.Build();
        }
    }
}
