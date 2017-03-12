using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientClearQuestCompletedBits
    {
        public List<int> Qbits;
    }
}
