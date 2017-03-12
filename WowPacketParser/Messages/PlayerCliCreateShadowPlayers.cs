using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCreateShadowPlayers
    {
        public ushort NumCopies;
        public sbyte Type;
    }
}
