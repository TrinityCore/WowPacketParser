using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ServerBuckDataList
    {
        public uint MpID;
        public List<ServerBuckDataEntry> Entries;
    }
}
