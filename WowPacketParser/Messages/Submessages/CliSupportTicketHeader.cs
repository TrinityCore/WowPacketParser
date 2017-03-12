using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliSupportTicketHeader
    {
        public int MapID;
        public Vector3 Position;
        public float Facing;
    }
}
