namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPartyCommandResult
    {
        public string Name;
        public uint ResultData;
        public ulong ResultGUID;
        public byte Result;
        public byte Command;
    }
}
