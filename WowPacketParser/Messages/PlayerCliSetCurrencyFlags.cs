using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetCurrencyFlags
    {
        public uint Flags;
        public uint CurrencyID;
    }
}
