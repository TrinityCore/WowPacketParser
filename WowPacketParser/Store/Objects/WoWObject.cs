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
            switch (Map)
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
                case 861:   // Firelands Dailies
                    return 120;
                default:
                    return 7200;
            }
        }

        public virtual void LoadValuesFromUpdateFields() { }
    }
}
