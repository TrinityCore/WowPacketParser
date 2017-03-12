using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientImmigrantHostSearchLog
    {
        public bool Success;
        public List<ImmigrantHostSearchLog> Entries;
    }
}
