namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUpdateActionButtons
    {
        public fixed ulong ActionButtons[132];
        public byte Reason;
    }
}
