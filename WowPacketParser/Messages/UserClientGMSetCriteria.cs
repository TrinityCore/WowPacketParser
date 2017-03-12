using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMSetCriteria
    {
        public int CriteriaID;
        public string Target;
        public int Quantity;
        public ulong Guid;
    }
}
