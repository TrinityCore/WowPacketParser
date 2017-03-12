namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellLogMissEntry
    {
        public ulong Victim;
        public byte MissReason;
        public ClientSpellLogMissDebug? Debug; // Optional
    }
}
