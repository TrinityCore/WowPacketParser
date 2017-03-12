using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliSupportTicketHeader
    {
        public int MapID;
        public Vector3 Position;
        public float Facing;
    }
}
