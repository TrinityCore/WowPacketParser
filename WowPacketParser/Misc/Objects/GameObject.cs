using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc.Objects
{
    public sealed class GameObject : WoWObject
    {
        public GameObject(Guid guid, MovementInfo moves, Dictionary<int, UpdateField> updateFields, uint map)
            : base(guid, ObjectType.GameObject, moves, updateFields, map)
        {
            Guid = guid;
            Movement = moves;
            UpdateFields = updateFields;
        }

        // MovementInfo contains position
        // Do we need to store anything else here?
    }
}
