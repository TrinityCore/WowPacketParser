namespace WowPacketParser.Messages.CliChat
{
    public unsafe struct GMChat // FIXME: No handlers
    {
        public string Source;
        public string Arguments;
        public string Dest;
        public ulong Target;
    }
}
