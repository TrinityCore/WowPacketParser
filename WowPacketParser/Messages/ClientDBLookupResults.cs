using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDBLookupResults
    {
        public uint NumResults;
        public List<byte> Results;
    }
}
