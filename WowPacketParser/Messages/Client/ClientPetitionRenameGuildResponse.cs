namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetitionRenameGuildResponse
    {
        public ulong PetitionGuid;
        public string NewGuildName;
    }
}
