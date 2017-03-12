using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAllAccountCriteria
    {
        public List<CriteriaProgress> Progress;
    }
}
