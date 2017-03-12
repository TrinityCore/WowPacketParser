using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCharacterRenameResult
    {
        public string Name;
        public byte Result;
        public ulong Guid; // Optional
    }
}
