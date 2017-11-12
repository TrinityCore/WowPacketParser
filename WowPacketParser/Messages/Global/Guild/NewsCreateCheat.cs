namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct NewsCreateCheat
    {
        public ulong Guild;
        public uint DateOffset;
        public uint Data;
        public uint NewsType;
    }
}
