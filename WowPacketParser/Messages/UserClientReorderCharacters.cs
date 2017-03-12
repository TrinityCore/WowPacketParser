using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientReorderCharacters
    {
        public List<UserClientReorderEntry> Entries;
    }
}
