using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct HotfixNotify
    {
        public uint TableHash;
        public int RecordID;
        public uint Timestamp;
    }
}
