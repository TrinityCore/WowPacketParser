namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientItemEnchantTimeUpdate
    {
        public ulong OwnerGuid;
        public ulong ItemGuid;
        public uint DurationLeft;
        public int Slot;
    }
}
