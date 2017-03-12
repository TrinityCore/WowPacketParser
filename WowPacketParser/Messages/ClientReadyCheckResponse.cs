using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientReadyCheckResponse
    {
        public bool IsReady;
        public ulong Player;
        public ulong PartyGUID;
    }
}
