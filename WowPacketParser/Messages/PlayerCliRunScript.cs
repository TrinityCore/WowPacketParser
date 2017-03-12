using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliRunScript
    {
        public string Text;
        public ulong Target;
    }
}
