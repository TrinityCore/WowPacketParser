using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetBattleDebugQueueDumpResponse
    {
        public List<PBQueueDumpMember> Members;
        public UnixTime AverageQueueTime;
    }
}
