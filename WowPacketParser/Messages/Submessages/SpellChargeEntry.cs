namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellChargeEntry
    {
        public uint Category;
        public uint NextRecoveryTime;
        public byte ConsumedCharges;
    }
}
