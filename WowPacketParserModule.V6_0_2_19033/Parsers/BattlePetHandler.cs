using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED)]
        [Parser(Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL)]
        [Parser(Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL_LOCK)]
        [Parser(Opcode.SMSG_PET_BATTLE_FINISHED)]
        [Parser(Opcode.CMSG_PET_BATTLE_FINAL_NOTIFY)]
        [Parser(Opcode.CMSG_JOIN_PET_BATTLE_QUEUE)]
        [Parser(Opcode.SMSG_PET_BATTLE_QUEUE_PROPOSE_MATCH)]
        public static void HandleBattlePetZero(Packet packet)
        {
        }

        public static void ReadClientBattlePet(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("BattlePetGUID", idx);

            packet.ReadInt32("SpeciesID", idx);
            packet.ReadInt32("DisplayID", idx);
            packet.ReadInt32("CollarID", idx);

            packet.ReadInt16("BreedID", idx);
            packet.ReadInt16("Level", idx);
            packet.ReadInt16("Xp", idx);
            packet.ReadInt16("BattlePetDBFlags", idx);

            packet.ReadInt32("Power", idx);
            packet.ReadInt32("Health", idx);
            packet.ReadInt32("MaxHealth", idx);
            packet.ReadInt32("Speed", idx);

            packet.ReadByte("BreedQuality", idx);

            packet.ResetBitReader();

            var customNameLength = packet.ReadBits(7);
            var hasOwnerInfo = packet.ReadBit("HasOwnerInfo", idx);
            packet.ReadBit("NoRename", idx);

            if (hasOwnerInfo) // OwnerInfo
                ReadClientBattlePetOwnerInfo(packet, idx);

            packet.ReadWoWString("CustomName", customNameLength, idx);
        }

        public static void ReadClientPetBattleSlot(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("BattlePetGUID", idx);

            packet.ReadInt32("CollarID", idx);
            packet.ReadByte("SlotIndex", idx);

            packet.ResetBitReader();

            packet.ReadBit("Locked", idx);
        }

        public static void ReadClientBattlePetOwnerInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt32("PlayerVirtualRealm", idx);
            packet.ReadUInt32("PlayerNativeRealm", idx);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.ReadInt16("TrapLevel");

            var slotsCount = packet.ReadInt32("SlotsCount");
            var petsCount = packet.ReadInt32("PetsCount");

            for (var i = 0; i < slotsCount; i++)
                ReadClientPetBattleSlot(packet, i);

            for (var i = 0; i < petsCount; i++)
                ReadClientBattlePet(packet, i);

            packet.ResetBitReader();

            packet.ReadBit("HasJournalLock");
        }

        [Parser(Opcode.CMSG_QUERY_BATTLE_PET_NAME)]
        public static void HandleBattlePetQuery(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetID");
            packet.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_QUERY_BATTLE_PET_NAME_RESPONSE)]
        public static void HandleBattlePetQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetID");

            packet.ReadInt32("CreatureID");
            packet.ReadTime("Timestamp");

            packet.ResetBitReader();
            var bit40 = packet.ReadBit("Allow");
            if (!bit40)
                return;

            var bits49 = packet.ReadBits(8);
            packet.ReadBit("HasDeclined");

            var bits97 = new uint[5];
            for (int i = 0; i < 5; i++)
                bits97[i] = packet.ReadBits(7);

            for (int i = 0; i < 5; i++)
                packet.ReadWoWString("DeclinedNames", bits97[i]);

            packet.ReadWoWString("Name", bits49);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET)]
        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET_CHEAT)]
        [Parser(Opcode.CMSG_BATTLE_PET_SUMMON)]
        [Parser(Opcode.CMSG_CAGE_BATTLE_PET)]
        [Parser(Opcode.SMSG_BATTLE_PET_DELETED)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");

            packet.ResetBitReader();

            var bits342 = packet.ReadBits(7);
            var bit341 = packet.ReadBit("HasDeclinedNames");

            packet.ReadWoWString("Name", bits342);

            if (bit341)
            {
                var bits97 = new uint[5];
                for (int i = 0; i < 5; i++)
                    bits97[i] = packet.ReadBits(7);

                for (int i = 0; i < 5; i++)
                    packet.ReadWoWString("DeclinedNames", bits97[i]);
            }
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT)]
        public static void HandleBattlePetSetBattleSlot(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
            packet.ReadByte("SlotIndex");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_FLAGS)]
        public static void HandleBattlePetSetFlags(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
            packet.ReadInt32("Flags");
            packet.ReadBits("ControlType", 2);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var petsCount = packet.ReadInt32("PetsCount");

            for (var i = 0; i < petsCount; ++i)
                ReadClientBattlePet(packet, i);

            packet.ResetBitReader();

            packet.ReadBit("AddedPet");
        }

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

        public static void ReadPetBattleEffectTarget60x(Packet packet, params object[] idx)
        {
            var type = packet.ReadBits("Type", 3, idx); // enum PetBattleEffectTargetEx

            packet.ResetBitReader();

            packet.ReadByte("Petx", idx);

            switch (type)
            {
                case 5:
                    packet.ReadInt32("AuraInstanceID", idx);
                    packet.ReadInt32("AuraAbilityID", idx);
                    packet.ReadInt32("RoundsRemaining", idx);
                    packet.ReadInt32("CurrentRound", idx);
                    break;
                case 2:
                    packet.ReadInt32("StateID", idx);
                    packet.ReadInt32("StateValue", idx);
                    break;
                case 1:
                    packet.ReadInt32("Health", idx);
                    break;
                case 3:
                    packet.ReadInt32("NewStatValue", idx);
                    break;
                case 0:
                    packet.ReadInt32("TriggerAbilityID", idx);
                    break;
                case 7:
                    packet.ReadInt32("ChangedAbilityID", idx);
                    packet.ReadInt32("CooldownRemaining", idx);
                    packet.ReadInt32("LockdownRemaining", idx);
                    break;
                case 4:
                    packet.ReadInt32("BroadcastTextID", idx);
                    break;
            }
        }

        public static void ReadPetBattleEffectTarget610(Packet packet, params object[] idx)
        {
            var type = packet.ReadBits("Type", 3, idx); // enum PetBattleEffectTargetEx

            packet.ResetBitReader();

            packet.ReadByte("Petx", idx);

            switch (type)
            {
                case 3:
                    packet.ReadInt32("AuraInstanceID", idx);
                    packet.ReadInt32("AuraAbilityID", idx);
                    packet.ReadInt32("RoundsRemaining", idx);
                    packet.ReadInt32("CurrentRound", idx);
                    break;
                case 6:
                    packet.ReadInt32("StateID", idx);
                    packet.ReadInt32("StateValue", idx);
                    break;
                case 4:
                    packet.ReadInt32("Health", idx);
                    break;
                case 1:
                    packet.ReadInt32("NewStatValue", idx);
                    break;
                case 5:
                    packet.ReadInt32("TriggerAbilityID", idx);
                    break;
                case 7:
                    packet.ReadInt32("ChangedAbilityID", idx);
                    packet.ReadInt32("CooldownRemaining", idx);
                    packet.ReadInt32("LockdownRemaining", idx);
                    break;
                case 2:
                    packet.ReadInt32("BroadcastTextID", idx);
                    break;
            }
        }
        public static void ReadPetBattleEffectTarget612(Packet packet, params object[] idx)
        {
            var type = packet.ReadBits("Type", 3, idx); // enum PetBattleEffectTargetEx

            packet.ResetBitReader();

            packet.ReadByte("Petx", idx);

            switch (type)
            {
                case 5:
                    packet.ReadInt32("AuraInstanceID", idx);
                    packet.ReadInt32("AuraAbilityID", idx);
                    packet.ReadInt32("RoundsRemaining", idx);
                    packet.ReadInt32("CurrentRound", idx);
                    break;
                case 6:
                    packet.ReadInt32("StateID", idx);
                    packet.ReadInt32("StateValue", idx);
                    break;
                case 4:
                    packet.ReadInt32("Health", idx);
                    break;
                case 1:
                    packet.ReadInt32("NewStatValue", idx);
                    break;
                case 0:
                    packet.ReadInt32("TriggerAbilityID", idx);
                    break;
                case 7:
                    packet.ReadInt32("ChangedAbilityID", idx);
                    packet.ReadInt32("CooldownRemaining", idx);
                    packet.ReadInt32("LockdownRemaining", idx);
                    break;
                case 2:
                    packet.ReadInt32("BroadcastTextID", idx);
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
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
                    ReadPetBattleEffectTarget612(packet, idx, "Targets", i);
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                    ReadPetBattleEffectTarget610(packet, idx, "Targets", i);
                else
                    ReadPetBattleEffectTarget60x(packet, idx, "Targets", i);
            }
        }

        public static void ReadPetBattleRoundResult(Packet packet, params object[] idx)
        {
            packet.ReadInt32("CurRound", idx);
            packet.ReadSByte("NextPetBattleState", idx);

            var effectsCount = packet.ReadInt32("EffectsCount", idx);

            for (var i = 0; i < 2; ++i)
            {
                packet.ReadByte("NextInputFlags", idx, i);
                packet.ReadSByte("NextTrapStatus", idx, i);
                packet.ReadUInt16("RoundTimeSecs", idx, i);
            }

            var cooldownsCount = packet.ReadInt32("CooldownsCount", idx);

            for (var i = 0; i < effectsCount; ++i)
                ReadPetBattleEffect(packet, idx, "Effects", i);

            for (var i = 0; i < cooldownsCount; ++i)
                ReadPetBattleActiveAbility(packet, idx, "Cooldowns", i);

            packet.ResetBitReader();

            var petXDiedCount = packet.ReadBits("PetXDied", 3, idx);

            packet.ResetBitReader();

            for (var i = 0; i < petXDiedCount; ++i)
                packet.ReadSByte("PetXDied", idx, i);
        }

        public static void ReadPetBattleFinalPet(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt16("Level", idx);
            packet.ReadUInt16("Xp", idx);
            packet.ReadInt32("Health", idx);
            packet.ReadInt32("MaxHealth", idx);
            packet.ReadUInt16("InitialLevel", idx);
            packet.ReadByte("Pboid", idx);

            packet.ResetBitReader();

            packet.ReadBit("Captured | Caged | SeenAction", idx);
            packet.ReadBit("Captured | Caged | SeenAction", idx);
            packet.ReadBit("Captured | Caged | SeenAction", idx);
            packet.ReadBit("AwardedXP", idx);

            packet.ResetBitReader();
        }

        public static void ReadPetBattleFinalRound(Packet packet, params object[] idx)
        {
            packet.ReadBit("Abandoned | PvpBattle", idx);
            packet.ReadBit("Abandoned | PvpBattle", idx);

            for (var i = 0; i < 2; ++i) // Winners
                packet.ReadBit("Winner", idx, i);

            packet.ResetBitReader();

            for (var i = 0; i < 2; ++i) // Winners
                packet.ReadInt32<UnitId>("NpcCreatureID", idx, i);

            var petsCount = packet.ReadInt32("PetsCount", idx);

            for (var i = 0; i < petsCount; ++i)
                ReadPetBattleFinalPet(packet, idx, "Pets", i);
        }

        [Parser(Opcode.SMSG_PET_BATTLE_INITIAL_UPDATE)]
        public static void HandlePetBattleInitialUpdate(Packet packet)
        {
            ReadPetBattleFullUpdate(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_FIRST_ROUND)]
        [Parser(Opcode.SMSG_PET_BATTLE_ROUND_RESULT)]
        [Parser(Opcode.SMSG_PET_BATTLE_REPLACEMENTS_MADE)]
        public static void HandlePetBattleRound(Packet packet)
        {
            ReadPetBattleRoundResult(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_FINAL_ROUND)]
        public static void HandleSceneObjectPetBattleFinalRound(Packet packet)
        {
            ReadPetBattleFinalRound(packet, "MsgData");
        }

        public static void ReadPetBattleLocations(Packet packet, params object[] idx)
        {
            packet.ReadInt32("LocationResult", idx);
            packet.ReadVector3("BattleOrigin", idx);
            packet.ReadSingle("BattleFacing", idx);

            for (var i = 0; i < 2; ++i)
                packet.ReadVector3("PlayerPositions", idx, i);
        }

        [Parser(Opcode.SMSG_PET_BATTLE_FINALIZE_LOCATION)]
        public static void HandlePetBattleFinalizeLocation(Packet packet)
        {
            ReadPetBattleLocations(packet, "Location");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_PVP_CHALLENGE)]
        public static void HandlePetBattlePVPChallenge(Packet packet)
        {
            packet.ReadPackedGuid128("ChallengerGUID");
            ReadPetBattleLocations(packet, "Location");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REQUEST_WILD)]
        public static void HandlePetBattleRequestWild(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            ReadPetBattleLocations(packet, "Location");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REQUEST_PVP)]
        public static void HandlePetBattleRequestPVP(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            ReadPetBattleLocations(packet, "OpponentCharacterID");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_REQUEST_FAILED)]
        public static void HandlePetBattleRequestFailed(Packet packet)
        {
            packet.ReadByte("Reason"); // TODO: enum
        }

        [Parser(Opcode.SMSG_PET_BATTLE_SLOT_UPDATES)]
        public static void HandlePetBattleSlotUpdates(Packet packet)
        {
            var petBattleSlotCount = packet.ReadInt32("PetBattleSlotCount");
            for (int i = 0; i < petBattleSlotCount; i++)
                ReadClientPetBattleSlot(packet, i, "PetBattleSlot");

            packet.ResetBitReader();

            packet.ReadBit("AutoSlotted");      // unconfirmed order
            packet.ReadBit("NewSlotUnlocked");  // unconfirmed order
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REPLACE_FRONT_PET)]
        public static void HandlePetBattleReplaceFrontPet(Packet packet)
        {
            packet.ReadSByte("FrontPet");
        }

        public static void ReadPetBattleInput(Packet packet, params object[] idx)
        {
            packet.ReadByte("MoveType");
            packet.ReadSByte("NewFrontPet");
            packet.ReadByte("DebugFlags");
            packet.ReadByte("BattleInterrupted");

            packet.ReadInt32("AbilityID");
            packet.ReadInt32("Round");

            packet.ResetBitReader();

            packet.ReadBit("IgnoreAbandonPenalty");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_INPUT)]
        public static void HandlePetBattleInput(Packet packet)
        {
            ReadPetBattleInput(packet, "PetBattleInput");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_QUEUE_STATUS)]
        public static void HandlePetBattleQueueStatus(Packet packet)
        {
            packet.ReadInt32("Status");

            var slotResultCount = packet.ReadInt32("SlotResultCount");

            LfgHandler.ReadCliRideTicket(packet, "RideTicket");

            for (int i = 0; i < slotResultCount; i++)
                packet.ReadInt32("SlotResult", i);

            var bit64 = packet.ReadBit("HasClientWaitTime");
            var bit56 = packet.ReadBit("HasAverageWaitTime");

            if (bit64)
                packet.ReadInt32("ClientWaitTime");

            if (bit56)
                packet.ReadInt32("AverageWaitTime");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_QUEUE_PROPOSE_MATCH_RESULT)]
        public static void HandlePetBattleQueueProposeMatchResult(Packet packet)
        {
            packet.ReadBit("Accepted");
        }

        [Parser(Opcode.CMSG_LEAVE_PET_BATTLE_QUEUE)]
        public static void HandleLeavePetBattleQueue(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REQUEST_UPDATE)]
        public static void HandlePetBattleRequestUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadBit("Canceled");
        }
    }
}
