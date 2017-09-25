namespace WowPacketParser.Messages.UserClient.Commentator
{
    public unsafe struct StartWargame
    {
        public ulong QueueID;
        public string[/*2*/] Names;
    }
}
