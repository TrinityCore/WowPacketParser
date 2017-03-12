using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleEnviroUpdate
    {
        public List<PetBattleActiveAura> Auras;
        public List<PetBattleActiveState> States;
    }
}
