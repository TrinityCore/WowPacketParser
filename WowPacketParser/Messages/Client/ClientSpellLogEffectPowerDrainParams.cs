namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellLogEffectPowerDrainParams
    {
        public ulong Victim;
        public uint Points;
        public uint PowerType;
        public float Amplitude;
    }
}
