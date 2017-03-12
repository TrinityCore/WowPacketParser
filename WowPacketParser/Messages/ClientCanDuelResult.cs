using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCanDuelResult
    {
        public bool Result;
        public ulong TargetGUID;
    }
}
