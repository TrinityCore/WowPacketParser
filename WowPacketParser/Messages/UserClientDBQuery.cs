using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDBQuery
    {
        public ulong Guid;
        public uint TableHash;
        public int RecordID;
    }
}
