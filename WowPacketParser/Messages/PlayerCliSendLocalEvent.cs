using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSendLocalEvent
    {
        public ulong TargetGUID;
        public int EventID;
    }
}
