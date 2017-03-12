using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleEffect
    {
        public uint AbilityEffectID;
        public ushort Flags;
        public ushort SourceAuraInstanceID;
        public ushort TurnInstanceID;
        public sbyte PetBattleEffectType;
        public byte CasterPBOID;
        public byte StackDepth;
        public List<PetBattleEffectTarget> Targets;
    }
}
