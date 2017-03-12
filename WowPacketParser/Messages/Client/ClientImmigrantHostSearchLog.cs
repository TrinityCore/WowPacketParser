using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientImmigrantHostSearchLog
    {
        public bool Success;
        public List<ImmigrantHostSearchLog> Entries;
    }
}
