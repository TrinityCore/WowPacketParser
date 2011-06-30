using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public sealed class WowObject
    {
        public Guid Guid;
        public Vector3 Position;
        public ObjectType Type;
        public MovementInfo Movement;

        public int _unitflag;
        public int _npcflag;
        public int _dynamicflag;

        public int _Faction, _Level, _BaseAttackTime;

        public WowObject(Guid guid, ObjectType type, MovementInfo moves)
        {
            Guid = guid;
            Type = type;
            Movement = moves;
        }
    }
}
