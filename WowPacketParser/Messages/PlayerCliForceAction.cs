using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliForceAction
    {
        public byte Category;
        public bool OnSelf;
        public bool Set;
    }
}
