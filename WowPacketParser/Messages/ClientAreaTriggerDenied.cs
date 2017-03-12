using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerDenied
    {
        public bool Entered;
        public int AreaTriggerID;
    }
}
