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
        [Parser(Opcode.CMSG_PET_BATTLE_QUIT_NOTIFY)]
        public static void HandleBattlePetZero(Packet packet)
        {
        }

        public static void ReadClientBattlePet(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID", idx);

            packet.Translator.ReadInt32("SpeciesID", idx);
            packet.Translator.ReadInt32("DisplayID", idx);
            packet.Translator.ReadInt32("CollarID", idx);

            packet.Translator.ReadInt16("BreedID", idx);
            packet.Translator.ReadInt16("Level", idx);
            packet.Translator.ReadInt16("Xp", idx);
            packet.Translator.ReadInt16("BattlePetDBFlags", idx);

            packet.Translator.ReadInt32("Power", idx);
            packet.Translator.ReadInt32("Health", idx);
            packet.Translator.ReadInt32("MaxHealth", idx);
            packet.Translator.ReadInt32("Speed", idx);

            packet.Translator.ReadByte("BreedQuality", idx);

            packet.Translator.ResetBitReader();

            var customNameLength = packet.Translator.ReadBits(7);
            var hasOwnerInfo = packet.Translator.ReadBit("HasOwnerInfo", idx);
            packet.Translator.ReadBit("NoRename", idx);

            if (hasOwnerInfo)
                ReadClientBattlePetOwnerInfo(packet, "OwnerInfo", idx);

            packet.Translator.ReadWoWString("CustomName", customNameLength, idx);
        }

        public static void ReadClientPetBattleSlot(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID", idx);

            packet.Translator.ReadInt32("CollarID", idx);
            packet.Translator.ReadByte("SlotIndex", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Locked", idx);
        }

        public static void ReadClientBattlePetOwnerInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("Guid", idx);
            packet.Translator.ReadUInt32("PlayerVirtualRealm", idx);
            packet.Translator.ReadUInt32("PlayerNativeRealm", idx);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.Translator.ReadInt16("TrapLevel");

            var slotsCount = packet.Translator.ReadInt32("SlotsCount");
            var petsCount = packet.Translator.ReadInt32("PetsCount");

            for (var i = 0; i < slotsCount; i++)
                ReadClientPetBattleSlot(packet, i);

            for (var i = 0; i < petsCount; i++)
                ReadClientBattlePet(packet, i);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("HasJournalLock");
        }

        [Parser(Opcode.CMSG_QUERY_BATTLE_PET_NAME)]
        public static void HandleBattlePetQuery(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BattlePetID");
            packet.Translator.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_QUERY_BATTLE_PET_NAME_RESPONSE)]
        public static void HandleBattlePetQueryResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BattlePetID");

            packet.Translator.ReadInt32("CreatureID");
            packet.Translator.ReadTime("Timestamp");

            packet.Translator.ResetBitReader();
            var bit40 = packet.Translator.ReadBit("Allow");
            if (!bit40)
                return;

            var bits49 = packet.Translator.ReadBits(8);
            packet.Translator.ReadBit("HasDeclined");

            var bits97 = new uint[5];
            for (int i = 0; i < 5; i++)
                bits97[i] = packet.Translator.ReadBits(7);

            for (int i = 0; i < 5; i++)
                packet.Translator.ReadWoWString("DeclinedNames", bits97[i]);

            packet.Translator.ReadWoWString("Name", bits49);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET)]
        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET_CHEAT)]
        [Parser(Opcode.CMSG_BATTLE_PET_SUMMON)]
        [Parser(Opcode.CMSG_CAGE_BATTLE_PET)]
        [Parser(Opcode.SMSG_BATTLE_PET_DELETED)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID");

            packet.Translator.ResetBitReader();

            var bits342 = packet.Translator.ReadBits(7);
            var bit341 = packet.Translator.ReadBit("HasDeclinedNames");

            packet.Translator.ReadWoWString("Name", bits342);

            if (bit341)
            {
                var bits97 = new uint[5];
                for (int i = 0; i < 5; i++)
                    bits97[i] = packet.Translator.ReadBits(7);

                for (int i = 0; i < 5; i++)
                    packet.Translator.ReadWoWString("DeclinedNames", bits97[i]);
            }
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT)]
        public static void HandleBattlePetSetBattleSlot(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID");
            packet.Translator.ReadByte("SlotIndex");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_FLAGS)]
        public static void HandleBattlePetSetFlags(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID");
            packet.Translator.ReadInt32("Flags");
            packet.Translator.ReadBits("ControlType", 2);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var petsCount = packet.Translator.ReadInt32("PetsCount");

            for (var i = 0; i < petsCount; ++i)
                ReadClientBattlePet(packet, i);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("AddedPet");
        }

        public static void ReadPetBattlePetUpdate(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID", idx);

            packet.Translator.ReadInt32("SpeciesID", idx);
            packet.Translator.ReadInt32("DisplayID", idx);
            packet.Translator.ReadInt32("CollarID", idx);

            packet.Translator.ReadInt16("Level", idx);
            packet.Translator.ReadInt16("Xp", idx);

            packet.Translator.ReadInt32("CurHealth", idx);
            packet.Translator.ReadInt32("MaxHealth", idx);
            packet.Translator.ReadInt32("Power", idx);
            packet.Translator.ReadInt32("Speed", idx);
            packet.Translator.ReadInt32("NpcTeamMemberID", idx);

            packet.Translator.ReadInt16("BreedQuality", idx);
            packet.Translator.ReadInt16("StatusFlags", idx);

            packet.Translator.ReadByte("Slot", idx);

            var abilitiesCount = packet.Translator.ReadInt32("AbilitiesCount", idx);
            var aurasCount = packet.Translator.ReadInt32("AurasCount", idx);
            var statesCount = packet.Translator.ReadInt32("StatesCount", idx);

            for (var i = 0; i < abilitiesCount; ++i)
                ReadPetBattleActiveAbility(packet, idx, "Abilities", i);

            for (var i = 0; i < aurasCount; ++i)
                ReadPetBattleActiveAura(packet, idx, "Auras", i);

            for (var i = 0; i < statesCount; ++i)
                ReadPetBattleActiveState(packet, idx, "States", i);

            packet.Translator.ResetBitReader();

            var bits57 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("CustomName", bits57, idx);
        }

        public static void ReadPetBattlePlayerUpdate(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("CharacterID", idx);

            packet.Translator.ReadInt32("TrapAbilityID", idx);
            packet.Translator.ReadInt32("TrapStatus", idx);

            packet.Translator.ReadInt16("RoundTimeSecs", idx);

            packet.Translator.ReadSByte("FrontPet", idx);
            packet.Translator.ReadByte("InputFlags", idx);

            packet.Translator.ResetBitReader();

            var petsCount = packet.Translator.ReadBits("PetsCount", 2, idx);

            for (var i = 0; i < petsCount; ++i)
                ReadPetBattlePetUpdate(packet, idx, "Pets", i);
        }

        public static void ReadPetBattleActiveState(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("StateID", idx);
            packet.Translator.ReadInt32("StateValue", idx);
        }

        public static void ReadPetBattleActiveAbility(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("AbilityID", idx);
            packet.Translator.ReadInt16("CooldownRemaining", idx);
            packet.Translator.ReadInt16("LockdownRemaining", idx);
            packet.Translator.ReadByte("AbilityIndex", idx);
            packet.Translator.ReadByte("Pboid", idx);
        }

        public static void ReadPetBattleActiveAura(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("AbilityID", idx);
            packet.Translator.ReadInt32("InstanceID", idx);
            packet.Translator.ReadInt32("RoundsRemaining", idx);
            packet.Translator.ReadInt32("CurrentRound", idx);
            packet.Translator.ReadByte("CasterPBOID", idx);
        }

        public static void ReadPetBattleEnviroUpdate(Packet packet, params object[] idx)
        {
            var aurasCount = packet.Translator.ReadInt32("AurasCount", idx);
            var statesCount = packet.Translator.ReadInt32("StatesCount", idx);

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

            packet.Translator.ReadInt16("WaitingForFrontPetsMaxSecs", idx);
            packet.Translator.ReadInt16("PvpMaxRoundTime", idx);

            packet.Translator.ReadInt32("CurRound", idx);
            packet.Translator.ReadInt32("NpcCreatureID", idx);
            packet.Translator.ReadInt32("NpcDisplayID", idx);

            packet.Translator.ReadByte("CurPetBattleState", idx);
            packet.Translator.ReadByte("ForfeitPenalty", idx);

            packet.Translator.ReadPackedGuid128("InitialWildPetGUID", idx);

            packet.Translator.ReadBit("IsPVP", idx);
            packet.Translator.ReadBit("CanAwardXP", idx);
        }

        private static readonly int[] _petBattleEffectTargets602 = {5, 3, 2, 4, 7, 1, 0, 6};
        private static readonly int[] _petBattleEffectTargets610 = {0, 4, 7, 1, 3, 5, 2, 6};
        private static readonly int[] _petBattleEffectTargets612 = {5, 4, 7, 0, 3, 1, 2, 6};
        private static readonly int[] _petBattleEffectTargets620 = {0, 2, 7, 6, 4, 1, 3, 5};
        private static readonly int[] _petBattleEffectTargets623 = {0, 7, 1, 4, 3, 6, 5, 2};
        private static readonly int[] _petBattleEffectTargets624 = {0, 1, 2, 3, 4, 5, 6, 7};

        public static void ReadPetBattleEffectTarget(Packet packet, int[] targetTypeValueMap , params object[] idx)
        {
            var type = packet.Translator.ReadBits("Type", 3, idx); // enum PetBattleEffectTargetEx

            packet.Translator.ResetBitReader();

            packet.Translator.ReadByte("Petx", idx);

            switch (targetTypeValueMap[type])
            {
                case 1:
                    packet.Translator.ReadInt32("AuraInstanceID", idx);
                    packet.Translator.ReadInt32("AuraAbilityID", idx);
                    packet.Translator.ReadInt32("RoundsRemaining", idx);
                    packet.Translator.ReadInt32("CurrentRound", idx);
                    break;
                case 2:
                    packet.Translator.ReadInt32("StateID", idx);
                    packet.Translator.ReadInt32("StateValue", idx);
                    break;
                case 3:
                    packet.Translator.ReadInt32("Health", idx);
                    break;
                case 4:
                    packet.Translator.ReadInt32("NewStatValue", idx);
                    break;
                case 5:
                    packet.Translator.ReadInt32("TriggerAbilityID", idx);
                    break;
                case 6:
                    packet.Translator.ReadInt32("ChangedAbilityID", idx);
                    packet.Translator.ReadInt32("CooldownRemaining", idx);
                    packet.Translator.ReadInt32("LockdownRemaining", idx);
                    break;
                case 7:
                    packet.Translator.ReadInt32("BroadcastTextID", idx);
                    break;
            }
        }

        public static void ReadPetBattleEffect(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("AbilityEffectID", idx);
            packet.Translator.ReadUInt16("Flags", idx);
            packet.Translator.ReadUInt16("SourceAuraInstanceID", idx);
            packet.Translator.ReadUInt16("TurnInstanceID", idx);
            packet.Translator.ReadSByte("PetBattleEffectType", idx);
            packet.Translator.ReadByte("CasterPBOID", idx);
            packet.Translator.ReadByte("StackDepth", idx);

            var targetsCount = packet.Translator.ReadInt32("TargetsCount", idx);

            var targetTypeValueMap = _petBattleEffectTargets624;
            if (!ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_3_20886))
                    targetTypeValueMap = _petBattleEffectTargets623;
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                    targetTypeValueMap = _petBattleEffectTargets620;
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
                    targetTypeValueMap = _petBattleEffectTargets612;
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                    targetTypeValueMap = _petBattleEffectTargets610;
                else
                    targetTypeValueMap = _petBattleEffectTargets602;
            }

            for (var i = 0; i < targetsCount; ++i)
                ReadPetBattleEffectTarget(packet, targetTypeValueMap, i);
        }

        public static void ReadPetBattleRoundResult(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("CurRound", idx);
            packet.Translator.ReadSByte("NextPetBattleState", idx);

            var effectsCount = packet.Translator.ReadInt32("EffectsCount", idx);

            for (var i = 0; i < 2; ++i)
            {
                packet.Translator.ReadByte("NextInputFlags", idx, i);
                packet.Translator.ReadSByte("NextTrapStatus", idx, i);
                packet.Translator.ReadUInt16("RoundTimeSecs", idx, i);
            }

            var cooldownsCount = packet.Translator.ReadInt32("CooldownsCount", idx);

            for (var i = 0; i < effectsCount; ++i)
                ReadPetBattleEffect(packet, idx, "Effects", i);

            for (var i = 0; i < cooldownsCount; ++i)
                ReadPetBattleActiveAbility(packet, idx, "Cooldowns", i);

            packet.Translator.ResetBitReader();

            var petXDiedCount = packet.Translator.ReadBits("PetXDied", 3, idx);

            packet.Translator.ResetBitReader();

            for (var i = 0; i < petXDiedCount; ++i)
                packet.Translator.ReadSByte("PetXDied", idx, i);
        }

        public static void ReadPetBattleFinalPet(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("Guid", idx);
            packet.Translator.ReadUInt16("Level", idx);
            packet.Translator.ReadUInt16("Xp", idx);
            packet.Translator.ReadInt32("Health", idx);
            packet.Translator.ReadInt32("MaxHealth", idx);
            packet.Translator.ReadUInt16("InitialLevel", idx);
            packet.Translator.ReadByte("Pboid", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Captured | Caged | SeenAction", idx);
            packet.Translator.ReadBit("Captured | Caged | SeenAction", idx);
            packet.Translator.ReadBit("Captured | Caged | SeenAction", idx);
            packet.Translator.ReadBit("AwardedXP", idx);

            packet.Translator.ResetBitReader();
        }

        public static void ReadPetBattleFinalRound(Packet packet, params object[] idx)
        {
            packet.Translator.ReadBit("Abandoned | PvpBattle", idx);
            packet.Translator.ReadBit("Abandoned | PvpBattle", idx);

            for (var i = 0; i < 2; ++i) // Winners
                packet.Translator.ReadBit("Winner", idx, i);

            packet.Translator.ResetBitReader();

            for (var i = 0; i < 2; ++i) // Winners
                packet.Translator.ReadInt32<UnitId>("NpcCreatureID", idx, i);

            var petsCount = packet.Translator.ReadInt32("PetsCount", idx);

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
            packet.Translator.ReadInt32("LocationResult", idx);
            packet.Translator.ReadVector3("BattleOrigin", idx);
            packet.Translator.ReadSingle("BattleFacing", idx);

            for (var i = 0; i < 2; ++i)
                packet.Translator.ReadVector3("PlayerPositions", idx, i);
        }

        [Parser(Opcode.SMSG_PET_BATTLE_FINALIZE_LOCATION)]
        public static void HandlePetBattleFinalizeLocation(Packet packet)
        {
            ReadPetBattleLocations(packet, "Location");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_PVP_CHALLENGE)]
        public static void HandlePetBattlePVPChallenge(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ChallengerGUID");
            ReadPetBattleLocations(packet, "Location");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REQUEST_WILD)]
        public static void HandlePetBattleRequestWild(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            ReadPetBattleLocations(packet, "Location");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REQUEST_PVP)]
        public static void HandlePetBattleRequestPVP(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            ReadPetBattleLocations(packet, "OpponentCharacterID");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_REQUEST_FAILED)]
        public static void HandlePetBattleRequestFailed(Packet packet)
        {
            packet.Translator.ReadByte("Reason"); // TODO: enum
        }

        [Parser(Opcode.SMSG_PET_BATTLE_SLOT_UPDATES)]
        public static void HandlePetBattleSlotUpdates(Packet packet)
        {
            var petBattleSlotCount = packet.Translator.ReadInt32("PetBattleSlotCount");
            for (int i = 0; i < petBattleSlotCount; i++)
                ReadClientPetBattleSlot(packet, i, "PetBattleSlot");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("NewSlotUnlocked");
            packet.Translator.ReadBit("AutoSlotted");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REPLACE_FRONT_PET)]
        public static void HandlePetBattleReplaceFrontPet(Packet packet)
        {
            packet.Translator.ReadSByte("FrontPet");
        }

        public static void ReadPetBattleInput(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("MoveType");
            packet.Translator.ReadSByte("NewFrontPet");
            packet.Translator.ReadByte("DebugFlags");
            packet.Translator.ReadByte("BattleInterrupted");

            packet.Translator.ReadInt32("AbilityID");
            packet.Translator.ReadInt32("Round");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("IgnoreAbandonPenalty");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_INPUT)]
        public static void HandlePetBattleInput(Packet packet)
        {
            ReadPetBattleInput(packet, "PetBattleInput");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_QUEUE_STATUS)]
        public static void HandlePetBattleQueueStatus(Packet packet)
        {
            packet.Translator.ReadInt32("Status");

            var slotResultCount = packet.Translator.ReadInt32("SlotResultCount");

            LfgHandler.ReadCliRideTicket(packet, "RideTicket");

            for (int i = 0; i < slotResultCount; i++)
                packet.Translator.ReadInt32("SlotResult", i);

            var bit64 = packet.Translator.ReadBit("HasClientWaitTime");
            var bit56 = packet.Translator.ReadBit("HasAverageWaitTime");

            if (bit64)
                packet.Translator.ReadInt32("ClientWaitTime");

            if (bit56)
                packet.Translator.ReadInt32("AverageWaitTime");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_QUEUE_PROPOSE_MATCH_RESULT)]
        public static void HandlePetBattleQueueProposeMatchResult(Packet packet)
        {
            packet.Translator.ReadBit("Accepted");
        }

        [Parser(Opcode.CMSG_LEAVE_PET_BATTLE_QUEUE)]
        public static void HandleLeavePetBattleQueue(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket");
        }

        [Parser(Opcode.CMSG_PET_BATTLE_REQUEST_UPDATE)]
        public static void HandlePetBattleRequestUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadBit("Canceled");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_ERROR)]
        public static void HandleBattlePetError(Packet packet)
        {
            packet.Translator.ReadBits("Result", 4);
            packet.Translator.ReadInt32("CreatureID");
        }
    }
}
