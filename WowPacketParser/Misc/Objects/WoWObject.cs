using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc.Objects
{
    public class WoWObject
    {
        public WoWObject(Guid guid, ObjectType type, MovementInfo moves, Dictionary<int, UpdateField> updateFields, uint map)
        {
            Guid = guid;
            Type = type;
            Movement = moves;
            UpdateFields = updateFields;
        }

        public Guid Guid;

        public Vector3 GetPosition()
        {
            return Movement.Position;
        }

        public ObjectType Type;

        public MovementInfo Movement;

        public uint Map;

        public Dictionary<int, UpdateField> UpdateFields;
    }
}
