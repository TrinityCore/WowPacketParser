using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Misc;
using PacketParser.Enums.Version;

namespace PacketParser.DataStructures
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

        public Guid? GetGuid()
        {
            UpdateField low;
            UpdateField high;
            if (UpdateFields.TryGetValue((int)Enums.Version.UpdateFields.GetUpdateFieldOffset(ObjectField.OBJECT_FIELD_GUID), out low))
                if (UpdateFields.TryGetValue((int)Enums.Version.UpdateFields.GetUpdateFieldOffset(ObjectField.OBJECT_FIELD_GUID + 1), out high))
                {
                    ulong lowg = low.UInt32Value;
                    ulong highg = high.UInt32Value;
                    return new Guid(lowg | (highg<<32));
                }
            return null;
        }
    }
}
