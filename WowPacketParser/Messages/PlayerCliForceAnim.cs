using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliForceAnim
    {
        public ulong Target;
        public string Arguments;
    }
}
