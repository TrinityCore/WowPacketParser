using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetEveryoneIsAssistant
    {
        public byte PartyIndex;
        public bool EveryoneIsAssistant;
    }
}
