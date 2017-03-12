using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetWorldState
    {
        public int VariableID;
        public int Value;
    }
}
