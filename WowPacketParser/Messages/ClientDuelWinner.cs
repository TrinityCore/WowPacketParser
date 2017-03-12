using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDuelWinner
    {
        public string BeatenName;
        public string WinnerName;
        public bool Fled;
        public uint BeatenVirtualRealmAddress;
        public uint WinnerVirtualRealmAddress;
    }
}
