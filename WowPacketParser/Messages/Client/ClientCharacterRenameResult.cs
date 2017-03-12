namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCharacterRenameResult
    {
        public string Name;
        public byte Result;
        public ulong? Guid; // Optional
    }
}
