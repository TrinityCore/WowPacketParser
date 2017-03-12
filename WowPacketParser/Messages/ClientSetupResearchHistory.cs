using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetupResearchHistory
    {
        public List<ResearchHistory> History;
    }
}
