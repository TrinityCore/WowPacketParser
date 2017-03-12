using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientResurrectResponse
    {
        public ulong Resurrecter;
        public uint Response;
    }
}
