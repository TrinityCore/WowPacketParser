using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientInitWorldStates
    {
        public List<ClientWorldStateInfo> Worldstates;
        public int AreaID;
        public int SubareaID;
        public int MapID;
    }
}
