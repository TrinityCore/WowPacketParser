using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class WoWObject
    {
        public ObjectType Type;

        public MovementInfo Movement;

        public uint Map;

        public int Area;

        public Dictionary<int, UpdateField> UpdateFields; // SMSG_UPDATE_OBJECT - CreateObject

        public ICollection<Dictionary<int, UpdateField>> ChangedUpdateFieldsList; // SMSG_UPDATE_OBJECT - Values

        public uint PhaseMask;

        public virtual bool IsTemporarySpawn()
        {
            return false;
        }

        public int GetDefaultSpawnTime()
        {
            // If map is Eastern Kingdoms, Kalimdor, Outland, Northrend or Ebon Hold use a lower respawn time
            // TODO: Rank and if npc is needed for quest kill should change spawntime as well
            return (Map == 0 || Map == 1 || Map == 530 || Map == 571 || Map == 609) ? 120 : 7200;
        }
    }
}
