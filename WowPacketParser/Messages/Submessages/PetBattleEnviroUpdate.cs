using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleEnviroUpdate
    {
        public List<PetBattleActiveAura> Auras;
        public List<PetBattleActiveState> States;
    }
}
