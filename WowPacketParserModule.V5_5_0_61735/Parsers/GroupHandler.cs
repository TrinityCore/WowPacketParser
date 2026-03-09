
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class GroupHandler
    {
        public static void ReadAuraInfos(Packet packet, params object[] index)
        {
            packet.ReadInt32<SpellId>("Aura", index);
            packet.ReadUInt16("Flags", index);
            packet.ReadUInt32("ActiveFlags", index);
            var byte3 = packet.ReadUInt32("PointsCount", index);

            for (int j = 0; j < byte3; j++)
                packet.ReadSingle("Points", index, j);
        }

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

        [Parser(Opcode.SMSG_CHANGE_PLAYER_DIFFICULTY_RESULT)]
        public static void HandleChangePlayerDifficultyResult(Packet packet)
        {
            var result = packet.ReadBitsE<DifficultyChangeResult>("Result", 4);

            switch (result)
            {
                case DifficultyChangeResult.Cooldown:
                    packet.ReadBit("InCombat");
                    packet.ReadInt64("Cooldown");
                    break;
                case DifficultyChangeResult.LoadingScreenEnable:
                    packet.ReadBit("Unused");
                    packet.ReadInt64("NextDifficultyChangeTime");
                    break;
                case DifficultyChangeResult.MapDifficultyConditionNotSatisfied:
                    packet.ReadInt32E<MapDifficulty>("MapDifficultyID");
                    break;
                case DifficultyChangeResult.PlayerAlreadyLockedToDifferentInstance:
                    packet.ReadPackedGuid128("PlayerGUID");
                    break;
                case DifficultyChangeResult.Success:
                    packet.ReadInt32<MapId>("MapID");
                    packet.ReadInt32<DifficultyId>("DifficultyID");
                    break;
                default:
                    break;
            }
        }

        [Parser(Opcode.SMSG_ROLE_CHANGED_INFORM)]
        public static void HandleRoleChangedInform(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("From");
            packet.ReadPackedGuid128("ChangedUnit");
            packet.ReadByteE<LfgRoleFlag>("OldRole");
            packet.ReadByteE<LfgRoleFlag>("NewRole");
        }

        [Parser(Opcode.SMSG_ROLE_POLL_INFORM)]
        public static void HandleRolePollInform(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("From");
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
            packet.ReadBit("IsCrossFaction");

            SessionHandler.ReadVirtualRealmInfo(packet, "VirtualRealm");

            packet.ReadPackedGuid128("InviterGuid");
            packet.ReadPackedGuid128("InviterBNetAccountID");
            packet.ReadInt16("InviterCfgRealmID");
            packet.ReadByte("ProposedRoles");
            var lfgSlots = packet.ReadInt32();
            packet.ReadInt32("LfgCompletedMask");
            packet.ReadWoWString("InviterName", len);
            for (int i = 0; i < lfgSlots; i++)
                packet.ReadInt32("LfgSlots", i);
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
            packet.ReadInt32("PingRestriction");

            var playerCount = packet.ReadUInt32("PlayerListCount");
            var hasChallengeMode = ClientVersion.AddedInVersion(ClientVersionBuild.V5_5_1_63311) && packet.ReadBit("HasChallengeMode");
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
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_5_1_63311))
                {
                    packet.ResetBitReader();
                    packet.ReadPackedGuid128("BnetAccountGUID", "LeaverInfo");
                    packet.ReadSingle("LeaveScore", "LeaverInfo");
                    packet.ReadUInt32("SeasonID", "LeaverInfo");
                    packet.ReadUInt32("TotalLeaves", "LeaverInfo");
                    packet.ReadUInt32("TotalSuccesses", "LeaverInfo");
                    packet.ReadInt32("ConsecutiveSuccesses", "LeaverInfo");
                    packet.ReadTime64("LastPenaltyTime", "LeaverInfo");
                    packet.ReadTime64("LeaverExpirationTime", "LeaverInfo");
                    packet.ReadInt32("Unknown_1120", "LeaverInfo");
                    packet.ReadBit("LeaverStatus", "LeaverInfo");
                }
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

            if (hasChallengeMode)
            {
                packet.ResetBitReader();
                packet.ReadInt32("MapID", "ChallengeMode");
                packet.ReadInt32("InitialPlayerCount", "ChallengeMode");
                packet.ReadUInt64("InstanceID", "ChallengeMode");
                packet.ReadTime64("StartTime", "ChallengeMode");
                packet.ReadPackedGuid128("KeystoneOwnerGUID", "ChallengeMode");
                packet.ReadPackedGuid128("LeaverGUID", "ChallengeMode");
                packet.ReadBit("IsActive", "ChallengeMode");
                packet.ReadBit("HasRestrictions", "ChallengeMode");
                packet.ReadBit("CanVoteAbandon", "ChallengeMode");
            }

            if (hasLFG)
            {
                packet.ResetBitReader();

                packet.ReadUInt32("Slot");
                packet.ReadByte("MyFlags");
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

        [Parser(Opcode.SMSG_READY_CHECK_STARTED)]
        public static void HandleReadyCheckStarted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadPackedGuid128("InitiatorGUID");
            packet.ReadInt64("Duration");
        }

        [Parser(Opcode.SMSG_READY_CHECK_RESPONSE)]
        public static void HandleReadyCheckResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadPackedGuid128("Player");
            packet.ReadBit("IsReady");
        }

        [Parser(Opcode.SMSG_READY_CHECK_COMPLETED)]
        public static void HandleReadyCheckCompleted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
        }

        [Parser(Opcode.SMSG_GROUP_NEW_LEADER)]
        public static void HandleGroupNewLeader(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
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

        [Parser(Opcode.SMSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            packet.ReadPackedGuid128("Sender");
            packet.ReadVector2("Position");
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_PARTIAL_STATE)]
        public static void HandlePartyMemberPartialState(Packet packet)
        {
            packet.ReadBit("ForEnemyChanged");
            packet.ReadBit("SetPvPInactive"); // adds GroupMemberStatusFlag 0x0020 if true, removes 0x0020 if false
            packet.ReadBit("Unk901_1");
            var partyTypeChanged = packet.ReadBit("PartyTypeChanged");
            var flagsChanged = packet.ReadBit("FlagsChanged");
            var powerTypeChanged = packet.ReadBit("PowerTypeChanged");
            var overrideDisplayPowerChanged = packet.ReadBit("OverrideDisplayPowerChanged");
            var currentHealthChanged = packet.ReadBit("CurrentHealthChanged");

            var maxHealthChanged = packet.ReadBit("MaxHealthChanged");
            var powerChanged = packet.ReadBit("PowerChanged");
            var maxPowerChanged = packet.ReadBit("MaxPowerChanged");
            var levelChanged = packet.ReadBit("LevelChanged");
            var specChanged = packet.ReadBit("SpecChanged");
            var areaIdChanged = packet.ReadBit("AreaIdChanged");
            var wmoGroupIdChanged = packet.ReadBit("WmoGroupIdChanged");
            var wmoDoodadPlacementIdChanged = packet.ReadBit("WmoDoodadPlacementIdChanged");

            var positionChanged = packet.ReadBit("PositionChanged");
            var vehicleSeatRecIdChanged = packet.ReadBit("VehicleSeatRecIdChanged");
            var aurasChanged = packet.ReadBit("AurasChanged");
            var petChanged = packet.ReadBit("PetChanged");
            var phaseChanged = packet.ReadBit("PhaseChanged");
            var ctrOptionsChanged = packet.ReadBit("CTROptionsChanged");

            if (petChanged)
            {
                packet.ResetBitReader();
                var petGuidChanged = packet.ReadBit("GuidChanged", "Pet");
                var petNameChanged = packet.ReadBit("NameChanged", "Pet");
                var petDisplayIdChanged = packet.ReadBit("DisplayIdChanged", "Pet");
                var petMaxHealthChanged = packet.ReadBit("MaxHealthChanged", "Pet");
                var petHealthChanged = packet.ReadBit("HealthChanged", "Pet");
                var petAurasChanged = packet.ReadBit("AurasChanged", "Pet");
                if (petNameChanged)
                {
                    packet.ResetBitReader();
                    var len = packet.ReadBits(8);
                    packet.ReadWoWString("NewPetName", len, "Pet");
                }
                if (petGuidChanged)
                    packet.ReadPackedGuid128("NewPetGuid", "Pet");
                if (petDisplayIdChanged)
                    packet.ReadUInt32("PetDisplayID", "Pet");
                if (petMaxHealthChanged)
                    packet.ReadUInt32("PetMaxHealth", "Pet");
                if (petHealthChanged)
                    packet.ReadUInt32("PetHealth", "Pet");
                if (petAurasChanged)
                {
                    var cnt = packet.ReadInt32("AuraCount", "Pet", "Aura");
                    for (int i = 0; i < cnt; i++)
                        ReadAuraInfos(packet, "Pet", "Aura", i);
                }
            }

            packet.ReadPackedGuid128("AffectedGUID");
            if (partyTypeChanged)
            {
                for (int i = 0; i < 2; i++)
                    packet.ReadByte("PartyType", i);
            }

            if (flagsChanged)
                packet.ReadUInt16E<GroupMemberStatusFlag>("Flags");
            if (powerTypeChanged)
                packet.ReadByte("PowerType");
            if (overrideDisplayPowerChanged)
                packet.ReadUInt16("OverrideDisplayPower");
            if (currentHealthChanged)
                packet.ReadUInt32("CurrentHealth");
            if (maxHealthChanged)
                packet.ReadUInt32("MaxHealth");
            if (powerChanged)
                packet.ReadUInt16("Power");
            if (maxPowerChanged)
                packet.ReadUInt16("MaxPower");
            if (levelChanged)
                packet.ReadUInt16("Level");
            if (specChanged)
                packet.ReadUInt16("Spec");
            if (areaIdChanged)
                packet.ReadUInt16("AreaID");
            if (wmoGroupIdChanged)
                packet.ReadUInt16("WmoGroupID");
            if (wmoDoodadPlacementIdChanged)
                packet.ReadUInt32("WmoDoodadPlacementID");
            if (positionChanged)
            {
                packet.ReadUInt16("PositionX");
                packet.ReadUInt16("PositionY");
                packet.ReadUInt16("PositionZ");
            }
            if (vehicleSeatRecIdChanged)
                packet.ReadUInt32("VehicleSeatRecID");
            if (aurasChanged)
            {
                var cnt = packet.ReadInt32("AuraCount", "Aura");
                for (int i = 0; i < cnt; i++)
                    ReadAuraInfos(packet, "Aura", i);
            }
            if (phaseChanged)
                ReadPhaseInfos(packet, "Phase");

            if (ctrOptionsChanged)
            {
                packet.ReadUInt32("ConditionalFlags", "CTROptions");
                packet.ReadByte("FactionGroup ", "CTROptions");
                packet.ReadUInt32("ChromieTimeExpansionMask", "CTROptions");
            }
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

            packet.ReadUInt32("ConditionalFlags", "CTROptions");
            packet.ReadByte("FactionGroup ", "CTROptions");
            packet.ReadUInt32("ChromieTimeExpansionMask", "CTROptions");

            for (int i = 0; i < auraCount; i++)
                ReadAuraInfos(packet, "Aura", i);

            packet.ResetBitReader();
            var hasPet = packet.ReadBit("HasPet");

            if (hasPet) // Pet
                ReadPetInfos(packet, "Pet");

            packet.ReadPackedGuid128("MemberGuid");
        }

        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        [Parser(Opcode.SMSG_GROUP_REQUEST_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            var nameLength = packet.ReadBits(9);
            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.SMSG_GROUP_UNINVITE)]
        public static void HandleGroupUninvite(Packet packet)
        {
            packet.ReadByte("Reason");
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

        [Parser(Opcode.SMSG_RAID_GROUP_ONLY)]
        public static void HandleRaidGroupOnly(Packet packet)
        {
            packet.ReadInt32("Delay");
            packet.ReadUInt32("Reason");
        }

        [Parser(Opcode.SMSG_BROADCAST_SUMMON_CAST)]
        public static void HandleBroadcastSummonCast(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
        }

        [Parser(Opcode.SMSG_BROADCAST_SUMMON_RESPONSE)]
        public static void HandleBroadcastSummonResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadBit("Accepted");
        }

        [Parser(Opcode.CMSG_CLEAR_RAID_MARKER)]
        public static void HandleClearRaidMarker(Packet packet)
        {
            packet.ReadByte("MarkerId");
        }

        [Parser(Opcode.CMSG_SET_ROLE)]
        public static void HandleSetRole(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadPackedGuid128("ChangedUnit");
            packet.ReadByte("Role");
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_INITIATE_ROLE_POLL)]
        [Parser(Opcode.CMSG_REQUEST_PARTY_JOIN_UPDATES)]
        [Parser(Opcode.CMSG_DO_READY_CHECK)]
        [Parser(Opcode.CMSG_LEAVE_GROUP)]
        public static void HandleInitiateRolePoll(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ResetBitReader();
            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);
            packet.ReadUInt32("ProposedRoles");
            packet.ReadPackedGuid128("TargetGuid");

            packet.ReadWoWString("TargetName", lenTargetName);
            packet.ReadWoWString("TargetRealm", lenTargetRealm);

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE_RESPONSE)]
        public static void HandlePartyInviteResponse(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadBit("Accept");
            var hasRolesDesired = packet.ReadBit("HasRolesDesired");
            packet.ResetBitReader();

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");

            if (hasRolesDesired)
                packet.ReadByte("RolesDesired");
        }

        [Parser(Opcode.CMSG_SET_EVERYONE_IS_ASSISTANT)]
        public static void HandleSetEveryoneIsAssistant(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadBit("EveryoneIsAssistant");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_READY_CHECK_RESPONSE)]
        public static void HandleClientReadyCheckResponse(Packet packet)
        {
            packet.ReadBit("IsReady");
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_PARTY_UNINVITE)]
        public static void HandlePartyUninvite(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            var len = packet.ReadBits(8);

            packet.ReadPackedGuid128("TargetGuid");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");

            packet.ReadWoWString("Reason", len);
        }

        [Parser(Opcode.CMSG_SET_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadByteE<LootMethod>("Method");
            packet.ReadPackedGuid128("Master");
            packet.ReadInt32E<ItemQuality>("Threshold");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_SET_PARTY_LEADER)]
        public static void HandleSetPartyLeader(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadPackedGuid128("Target");
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_MINIMAP_PING)]
        public static void HandleClientMinimapPing(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadVector2("Position");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
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

        [Parser(Opcode.CMSG_SWAP_SUB_GROUPS)]
        public static void HandleSwapSubGroups(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadPackedGuid128("FirstTarget");
            packet.ReadPackedGuid128("SecondTarget"); ;
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_CONVERT_RAID)]
        public static void HandleConvertRaid(Packet packet)
        {
            packet.ReadBit("Raid");
        }

        [Parser(Opcode.CMSG_SET_ASSISTANT_LEADER)]
        public static void HandleSetAssistantLeader(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadBit("Apply");
            packet.ReadPackedGuid128("Target");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_UPDATE_RAID_TARGET)]
        public static void HandleUpdateRaidTarget(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadPackedGuid128("Target");
            packet.ReadByte("Symbol");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_SET_PARTY_ASSIGNMENT)]
        public static void HandleSetPartyAssigment(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadBit("Set");
            packet.ReadInt32("Assignment");
            packet.ReadPackedGuid128("Target");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadPackedGuid128("Target");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_DO_COUNTDOWN)]
        public static void HandleDoCountdown(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit();
            packet.ReadBit("UnkBit");
            packet.ReadInt32("TotalTime");
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_GROUP_DESTROYED)]
        [Parser(Opcode.CMSG_REQUEST_RAID_INFO)]
        public static void HandleGroupNull(Packet packet)
        {
        }
    }
}
