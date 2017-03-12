using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerDebugPlayerInside
    {
        public ulong TriggerGUID;
        public bool Inside;
    }
}
