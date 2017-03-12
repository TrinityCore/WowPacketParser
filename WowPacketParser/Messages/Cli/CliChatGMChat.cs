namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatGMChat // FIXME: No handlers
    {
        public string Source;
        public string Arguments;
        public string Dest;
        public ulong Target;
    }
}
