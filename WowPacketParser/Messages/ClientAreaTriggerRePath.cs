using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerRePath
    {
        public CliAreaTriggerSpline AreaTriggerSpline;
        public ulong TriggerGUID;
    }
}
