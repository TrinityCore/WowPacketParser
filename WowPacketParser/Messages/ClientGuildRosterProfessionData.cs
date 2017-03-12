using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildRosterProfessionData
    {
        public int DbID;
        public int Rank;
        public int Step;
    }
}
