using WowPacketParser.Misc;

namespace WowPacketParserModule.V6_1_0_19678.Parsers
{
    public static class BattlePetHandler
    {
        public static void ReadPetBattlePetUpdate(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("BattlePetGUID", idx);

            packet.ReadInt32("SpeciesID", idx);
            packet.ReadInt32("DisplayID", idx);
            packet.ReadInt32("CollarID", idx);

            packet.ReadInt16("Level", idx);
            packet.ReadInt16("Xp", idx);

            packet.ReadInt32("CurHealth", idx);
            packet.ReadInt32("MaxHealth", idx);
            packet.ReadInt32("Power", idx);
            packet.ReadInt32("Speed", idx);
            packet.ReadInt32("NpcTeamMemberID", idx);

            packet.ReadInt16("BreedQuality", idx);
            packet.ReadInt16("StatusFlags", idx);

            packet.ReadByte("Slot", idx);

            var abilitiesCount = packet.ReadInt32("AbilitiesCount", idx);
            var aurasCount = packet.ReadInt32("AurasCount", idx);
            var statesCount = packet.ReadInt32("StatesCount", idx);

            for (var i = 0; i < abilitiesCount; ++i)
                ReadPetBattleActiveAbility(packet, idx, "Abilities", i);

            for (var i = 0; i < aurasCount; ++i)
                ReadPetBattleActiveAura(packet, idx, "Auras", i);

            for (var i = 0; i < statesCount; ++i)
                ReadPetBattleActiveState(packet, idx, "States", i);

            packet.ResetBitReader();

            var bits57 = packet.ReadBits(7);
            packet.ReadWoWString("CustomName", bits57, idx);
        }

        public static void ReadPetBattlePlayerUpdate(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CharacterID", idx);

            packet.ReadInt32("TrapAbilityID", idx);
            packet.ReadInt32("TrapStatus", idx);

            packet.ReadInt16("RoundTimeSecs", idx);

            packet.ReadSByte("FrontPet", idx);
            packet.ReadByte("InputFlags", idx);

            packet.ResetBitReader();

            var petsCount = packet.ReadBits("PetsCount", 2, idx);

            for (var i = 0; i < petsCount; ++i)
                ReadPetBattlePetUpdate(packet, idx, "Pets", i);
        }

        public static void ReadPetBattleActiveState(Packet packet, params object[] idx)
        {
            packet.ReadInt32("StateID", idx);
            packet.ReadInt32("StateValue", idx);
        }

        public static void ReadPetBattleActiveAbility(Packet packet, params object[] idx)
        {
            packet.ReadInt32("AbilityID", idx);
            packet.ReadInt16("CooldownRemaining", idx);
            packet.ReadInt16("LockdownRemaining", idx);
            packet.ReadByte("AbilityIndex", idx);
            packet.ReadByte("Pboid", idx);
        }

        public static void ReadPetBattleActiveAura(Packet packet, params object[] idx)
        {
            packet.ReadInt32("AbilityID", idx);
            packet.ReadInt32("InstanceID", idx);
            packet.ReadInt32("RoundsRemaining", idx);
            packet.ReadInt32("CurrentRound", idx);
            packet.ReadByte("CasterPBOID", idx);
        }

        public static void ReadPetBattleEnviroUpdate(Packet packet, params object[] idx)
        {
            var aurasCount = packet.ReadInt32("AurasCount", idx);
            var statesCount = packet.ReadInt32("StatesCount", idx);

            for (var i = 0; i < aurasCount; ++i) // Auras
                ReadPetBattleActiveAura(packet, idx, i);

            for (var i = 0; i < statesCount; ++i) // States
                ReadPetBattleActiveState(packet, idx, i);
        }

        public static void ReadPetBattleFullUpdate(Packet packet, params object[] idx)
        {
            for (var i = 0; i < 2; ++i)
                ReadPetBattlePlayerUpdate(packet, idx, "Players", i);

            for (var i = 0; i < 3; ++i)
                ReadPetBattleEnviroUpdate(packet, idx, "Enviros", i);

            packet.ReadInt16("WaitingForFrontPetsMaxSecs", idx);
            packet.ReadInt16("PvpMaxRoundTime", idx);

            packet.ReadInt32("CurRound", idx);
            packet.ReadInt32("NpcCreatureID", idx);
            packet.ReadInt32("NpcDisplayID", idx);

            packet.ReadByte("CurPetBattleState", idx);
            packet.ReadByte("ForfeitPenalty", idx);

            packet.ReadPackedGuid128("InitialWildPetGUID", idx);

            packet.ReadBit("IsPVP", idx);
            packet.ReadBit("CanAwardXP", idx);
        }
    }
}
