namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientUndeleteCharacter
    {
        public ulong CharacterGuid;
        public int ClientToken;
    }
}
