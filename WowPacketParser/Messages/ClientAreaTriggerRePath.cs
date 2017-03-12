using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerRePath
    {
        public CliAreaTriggerSpline AreaTriggerSpline;
        public ulong TriggerGUID;
    }
}
