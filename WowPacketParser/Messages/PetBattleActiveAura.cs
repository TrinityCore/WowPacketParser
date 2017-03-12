using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleActiveAura
    {
        public int AbilityID;
        public uint InstanceID;
        public int RoundsRemaining;
        public int CurrentRound;
        public byte CasterPBOID;
    }
}
