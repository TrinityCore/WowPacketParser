using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAreaTrigger
    {
        public bool Entered;
        public bool FromClient;
        public int AreaTriggerID;
    }
}
