using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLiveRegionGetAccountCharacterList
    {
        public string DevRealmOverride;
        public uint Token;
        public string DevCharOverride;
        public byte? RegionID; // Optional
    }
}
