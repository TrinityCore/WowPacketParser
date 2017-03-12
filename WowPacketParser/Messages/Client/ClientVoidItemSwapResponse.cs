namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientVoidItemSwapResponse
    {
        public ulong VoidItemB;
        public ulong VoidItemA;
        public uint VoidItemSlotB;
        public uint VoidItemSlotA;
    }
}
