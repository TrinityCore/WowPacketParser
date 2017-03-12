using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLiveRegionAccountRestore
    {
        public uint Token;
        public string DevRealmOverride;
        public byte RegionID; // Optional
        public string DevCharOverride;
    }
}
