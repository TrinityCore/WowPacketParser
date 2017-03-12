using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePetError
    {
        public Battlepetresult BattlePetResult;
        public int CreatureID;
    }
}
