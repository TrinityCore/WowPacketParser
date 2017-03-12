using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientWho
    {
        public WhoRequest Request;
        public List<int> Areas;
    }
}
