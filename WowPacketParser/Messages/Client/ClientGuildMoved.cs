namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildMoved
    {
        public string TargetRealmName;
        public ulong GuildGUID;
    }
}
