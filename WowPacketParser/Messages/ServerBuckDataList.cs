using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ServerBuckDataList
    {
        public uint MpID;
        public List<ServerBuckDataEntry> Entries;
    }
}
