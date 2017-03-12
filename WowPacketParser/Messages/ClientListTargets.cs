using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientListTargets
    {
        public ulong UnitGUID;
        public List<ClientTargetThreat> Targets;
    }
}
