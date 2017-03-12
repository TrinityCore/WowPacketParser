namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDuelWinner
    {
        public string BeatenName;
        public string WinnerName;
        public bool Fled;
        public uint BeatenVirtualRealmAddress;
        public uint WinnerVirtualRealmAddress;
    }
}
