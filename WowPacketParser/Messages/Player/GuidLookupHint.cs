namespace WowPacketParser.Messages.Player
{
    public unsafe struct GuidLookupHint
    {
        public uint? VirtualRealmAddress; // Optional
        public uint? NativeRealmAddress; // Optional
    }
}
