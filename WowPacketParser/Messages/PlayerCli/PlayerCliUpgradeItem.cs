namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliUpgradeItem
    {
        public ulong ItemMaster;
        public ulong ItemGUID;
        public int ContainerSlot;
        public int UpgradeID;
        public int Slot;
    }
}
