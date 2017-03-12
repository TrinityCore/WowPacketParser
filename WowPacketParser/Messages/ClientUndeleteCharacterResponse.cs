using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUndeleteCharacterResponse
    {
        public ulong CharacterGuid;
        public int ClientToken;
        public UndeleteCharacterResult Result;
    }
}
