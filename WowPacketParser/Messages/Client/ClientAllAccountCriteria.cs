using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAllAccountCriteria
    {
        public List<CriteriaProgress> Progress;
    }
}
