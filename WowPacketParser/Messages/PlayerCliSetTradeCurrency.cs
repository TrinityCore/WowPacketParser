using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetTradeCurrency
    {
        public uint Type;
        public uint Quantity;
    }
}
