using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuraInfo
    {
        public byte Slot;
        public ClientAuraDataInfo? AuraData; // Optional
    }
}
