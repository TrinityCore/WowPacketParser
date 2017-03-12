using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientServerCommand
    {
        public string Line;
        public ulong Target;
    }
}
