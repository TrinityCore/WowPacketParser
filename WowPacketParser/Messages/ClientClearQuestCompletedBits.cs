using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientClearQuestCompletedBits
    {
        public List<int> Qbits;
    }
}
