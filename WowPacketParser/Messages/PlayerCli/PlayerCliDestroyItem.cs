namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliDestroyItem
    {
        public uint Count;
        public byte SlotNum;
        public byte ContainerId;
    }
}
