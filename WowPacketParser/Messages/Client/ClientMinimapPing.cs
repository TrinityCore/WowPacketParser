using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMinimapPing
    {
        public ulong Sender;
        public Vector2 Position;
    }
}
