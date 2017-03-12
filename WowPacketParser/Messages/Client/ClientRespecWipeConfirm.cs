namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRespecWipeConfirm
    {
        public ulong RespecMaster;
        public uint Cost;
        public sbyte RespecType;
    }
}
