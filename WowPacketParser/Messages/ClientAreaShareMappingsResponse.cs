using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaShareMappingsResponse
    {
        public List<AreaShareMappingInfo> Mappings;
    }
}
