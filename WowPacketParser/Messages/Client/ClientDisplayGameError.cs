namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDisplayGameError
    {
        public int? Arg2; // Optional
        public int? Arg; // Optional
        public uint Error;
    }
}
