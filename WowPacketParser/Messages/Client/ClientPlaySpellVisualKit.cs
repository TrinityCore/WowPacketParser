namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPlaySpellVisualKit
    {
        public ulong Unit;
        public int KitType;
        public uint Duration;
        public int KitRecID;
    }
}
