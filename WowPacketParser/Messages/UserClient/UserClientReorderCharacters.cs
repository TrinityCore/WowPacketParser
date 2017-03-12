using System.Collections.Generic;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientReorderCharacters
    {
        public List<UserClientReorderEntry> Entries;
    }
}
