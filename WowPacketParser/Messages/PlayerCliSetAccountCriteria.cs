using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetAccountCriteria
    {
        public ulong Quantity;
        public int CriteriaID;
    }
}
