namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAttackStop
    {
        public ulong Attacker;
        public ulong Victim;
        public bool NowDead;
    }
}
