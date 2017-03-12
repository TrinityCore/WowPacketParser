namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientItemTimeUpdate
    {
        public ulong ItemGuid;
        public uint DurationLeft;
    }
}
