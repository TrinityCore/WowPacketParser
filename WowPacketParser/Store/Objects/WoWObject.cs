using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class WoWObject
    {
        public Vector3 GetPosition()
        {
            return Movement.Position;
        }

        public ObjectType Type;

        public MovementInfo Movement;

        public uint Map;

        public int Area;

        public Dictionary<int, UpdateField> UpdateFields; // SMSG_UPDATE_OBJECT - CreateObject

        public ICollection<Dictionary<int, UpdateField>> ChangedUpdateFieldsList; // SMSG_UPDATE_OBJECT - Values

        public uint PhaseMask;

        public bool IsTemporarySpawn()
        {
            // Should this return false here and be overriden in Unit class?

            // Can gameobjects be "temporary spawns"?
            if (!(this is Unit))
                return false;

            // If our unit got any of the folowing updated fields set,
            // it's probably a temporary spawn
            UpdateField uf;
            if (UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_SUMMONEDBY), out uf) ||
                UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_CREATED_BY_SPELL), out uf) ||
                UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_CREATEDBY), out uf))
                return uf.Int32Value != 0;

            return false;
        }
    }
}
