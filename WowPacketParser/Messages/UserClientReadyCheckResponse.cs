using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientReadyCheckResponse
    {
        public byte PartyIndex;
        public ulong PartyGUID;
        public bool IsReady;
    }
}
