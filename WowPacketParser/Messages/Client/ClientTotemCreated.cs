namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTotemCreated
    {
        public ulong Totem;
        public int SpellID;
        public int Duration;
        public byte Slot;
    }
}
