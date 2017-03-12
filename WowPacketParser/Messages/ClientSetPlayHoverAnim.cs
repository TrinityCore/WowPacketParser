using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetPlayHoverAnim
    {
        public ulong UnitGUID;
        public bool PlayHoverAnim;
    }
}
