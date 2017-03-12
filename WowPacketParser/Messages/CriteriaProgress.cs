using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CriteriaProgress
    {
        public int Id;
        public ulong Quantity;
        public ulong Player;
        public int Flags;
        public Data Date;
        public UnixTime TimeFromStart;
        public UnixTime TimeFromCreate;
    }
}
