using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaShareMappingsResponse // FIXME: No handlers
    {
        public List<AreaShareMappingInfo> Mappings;
    }
}
