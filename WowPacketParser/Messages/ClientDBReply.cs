using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDBReply
    {
        public uint TableHash;
        public uint Timestamp;
        public int RecordID;
        public Data Data;
    }
}
