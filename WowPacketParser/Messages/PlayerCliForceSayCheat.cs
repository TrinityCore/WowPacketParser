using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliForceSayCheat
    {
        public ulong Target;
        public int TextID;
    }
}
