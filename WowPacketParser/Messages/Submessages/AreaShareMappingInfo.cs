using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct AreaShareMappingInfo
    {
        public uint AreaShareID;
        public List<uint> AreaIDs;
        public uint HostingRealm;
        public List<uint> OtherRealms;
    }
}
