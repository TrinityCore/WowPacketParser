using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCancelCast
    {
        public int SpellID;
        public byte CastID;
    }
}
