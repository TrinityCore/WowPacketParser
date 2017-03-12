using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaShareInfoResponse
    {
        public uint AreaShareInfoID;
        public uint CurrentRealm;
        public uint AreaID;
        public List<uint> OtherRealms;
    }
}
