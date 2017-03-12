using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlayOrphanSpellVisual
    {
        public ulong Target;
        public Vector3 SourceLocation;
        public int SpellVisualID;
        public bool SpeedAsTime;
        public float TravelSpeed;
        public Vector3 SourceOrientation;
        public Vector3 TargetLocation;
    }
}
