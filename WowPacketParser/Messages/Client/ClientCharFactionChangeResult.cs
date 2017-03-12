using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCharFactionChangeResult
    {
        public byte Result;
        public ulong Guid;
        public CharFactionChangeDisplayInfo? Display; // Optional
    }
}
