using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerReShape
    {
        public CliAreaTriggerPolygon AreaTriggerPolygon;
        public ulong TriggerGUID;
    }
}
