using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaShareInfoResponse
    {
        public uint AreaShareInfoID;
        public uint CurrentRealm;
        public uint AreaID;
        public List<uint> OtherRealms;
    }
}
