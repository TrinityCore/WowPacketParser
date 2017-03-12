namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTotemMoved
    {
        public ulong Totem;
        public byte Slot;
        public byte NewSlot;
    }
}
