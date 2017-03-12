using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlaySpellVisual
    {
        public ulong Source;
        public ulong Target;
        public ushort MissReason;
        public int SpellVisualID;
        public bool SpeedAsTime;
        public ushort ReflectStatus;
        public float TravelSpeed;
        public Vector3 TargetPosition;
    }
}
