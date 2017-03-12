using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateWorldState
    {
        public int Value;
        public bool Hidden;
        public int VariableID;
    }
}
