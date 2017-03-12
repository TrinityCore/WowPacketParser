namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCharacterRenameRequest
    {
        public string NewName;
        public ulong Guid;
    }
}
