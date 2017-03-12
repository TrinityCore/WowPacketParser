using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientComplaintOffender
    {
        public ulong PlayerGuid;
        public uint RealmAddress;
        public uint TimeSinceOffence;
    }
}
