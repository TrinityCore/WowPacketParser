namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSetBankBagSlotAutosortDisabled
    {
        public bool Disable;
        public uint BagIndex;
    }
}
