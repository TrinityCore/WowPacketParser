using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc.Objects
{
    public sealed class Unit // inherit from WoWObject ?
    {
        public Unit(Guid guid, MovementInfo moves, Dictionary<int, UpdateField> updateFields, List<Aura> auras)
        {
            Guid = guid;
            Movement = moves;
            UpdateFields = updateFields;
            Auras = auras;
        }

        public Guid Guid { get; set; }

        public MovementInfo Movement { get; set; }

        public Dictionary<int, UpdateField> UpdateFields { get; set; }

        public List<Aura> Auras { get; set; }

    }
}
