using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildCriteriaProgress
    {
        public int CriteriaID;
        public UnixTime DateCreated;
        public UnixTime DateStarted;
        public UnixTime DateUpdated;
        public ulong Quantity;
        public ulong PlayerGUID;
        public int Flags;
    }
}
