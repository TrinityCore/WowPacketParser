using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryTimeResponse
    {
        public UnixTime CurrentTime;
        public int TimeOutRequest;
    }
}
