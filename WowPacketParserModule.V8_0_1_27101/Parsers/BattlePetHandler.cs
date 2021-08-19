using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class BattlePetHandler
    {
        public static void ReadPetBattleEffectTarget(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            var type = packet.ReadBits("Type", 4, idx); // enum PetBattleEffectTargetEx
            packet.ReadByte("Petx", idx);

            switch (type)
            {
                case 1:
                    packet.ReadUInt32("AuraInstanceID", idx);
                    packet.ReadUInt32("AuraAbilityID", idx);
                    packet.ReadInt32("RoundsRemaining", idx);
                    packet.ReadInt32("CurrentRound", idx);
                    break;
                case 2:
                    packet.ReadUInt32("StateID", idx);
                    packet.ReadInt32("StateValue", idx);
                    break;
                case 3:
                    packet.ReadInt32("Health", idx);
                    break;
                case 4:
                    packet.ReadInt32("NewStatValue", idx);
                    break;
                case 5:
                    packet.ReadInt32("TriggerAbilityID", idx);
                    break;
                case 6:
                    packet.ReadInt32("ChangedAbilityID", idx);
                    packet.ReadInt32("CooldownRemaining", idx);
                    packet.ReadInt32("LockdownRemaining", idx);
                    break;
                case 7:
                    packet.ReadInt32("BroadcastTextID", idx);
                    break;
                // type 8 is unhandled, last checked in build 8.2.5.32750
                case 9:
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                    {
                        packet.ReadInt32("Type9_Unk1", idx);
                        packet.ReadInt32("Type9_Unk2", idx);
                    }
                    break;
            }
        }

        public static void ReadPetBattleEffect(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("AbilityEffectID", idx);
            packet.ReadUInt16("Flags", idx);
            packet.ReadUInt16("SourceAuraInstanceID", idx);
            packet.ReadUInt16("TurnInstanceID", idx);
            packet.ReadSByte("PetBattleEffectType", idx);
            packet.ReadByte("CasterPBOID", idx);
            packet.ReadByte("StackDepth", idx);

            var targetsCount = packet.ReadInt32("TargetsCount", idx);

            for (var i = 0; i < targetsCount; ++i)
                ReadPetBattleEffectTarget(packet, i);
        }

        public static void ReadPetBattleActiveAbility(Packet packet, params object[] idx)
        {
            packet.ReadInt32("AbilityID", idx);
            packet.ReadInt16("CooldownRemaining", idx);
            packet.ReadInt16("LockdownRemaining", idx);
            packet.ReadSByte("AbilityIndex", idx);
            packet.ReadByte("Pboid", idx);
        }

        public static void ReadPetBattleRoundResult(Packet packet, params object[] idx)
        {
            packet.ReadInt32("CurRound", idx);
            packet.ReadSByte("NextPetBattleState", idx);

            var effectsCount = packet.ReadUInt32("EffectsCount", idx);

            for (var i = 0; i < 2; ++i)
            {
                packet.ReadByte("NextInputFlags", idx, i);
                packet.ReadSByte("NextTrapStatus", idx, i);
                packet.ReadUInt16("RoundTimeSecs", idx, i);
            }

            var cooldownsCount = packet.ReadUInt32("CooldownsCount", idx);

            for (var i = 0; i < cooldownsCount; ++i)
                ReadPetBattleActiveAbility(packet, idx, "Cooldowns", i);

            packet.ResetBitReader();
            var petXDiedCount = packet.ReadBits("PetXDied", 3, idx);

            for (var i = 0; i < effectsCount; ++i)
                ReadPetBattleEffect(packet, idx, "Effects", i);

            for (var i = 0; i < petXDiedCount; ++i)
                packet.ReadSByte("PetXDied", idx, i);
        }
    }
}
