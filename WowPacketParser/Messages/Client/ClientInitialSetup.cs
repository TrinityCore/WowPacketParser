using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
