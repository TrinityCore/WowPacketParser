namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSummonRequest
    {
        public ulong SummonerGUID;
        public uint SummonerVirtualRealmAddress;
        public int AreaID;
    }
}
