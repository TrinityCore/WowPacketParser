using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDBReply
    {
        public uint TableHash;
        public uint Timestamp;
        public int RecordID;
        public Data Data;
    }
}
