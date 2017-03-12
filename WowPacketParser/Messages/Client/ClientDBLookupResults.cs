using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDBLookupResults
    {
        public uint NumResults;
        public List<byte> Results;
    }
}
