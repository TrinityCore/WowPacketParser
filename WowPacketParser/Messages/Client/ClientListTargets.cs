using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientListTargets
    {
        public ulong UnitGUID;
        public List<ClientTargetThreat> Targets;
    }
}
