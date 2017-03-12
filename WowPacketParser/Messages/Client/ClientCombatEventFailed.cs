namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCombatEventFailed
    {
        public ulong Victim;
        public ulong Attacker;
    }
}
