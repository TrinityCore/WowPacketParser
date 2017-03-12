namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGameObjectPlaySpellVisualKit
    {
        public ulong Object;
        public int KitRecID;
        public int KitType;
        public uint Duration;
    }
}
