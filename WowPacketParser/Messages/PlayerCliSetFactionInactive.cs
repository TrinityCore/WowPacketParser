using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetFactionInactive
    {
        public bool State;
        public int Index;
    }
}
