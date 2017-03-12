using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct AreaShareMappingInfo
    {
        public uint AreaShareID;
        public List<uint> AreaIDs;
        public uint HostingRealm;
        public List<uint> OtherRealms;
    }
}
