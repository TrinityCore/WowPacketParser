using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMapControllerSetPointsCheat
    {
        public int Points;
        public byte Team;
    }
}
