using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientNeutralPlayerFactionSelectResult
    {
        public bool Success;
        public int NewRaceID;
    }
}
