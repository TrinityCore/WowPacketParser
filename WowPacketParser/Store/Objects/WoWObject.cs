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

        public bool ForceTemporarySpawn;

        public virtual bool IsTemporarySpawn()
        {
            return ForceTemporarySpawn;
        }

        public bool IsOnTransport()
        {
            return Movement.TransportGuid != Guid.Empty;
        }

        public int GetDefaultSpawnTime()
        {
            // If map is Continent use a lower respawn time
            // TODO: Rank and if npc is needed for quest kill should change spawntime as well
            return MapIsContinent(Map) ? 120 : 7200;
        }

        public int GetDefaultSpawnMask()
        {
            // 3 is the most common spawnmask outside of continents although it is not correct in all cases
            // TODO: read map/instance db to guess correct spawnmask
            return MapIsContinent(Map) ? 1 : 3;
        }

        private static bool MapIsContinent(uint mapId)
        {
            // TODO: remove hardcoded checks and read map dbc instead
            switch (mapId)
            {
                case 0:     // Eastern Kingdoms
                case 1:     // Kalimdor
                case 530:   // Outland
                case 571:   // Northrend
                case 609:   // Ebon Hold
                case 638:   // Gilneas 1
                case 655:   // Gilneas 2
                case 656:   // Gilneas 3
                case 646:   // Deepholm
                case 648:   // Kezan 1
                case 659:   // Kezan 2
                case 661:   // Kezan 3
                case 732:   // Tol Barad
                case 860:   // The Wandering Isle
                case 861:   // Firelands Dailies
                case 870:   // Pandaria
                case 974:   // Darkmoon Faire
                case 1064:  // Mogu Island Daily Area
                    return true;
                default:
                    return false;
            }
        }

        public virtual void LoadValuesFromUpdateFields() { }
    }
}
