using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCharacterRenameRequest
    {
        public string NewName;
        public ulong Guid;
    }
}
