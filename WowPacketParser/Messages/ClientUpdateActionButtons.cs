using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateActionButtons
    {
        public fixed ulong ActionButtons[132];
        public byte Reason;
    }
}
