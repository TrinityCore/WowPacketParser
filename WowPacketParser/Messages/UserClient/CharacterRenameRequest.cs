namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct CharacterRenameRequest
    {
        public string NewName;
        public ulong Guid;
    }
}
