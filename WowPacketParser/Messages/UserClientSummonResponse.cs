using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSummonResponse
    {
        public bool Accept;
        public ulong SummonerGUID;
    }
}
