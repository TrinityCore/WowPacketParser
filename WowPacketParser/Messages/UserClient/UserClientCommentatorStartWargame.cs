namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCommentatorStartWargame
    {
        public ulong QueueID;
        public string[/*2*/] Names;
    }
}
