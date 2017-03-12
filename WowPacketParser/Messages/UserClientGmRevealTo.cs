using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGmRevealTo
    {
        public uint Type;
        public string Name;
    }
}
