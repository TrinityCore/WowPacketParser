using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLevelUpInfo
    {
        public int Cp;
        public fixed int StatDelta[5];
        public int HealthDelta;
        public fixed int PowerDelta[6];
        public int Level;
    }
}
