using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetCurrency
    {
        public bool SuppressChatLog;
        public int? TrackedQuantity; // Optional
        public int Quantity;
        public uint Flags;
        public int Type;
        public int? WeeklyQuantity; // Optional
    }
}
