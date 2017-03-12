using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientWho
    {
        public WhoRequest Request;
        public List<int> Areas;
    }
}
