using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class GroupHandler
    {
        public static void ReadPartyMemberPhase(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("PhaseFlags", idx);
            packet.ReadUInt16("Id", idx);
        }

        public static void ReadPhaseInfos(Packet packet, params object[] index)
        {
            packet.ReadInt32("PhaseShiftFlags", index);
            var int4 = packet.ReadInt32("PhaseCount", index);
            packet.ReadPackedGuid128("PersonalGUID", index);
            for (int i = 0; i < int4; i++)
            {
                ReadPartyMemberPhase(packet, "PartyMemberPhase", index, i);
            }
        }

        public static void ReadAuraInfos(Packet packet, params object[] index)
        {
            packet.ReadInt32<SpellId>("Aura", index);
            packet.ReadUInt16("Flags", index);
            packet.ReadUInt32("ActiveFlags", index);
            var byte3 = packet.ReadUInt32("PointsCount", index);

            for (int j = 0; j < byte3; j++)
                packet.ReadSingle("Points", index, j);
        }

        public static void ReadPetInfos(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("PetGuid", index);
            packet.ReadInt32("ModelId", index);
            packet.ReadInt32("CurrentHealth", index);
            packet.ReadInt32("MaxHealth", index);

            var petAuraCount = packet.ReadUInt32("PetAuraCount", index);
            for (int i = 0; i < petAuraCount; i++)
                ReadAuraInfos(packet, index, i);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("PetName", len, index);
        }

        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            // CONFIRM IN SNIFFS
            var nameLength = packet.ReadBits(9);
            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.SMSG_GROUP_NEW_LEADER)]
        public static void HandleGroupNewLeader(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            packet.ReadPackedGuid128("Sender");
            packet.ReadVector2("Position");
        }

        [Parser(Opcode.SMSG_PARTY_COMMAND_RESULT)]
        public static void HandlePartyCommandResult(Packet packet)
        {
            var nameLength = packet.ReadBits(9);
            packet.ReadBitsE<PartyCommand>("Command", 4);
            packet.ReadBitsE<PartyResult>("Result", 6);
            packet.ReadUInt32("ResultData");
            packet.ReadPackedGuid128("ResultGUID");
            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.SMSG_PARTY_INVITE)]
        public static void HandlePartyInvite(Packet packet)
        {
            packet.ReadBit("CanAccept");
            packet.ReadBit("MightCRZYou");
            packet.ReadBit("IsXRealm");
            packet.ReadBit("MustBeBNetFriend");
            packet.ReadBit("AllowMultipleRoles");
            packet.ReadBit("QuestSessionActive");
            var len = packet.ReadBits(6);
            packet.ReadBit("Unused440");

            packet.ResetBitReader();

            // VirtualRealmInfo
            packet.ReadInt32("InviterVirtualRealmAddress");

            // VirtualRealmNameInfo
            packet.ReadBit("IsLocal");
            packet.ReadBit("IsInternalRealm");
            var bits2 = packet.ReadBits(8);
            var bits258 = packet.ReadBits(8);
            packet.ReadWoWString("InviterRealmNameActual", bits2);
            packet.ReadWoWString("InviterRealmNameNormalized", bits258);

            packet.ReadPackedGuid128("InviterGuid");
            packet.ReadPackedGuid128("InviterBNetAccountID");
            packet.ReadInt16("Unk1");
            packet.ReadByte("ProposedRoles");
            var lfgSlots = packet.ReadInt32();
            packet.ReadInt32("LfgCompletedMask");
            packet.ReadWoWString("InviterName", len);
            for (int i = 0; i < lfgSlots; i++)
                packet.ReadInt32("LfgSlots", i);
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_FULL_STATE)]
        public static void HandlePartyMemberFullState(Packet packet)
        {
            packet.ReadBit("ForEnemy");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("PartyType", i);

            packet.ReadUInt16E<GroupMemberStatusFlag>("Status");
            packet.ReadByte("PowerType");
            packet.ReadUInt16("PowerDisplayID");
            packet.ReadInt32("CurrentHealth");
            packet.ReadInt32("MaxHealth");
            packet.ReadUInt16("CurrentPower");
            packet.ReadUInt16("MaxPower");
            packet.ReadUInt16("Level");
            packet.ReadUInt16("SpecID");
            packet.ReadUInt16("ZoneID");
            packet.ReadUInt16("WmoGroupID");
            packet.ReadUInt32("WmoDoodadPlacementID");

            packet.ReadInt16("PositionX");
            packet.ReadInt16("PositionY");
            packet.ReadInt16("PositionZ");

            packet.ReadInt32("VehicleSeat");
            var auraCount = packet.ReadInt32("AuraCount");

            ReadPhaseInfos(packet, "Phase");

            packet.ReadUInt32("ContentTuningConditionMask", "CTROptions");
            packet.ReadInt32("Unused901", "CTROptions");
            packet.ReadUInt32("ExpansionLevelMask", "CTROptions");

            for (int i = 0; i < auraCount; i++)
                ReadAuraInfos(packet, "Aura", i);

            packet.ResetBitReader();
            var hasPet = packet.ReadBit("HasPet");

            if (hasPet) // Pet
                ReadPetInfos(packet, "Pet");

            packet.ReadPackedGuid128("MemberGuid");
        }

        [Parser(Opcode.SMSG_PARTY_UPDATE)]
        public static void HandlePartyUpdate(Packet packet)
        {
            packet.ReadUInt16("PartyFlags");
            packet.ReadByte("PartyIndex");
            packet.ReadByte("PartyType");

            packet.ReadInt32("MyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadInt32("SequenceNum");
            packet.ReadPackedGuid128("LeaderGUID");
            packet.ReadByte("LeaderFactionGroup");

            var playerCount = packet.ReadUInt32("PlayerListCount");
            var hasLFG = packet.ReadBit("HasLfgInfo");
            var hasLootSettings = packet.ReadBit("HasLootSettings");
            var hasDifficultySettings = packet.ReadBit("HasDifficultySettings");

            for (var i = 0; i < playerCount; i++)
            {
                packet.ResetBitReader();
                var playerNameLength = packet.ReadBits(6);
                var voiceStateLength = packet.ReadBits(6);
                packet.ReadBit("Connected", i);
                packet.ReadBit("VoiceChatSilenced", i);
                packet.ReadBit("FromSocialQueue", i);
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadByte("Subgroup", i);
                packet.ReadByte("Flags", i);
                packet.ReadByte("RolesAssigned", i);
                packet.ReadByteE<Class>("Class", i);
                packet.ReadByte("FactionGroup", i);
                packet.ReadWoWString("Name", playerNameLength, i);
                packet.ReadDynamicString("VoiceStateID", voiceStateLength, i);
            }

            packet.ResetBitReader();

            if (hasLootSettings)
            {
                packet.ReadByte("Method", "PartyLootSettings");
                packet.ReadPackedGuid128("LootMaster", "PartyLootSettings");
                packet.ReadByte("Threshold", "PartyLootSettings");
            }

            if (hasDifficultySettings)
            {
                packet.ReadUInt32("DungeonDifficultyID");
                packet.ReadUInt32("RaidDifficultyID");
                packet.ReadUInt32("LegacyRaidDifficultyID");
            }

            if (hasLFG)
            {
                packet.ResetBitReader();

                packet.ReadByte("MyFlags");
                packet.ReadUInt32("Slot");
                packet.ReadUInt32("MyRandomSlot");
                packet.ReadByte("MyPartialClear");
                packet.ReadSingle("MyGearDiff");
                packet.ReadByte("MyStrangerCount");
                packet.ReadByte("MyKickVoteCount");
                packet.ReadByte("BootCount");
                packet.ReadBit("Aborted");
                packet.ReadBit("MyFirstReward");
            }
        }

        [Parser(Opcode.SMSG_RAID_GROUP_ONLY)]
        public static void HandleRaidGroupOnly(Packet packet)
        {
            packet.ReadInt32("Delay");
            packet.ReadUInt32("Reason");
        }

        [Parser(Opcode.SMSG_RAID_MARKERS_CHANGED)]
        public static void HandleRaidMarkersChanged(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadInt32("ActiveMarkers");

            var count = packet.ReadBits(4);
            for (int i = 0; i < count; i++)
            {
                packet.ReadPackedGuid128("TransportGUID");
                packet.ReadInt32("MapID");
                packet.ReadVector3("Position");
            }
        }

        [Parser(Opcode.SMSG_READY_CHECK_COMPLETED)]
        public static void HandleReadyCheckCompleted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
        }

        [Parser(Opcode.SMSG_READY_CHECK_RESPONSE)]
        public static void HandleReadyCheckResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadPackedGuid128("Player");
            packet.ReadBit("IsReady");
        }

        [Parser(Opcode.SMSG_READY_CHECK_STARTED)]
        public static void HandleReadyCheckStarted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadPackedGuid128("InitiatorGUID");
            packet.ReadInt64("Duration");
        }

        [Parser(Opcode.SMSG_ROLE_CHANGED_INFORM)]
        public static void HandleRoleChangedInform(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("From");
            packet.ReadPackedGuid128("ChangedUnit");
            packet.ReadInt32E<LfgRoleFlag>("OldRole");
            packet.ReadInt32E<LfgRoleFlag>("NewRole");
        }

        [Parser(Opcode.SMSG_ROLE_POLL_INFORM)]
        public static void HandleRolePollInform(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("From");
        }

        [Parser(Opcode.SMSG_SEND_RAID_TARGET_UPDATE_ALL)]
        public static void HandleSendRaidTargetUpdateAll(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            var raidTargetSymbolCount = packet.ReadInt32("RaidTargetSymbolCount");
            for (int i = 0; i < raidTargetSymbolCount; i++)
            {
                packet.ReadPackedGuid128("Target", i);
                packet.ReadByte("Symbol", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_RAID_TARGET_UPDATE_SINGLE)]
        public static void HandleSendRaidTargetUpdateSingle(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByte("Symbol");
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("ChangedBy");
        }

        [Parser(Opcode.CMSG_CHANGE_SUB_GROUP)]
        public static void HandleChangeSubGroup(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadByte("NewSubGroup");

            var hasPartyIndex = packet.ReadBit();
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_REQUEST_RAID_INFO)]
        [Parser(Opcode.SMSG_GROUP_DESTROYED)]
        [Parser(Opcode.SMSG_GROUP_UNINVITE)]
        public static void HandleGroupNull(Packet packet)
        {
        }
    }
}
