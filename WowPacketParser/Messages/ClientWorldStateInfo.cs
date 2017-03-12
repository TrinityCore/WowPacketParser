using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientWorldStateInfo
    {
        public int VariableID;
        public int Value;
    }
}
