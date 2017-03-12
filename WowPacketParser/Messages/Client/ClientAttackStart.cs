namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAttackStart
    {
        public ulong Attacker;
        public ulong Victim;
    }
}
