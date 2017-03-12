using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemTimeUpdate
    {
        public ulong ItemGuid;
        public uint DurationLeft;
    }
}
