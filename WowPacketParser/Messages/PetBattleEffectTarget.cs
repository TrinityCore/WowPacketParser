using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleEffectTarget
    {
        public PetBattleEffectTargetEx Type;
        public byte Petx;
        public uint AuraInstanceID;
        public uint AuraAbilityID;
        public int RoundsRemaining;
        public int CurrentRound;
        public uint StateID;
        public int StateValue;
        public int Health;
        public int NewStatValue;
        public int TriggerAbilityID;
        public int ChangedAbilityID;
        public int CooldownRemaining;
        public int LockdownRemaining;
        public int BroadcastTextID;
    }
}
