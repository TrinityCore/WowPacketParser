using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCriteriaUpdate
    {
        public Data CurrentTime;
        public int Flags;
        public ulong Quantity;
        public ulong PlayerGUID;
        public int CriteriaID;
        public UnixTime ElapsedTime;
        public UnixTime CreationTime;
    }
}
