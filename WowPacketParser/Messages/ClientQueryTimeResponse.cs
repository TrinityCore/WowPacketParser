using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryTimeResponse
    {
        public UnixTime CurrentTime;
        public int TimeOutRequest;
    }
}
