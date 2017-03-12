namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ServerBuckDataEntry
    {
        public ulong Arg;
        public string Argname;
        public ulong Count;
        public ulong Accum;
        public ulong Sqaccum;
        public ulong Maximum;
        public ulong Minimum;
    }
}
