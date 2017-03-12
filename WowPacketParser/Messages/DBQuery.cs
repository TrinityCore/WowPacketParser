using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct DBQuery
    {
        public ulong Guid;
        public int RecordID;
    }
}
