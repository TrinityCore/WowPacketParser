using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct NotifyDestLocSpellCastData
    {
        public ulong Caster;
        public ulong DestTransport;
        public int SpellID;
        public Vector3 SourceLoc;
        public Vector3 DestLoc;
        public float MissileTrajectoryPitch;
        public float MissileTrajectorySpeed;
        public uint TravelTime;
        public byte DestLocSpellCastIndex;
        public byte CastID;
    }
}
