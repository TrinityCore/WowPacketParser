namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct PetitionRenameGuild
    {
        public ulong PetitionGuid;
        public string NewGuildName;
    }
}
