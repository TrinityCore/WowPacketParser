using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetActionFeedback
    {
        public int SpellID;
        public byte Response;
    }
}
