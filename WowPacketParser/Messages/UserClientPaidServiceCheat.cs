using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientPaidServiceCheat
    {
        public ulong Guid;
        public int ServiceID;
    }
}
