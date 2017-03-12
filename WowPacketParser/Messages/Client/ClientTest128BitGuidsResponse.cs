using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTest128BitGuidsResponse
    {
        public int128 Guid;
    }
}
