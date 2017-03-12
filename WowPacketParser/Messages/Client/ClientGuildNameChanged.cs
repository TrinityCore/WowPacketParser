namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildNameChanged
    {
        public ulong GuildGUID;
        public string GuildName;
    }
}
