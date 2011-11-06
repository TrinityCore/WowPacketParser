using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc.Objects
{
    public sealed class Unit : WoWObject
    {
        public Unit(Guid guid, MovementInfo moves, Dictionary<int, UpdateField> updateFields, List<Aura> auras, uint map)
            : base(guid, ObjectType.Unit, moves, updateFields, map)
        {
            Guid = guid;
            Movement = moves;
            UpdateFields = updateFields;
            Auras = auras;
        }

        public List<Aura> Auras { get; set; }

    }
}
