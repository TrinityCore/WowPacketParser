using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientResetAreaTrigger
    {
        public ulong TriggerGUID;
        public CliAreaTrigger AreaTrigger;
    }
}
