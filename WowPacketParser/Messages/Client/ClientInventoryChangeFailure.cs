namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientInventoryChangeFailure
    {
        public sbyte BagResult;
        public byte ContainerBSlot;
        public ulong SrcContainer;
        public ulong DstContainer;
        public int SrcSlot;
        public int LimitCategory;
        public int Level;
        public fixed ulong Item[2];
    }
}
