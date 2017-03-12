using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRealmQueryResponse
    {
        public uint VirtualRealmAddress;
        public byte LookupState;
        public VirtualRealmNameInfo NameInfo;
    }
}
