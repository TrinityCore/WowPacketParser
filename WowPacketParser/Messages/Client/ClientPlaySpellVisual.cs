using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
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
