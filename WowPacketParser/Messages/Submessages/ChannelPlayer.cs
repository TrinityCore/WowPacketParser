namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ChannelPlayer
    {
        public ulong Guid;
        public uint VirtualRealmAddress;
        public byte Flags;
    }
}
