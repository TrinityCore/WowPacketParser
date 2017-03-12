using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleMaxGameLengthWarning
    {
        public UnixTime TimeRemaining;
        public int RoundsRemaining;
    }
}
