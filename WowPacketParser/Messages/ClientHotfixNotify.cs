using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientHotfixNotify
    {
        public uint TableHash;
        public uint Timestamp;
        public int RecordID;
    }
}
