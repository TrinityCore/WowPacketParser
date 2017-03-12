using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerReShape
    {
        public CliAreaTriggerPolygon AreaTriggerPolygon;
        public ulong TriggerGUID;
    }
}
