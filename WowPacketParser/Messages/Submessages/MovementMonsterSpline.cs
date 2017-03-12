using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct MovementMonsterSpline
    {
        public uint ID;
        public Vector3 Destination;
        public bool CrzTeleport;
        public MovementSpline Move;
    }
}
