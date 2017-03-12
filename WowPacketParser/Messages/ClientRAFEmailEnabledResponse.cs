using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRAFEmailEnabledResponse
    {
        public RafEmailEnabledResponse Result;
    }
}
