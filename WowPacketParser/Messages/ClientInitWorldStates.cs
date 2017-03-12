using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInitWorldStates
    {
        public List<ClientWorldStateInfo> Worldstates;
        public int AreaID;
        public int SubareaID;
        public int MapID;
    }
}
