using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientForceActionShowResponse
    {
        public uint DebugCombatVictimActions;
        public uint DebugCombatActions;
    }
}
