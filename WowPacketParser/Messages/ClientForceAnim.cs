using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientForceAnim
    {
        public string Arguments;
        public ulong Target;
    }
}
