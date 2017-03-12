namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LFGPlayerRewards
    {
        public int RewardItem;
        public uint RewardItemQuantity;
        public int BonusCurrency;
        public bool IsCurrency;
    }
}
