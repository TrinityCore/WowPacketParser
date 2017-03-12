namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetItemChallengeModeData
    {
        public ulong ItemGUID;
        public fixed int Stats[6];
    }
}
