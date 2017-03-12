namespace WowPacketParser.Messages.Global
{
    public unsafe struct GlobalGuildNewsCreateCheat
    {
        public ulong Guild;
        public uint DateOffset;
        public uint Data;
        public uint NewsType;
    }
}
