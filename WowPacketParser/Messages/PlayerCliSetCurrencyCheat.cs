using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetCurrencyCheat
    {
        public int Quantity;
        public int Type;
    }
}
