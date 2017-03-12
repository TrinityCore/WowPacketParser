using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerDebugSweep
    {
        public ulong TriggerGUID;
        public uint TimeFromCreation1;
        public uint TimeFromCreation0;
    }
}
