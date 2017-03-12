using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDBQueryBulk
    {
        public uint TableHash;
        public List<DBQuery> Queries;
    }
}
