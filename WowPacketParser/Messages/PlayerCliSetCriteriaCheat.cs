using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetCriteriaCheat
    {
        public ulong Quantity;
        public uint CriteriaID;
    }
}
