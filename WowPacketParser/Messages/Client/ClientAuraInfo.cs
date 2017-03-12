namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuraInfo
    {
        public byte Slot;
        public ClientAuraDataInfo? AuraData; // Optional
    }
}
