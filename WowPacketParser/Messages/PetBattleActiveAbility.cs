using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleActiveAbility
    {
        public int AbilityID;
        public short CooldownRemaining;
        public short LockdownRemaining;
        public sbyte AbilityIndex;
        public byte Pboid;
    }
}
