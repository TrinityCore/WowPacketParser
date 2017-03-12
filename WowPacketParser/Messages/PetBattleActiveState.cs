using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleActiveState
    {
        public uint StateID;
        public int StateValue;
    }
}
