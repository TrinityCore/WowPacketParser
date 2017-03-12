using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSummonRequest
    {
        public ulong SummonerGUID;
        public uint SummonerVirtualRealmAddress;
        public int AreaID;
    }
}
