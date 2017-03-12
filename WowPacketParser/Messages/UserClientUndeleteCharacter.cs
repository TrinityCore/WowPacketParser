using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientUndeleteCharacter
    {
        public ulong CharacterGuid;
        public int ClientToken;
    }
}
