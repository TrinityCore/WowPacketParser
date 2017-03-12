using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliUseToy
    {
        public SpellCastRequest Cast;
        public int ItemID;
    }
}
