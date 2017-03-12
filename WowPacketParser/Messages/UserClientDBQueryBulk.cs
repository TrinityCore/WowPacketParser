using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDBQueryBulk
    {
        public uint TableHash;
        public List<DBQuery> Queries;
    }
}
