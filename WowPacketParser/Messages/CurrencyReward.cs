using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CurrencyReward
    {
        public uint CurrencyID;
        public uint Quantity;
    }
}
