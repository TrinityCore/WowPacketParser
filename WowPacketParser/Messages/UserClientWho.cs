using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientWho
    {
        public WhoRequest Request;
        public List<int> Areas;
    }
}
