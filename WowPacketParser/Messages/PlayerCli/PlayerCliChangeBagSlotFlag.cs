namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliChangeBagSlotFlag
    {
        public bool On;
        public uint FlagToChange;
        public uint BagIndex;
    }
}
