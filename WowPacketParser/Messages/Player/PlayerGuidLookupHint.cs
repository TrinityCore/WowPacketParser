namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerGuidLookupHint
    {
        public uint? VirtualRealmAddress; // Optional
        public uint? NativeRealmAddress; // Optional
    }
}
