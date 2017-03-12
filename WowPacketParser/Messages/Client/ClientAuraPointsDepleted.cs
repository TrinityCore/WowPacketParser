namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuraPointsDepleted
    {
        public ulong Unit;
        public byte Slot;
        public byte EffectIndex;
    }
}
