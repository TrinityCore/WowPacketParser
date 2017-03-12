using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSuppressNPCGreetings
    {
        public bool SuppressNPCGreetings;
        public ulong UnitGUID;
    }
}
