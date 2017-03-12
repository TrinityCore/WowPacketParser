using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUndeleteCharacterResponse
    {
        public ulong CharacterGuid;
        public int ClientToken;
        public UndeleteCharacterResult Result;
    }
}
