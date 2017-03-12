using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliUseToy
    {
        public SpellCastRequest Cast;
        public int ItemID;
    }
}
