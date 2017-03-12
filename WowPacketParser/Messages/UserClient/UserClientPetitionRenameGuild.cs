namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientPetitionRenameGuild
    {
        public ulong PetitionGuid;
        public string NewGuildName;
    }
}
