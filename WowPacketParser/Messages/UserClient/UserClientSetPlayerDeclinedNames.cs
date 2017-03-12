namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetPlayerDeclinedNames
    {
        public ulong Player;
        public string[/*5*/] DeclinedName;
    }
}
