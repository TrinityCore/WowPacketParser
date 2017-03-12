namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUpdateCharacterFlags
    {
        public ulong Character;
        public uint? Flags3; // Optional
        public uint? Flags; // Optional
        public uint? Flags2; // Optional
    }
}
