using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetupCurrencyRecord
    {
        public int Type;
        public int Quantity;
        public int? WeeklyQuantity; // Optional
        public int? MaxWeeklyQuantity; // Optional
        public int? TrackedQuantity; // Optional
        public byte Flags;
    }
}
