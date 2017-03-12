namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellTargetData
    {
        public uint Flags;
        public ulong Unit;
        public ulong Item;
        public Location? SrcLocation; // Optional
        public Location? DstLocation; // Optional
        public string Name;
    }
}
