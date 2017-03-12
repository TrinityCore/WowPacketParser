using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
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
