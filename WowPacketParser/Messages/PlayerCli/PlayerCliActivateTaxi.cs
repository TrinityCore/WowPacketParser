namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliActivateTaxi
    {
        public ulong Vendor;
        public uint StartNode;
        public uint DestNode;
    }
}
