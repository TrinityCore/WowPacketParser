using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInitialSetup
    {
        public byte ServerExpansionTier;
        public byte ServerExpansionLevel;
        public UnixTime RaidOrigin;
        public List<byte> QuestsCompleted;
        public int ServerRegionID;
    }
}
