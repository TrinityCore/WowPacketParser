namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLogXPGain
    {
        public int Amount;
        public float GroupBonus;
        public byte Reason;
        public bool ReferAFriend;
        public int Original;
        public ulong Victim;
    }
}
