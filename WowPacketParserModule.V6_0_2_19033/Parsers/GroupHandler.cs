using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_MINIMAP_PING)]
        public static void HandleClientMinimapPing(Packet packet)
        {
            packet.ReadVector2("Position");
            packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            packet.ReadPackedGuid128("Sender");
            packet.ReadVector2("Position");
        }

        [Parser(Opcode.SMSG_PARTY_UPDATE)]
        public static void HandlePartyUpdate(Packet packet)
        {
            packet.ReadByte("PartyFlags");
            packet.ReadByte("PartyIndex");
            packet.ReadByte("PartyType");

            packet.ReadInt32("MyIndex");
            packet.ReadPackedGuid128("LeaderGUID");
            packet.ReadInt32("SequenceNum");
            packet.ReadPackedGuid128("PartyGUID");

            var int13 = packet.ReadInt32("PlayerListCount");
            for (int i = 0; i < int13; i++)
            {
                packet.ResetBitReader();
                var bits76 = packet.ReadBits(6);

                packet.ReadPackedGuid128("Guid", i);

                packet.ReadByte("Connected", i);
                packet.ReadByte("Subgroup", i);
                packet.ReadByte("Flags", i);
                packet.ReadByte("RolesAssigned", i);
                packet.ReadByteE<Class>("PlayerClass", i);

                packet.ReadWoWString("Name", bits76, i);
            }

            packet.ResetBitReader();

            var bit68 = packet.ReadBit("HasLfgInfo");
            var bit144 = packet.ReadBit("HasLootSettings");
            var bit164 = packet.ReadBit("HasDifficultySettings");

            if (bit68)
            {
                packet.ReadByte("MyLfgFlags");
                packet.ReadInt32("LfgSlot");
                packet.ReadInt32("MyLfgRandomSlot");
                packet.ReadByte("MyLfgPartialClear");
                packet.ReadSingle("MyLfgGearDiff");
                packet.ReadByte("MyLfgStrangerCount");
                packet.ReadByte("MyLfgKickVoteCount");
                packet.ReadByte("LfgBootCount");

                packet.ResetBitReader();

                packet.ReadBit("LfgAborted");
                packet.ReadBit("MyLfgFirstReward");
            }

            if (bit144)
            {
                packet.ReadByte("LootMethod");
                packet.ReadPackedGuid128("LootMaster");
                packet.ReadByte("LootThreshold");
            }

            if (bit164)
            {
                packet.ReadInt32("Unk Int4");
                //for (int i = 0; i < 2; i++)
                //{
                    packet.ReadInt32("DungeonDifficultyID");
                    packet.ReadInt32("RaidDifficultyID");
                //}
            }
        }

        public static void ReadPartyMemberPhase(Packet packet, params object[] idx)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
                packet.ReadUInt32("PhaseFlags", idx);
            else
                packet.ReadUInt16("PhaseFlags", idx);
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

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            packet.ReadBit("ForEnemy");
            packet.ReadBit("FullUpdate");
            var bit761 = packet.ReadBit("HasPartyType");
            var bit790 = packet.ReadBit("HasStatus");
            var bit763 = packet.ReadBit("HasPowerType");
            var bit322 = packet.ReadBit("HasSpec");
            var bit28 = packet.ReadBit("HasHealth");
            var bit316 = packet.ReadBit("HasMaxHealth");
            var bit748 = packet.ReadBit("HasPower");
            var bit766 = packet.ReadBit("HasMaxPower");
            var bit752 = packet.ReadBit("HasLevel");
            var bit326 = packet.ReadBit("HasSpec");
            var bit770 = packet.ReadBit("HasAreaId");
            var bit756 = packet.ReadBit("HasWmoGroupID");
            var bit776 = packet.ReadBit("HasWmoDoodadPlacementID");
            var bit786 = packet.ReadBit("HasPosition");
            var bit20 = packet.ReadBit("HasVehicleSeatRecID");
            var bit308 = packet.ReadBit("HasAuras");
            var bit736 = packet.ReadBit("HasPet");
            var bit72 = packet.ReadBit("HasPhase");

            packet.ReadPackedGuid128("MemberGuid");

            if (bit761)
            {
                // sub_5FB6A9
                for (int i = 0; i < 2; i++)
                    packet.ReadByte("PartyType", i);
            }

            if (bit790)
                packet.ReadInt16E<GroupMemberStatusFlag>("Flags");

            if (bit763)
                packet.ReadByte("DisplayPower");

            if (bit322)
                packet.ReadInt16("OverrideDisplayPower");

            if (bit28)
                packet.ReadInt32("Health");

            if (bit316)
                packet.ReadInt32("MaxHealth");

            if (bit748)
                packet.ReadInt16("Power");

            if (bit766)
                packet.ReadInt16("MaxPower");

            if (bit752)
                packet.ReadInt16("Level");

            if (bit326)
                packet.ReadInt16("Spec");

            if (bit770)
                packet.ReadInt16<ZoneId>("AreaID");

            if (bit756)
                packet.ReadInt16("WmoGroupID");

            if (bit776)
                packet.ReadInt32("WmoDoodadPlacementID");

            if (bit786)
            {
                // sub_626D9A
                packet.ReadInt16("PositionX");
                packet.ReadInt16("PositionY");
                packet.ReadInt16("PositionZ");
            }

            if (bit20)
                packet.ReadInt32("VehicleSeatRecID");

            if (bit308)
            {
                // sub_618493
                var count = packet.ReadInt32("AuraCount");

                for (int i = 0; i < count; i++)
                {
                    packet.ReadInt32<SpellId>("Aura", i);
                    packet.ReadByte("Flags", i);
                    packet.ReadInt32("ActiveFlags", i);
                    var byte3 = packet.ReadInt32("PointsCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.ReadSingle("Points", i, j);
                }
            }

            if (bit736) // Pet
            {
                // sub_618CF4
                packet.ResetBitReader();

                var bit16 = packet.ReadBit("HasPetGUID");
                var bit153 = packet.ReadBit("HasPetName");
                var bit156 = packet.ReadBit("HasPetModelId");
                var bit164 = packet.ReadBit("HasPetCurrentHealth");
                var bit172 = packet.ReadBit("HasPetMaxHealth");
                var bit404 = packet.ReadBit("HasPetAuras");

                if (bit16)
                    packet.ReadPackedGuid128("PetGUID");

                if (bit153)
                {
                    // sub_5EA889
                    packet.ResetBitReader();
                    var len = packet.ReadBits(8);
                    packet.ReadWoWString("PetName", len);
                }

                if (bit156)
                    packet.ReadInt16("PetDisplayID");

                if (bit164)
                    packet.ReadInt32("PetHealth");

                if (bit172)
                    packet.ReadInt32("PetMaxHealth");

                if (bit404)
                {
                    var count = packet.ReadInt32("PetAuraCount");

                    for (int i = 0; i < count; i++)
                    {
                        packet.ReadInt32<SpellId>("PetAura", i);
                        packet.ReadByte("PetFlags", i);
                        packet.ReadInt32("PetActiveFlags", i);
                        var byte3 = packet.ReadInt32("PetPointsCount", i);

                        for (int j = 0; j < byte3; j++)
                            packet.ReadSingle("PetPoints", i, j);
                    }
                }
            }

            if (bit72) // Phase
                ReadPhaseInfos(packet, "Phase");
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_FULL_STATE)]
        public static void HandlePartyMemberFullState(Packet packet)
        {
            packet.ReadBit("ForEnemy");
            packet.ReadPackedGuid128("MemberGuid");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("PartyType", i);

            packet.ReadInt16E<GroupMemberStatusFlag>("Flags");

            packet.ReadByte("PowerType");
            packet.ReadInt16("OverrideDisplayPower");
            packet.ReadInt32("Health");
            packet.ReadInt32("MaxHealth");
            packet.ReadInt16("Power");
            packet.ReadInt16("MaxPower");
            packet.ReadInt16("Level");

            packet.ReadInt16("Spec");
            packet.ReadInt16<ZoneId>("AreaID");

            packet.ReadInt16("WmoGroupID");
            packet.ReadInt32("WmoDoodadPlacementID");

            packet.ReadInt16("PositionX");
            packet.ReadInt16("PositionY");
            packet.ReadInt16("PositionZ");

            packet.ReadInt32("VehicleSeatRecID");
            var auraCount = packet.ReadInt32("AuraCount");

            ReadPhaseInfos(packet, "Phase");

            for (int i = 0; i < auraCount; i++)
            {
                packet.ReadInt32<SpellId>("Aura", i);
                packet.ReadByte("Flags", i);
                packet.ReadInt32("ActiveFlags", i);
                var byte3 = packet.ReadInt32("PointsCount", i);

                for (int j = 0; j < byte3; j++)
                    packet.ReadSingle("Points", i, j);
            }

            packet.ResetBitReader();

            var hasPet = packet.ReadBit("HasPet");
            if (hasPet) // Pet
            {
                packet.ReadPackedGuid128("PetGuid");
                packet.ReadInt16("PetDisplayID");
                packet.ReadInt32("PetMaxHealth");
                packet.ReadInt32("PetHealth");

                var petAuraCount = packet.ReadInt32("PetAuraCount");
                for (int i = 0; i < petAuraCount; i++)
                {
                    packet.ReadInt32<SpellId>("PetAura", i);
                    packet.ReadByte("PetFlags", i);
                    packet.ReadInt32("PetActiveFlags", i);
                    var byte3 = packet.ReadInt32("PetPointsCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.ReadSingle("PetPoints", i, j);
                }

                packet.ResetBitReader();

                var len = packet.ReadBits(8);
                packet.ReadWoWString("PetName", len);
            }
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

        [Parser(Opcode.SMSG_GROUP_NEW_LEADER)]
        public static void HandleGroupNewLeader(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            var len = packet.ReadBits(6);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_PARTY_INVITE)]
        public static void HandlePartyInvite(Packet packet)
        {
            // Order guessed
            packet.ReadBit("CanAccept");
            packet.ReadBit("MightCRZYou");
            packet.ReadBit("MustBeBNetFriend");
            packet.ReadBit("AllowMultipleRoles");
            packet.ReadBit("IsXRealm");

            var len = packet.ReadBits(6);

            packet.ReadPackedGuid128("InviterGuid");
            packet.ReadPackedGuid128("InviterBNetAccountID");

            packet.ReadInt32("InviterCfgRealmID");
            packet.ReadInt16("Unk1");

            packet.ResetBitReader();

            packet.ReadBit("IsLocal");
            packet.ReadBit("Unk2");

            var bits2 = packet.ReadBits(8);
            var bits258 = packet.ReadBits(8);
            packet.ReadWoWString("InviterRealmNameActual", bits2);
            packet.ReadWoWString("InviterRealmNameNormalized", bits258);

            packet.ReadInt32("ProposedRoles");
            var int32 = packet.ReadInt32("LfgSlotsCount");
            packet.ReadInt32("LfgCompletedMask");

            packet.ReadWoWString("InviterName", len);

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("LfgSlots", i);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("Target");
        }

        [Parser(Opcode.CMSG_UPDATE_RAID_TARGET)]
        public static void HandleUpdateRaidTarget(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("Target");
            packet.ReadByte("Symbol");
        }

        [Parser(Opcode.SMSG_SEND_RAID_TARGET_UPDATE_SINGLE)]
        public static void HandleSendRaidTargetUpdateSingle(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByte("Symbol");
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("ChangedBy");
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadInt64("Duration");
            else
                packet.ReadInt32("Duration");
        }

        [Parser(Opcode.CMSG_READY_CHECK_RESPONSE)]
        public static void HandleClientReadyCheckResponse(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadBit("IsReady");
        }

        [Parser(Opcode.SMSG_READY_CHECK_COMPLETED)]
        public static void HandleReadyCheckCompleted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
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

        [Parser(Opcode.CMSG_LEAVE_GROUP)]
        [Parser(Opcode.CMSG_REQUEST_PARTY_JOIN_UPDATES)]
        public static void HandleLeaveGroup(Packet packet)
        {
            packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE_RESPONSE)]
        public static void HandlePartyInviteResponse(Packet packet)
        {
            packet.ReadByte("PartyIndex");

            packet.ResetBitReader();

            packet.ReadBit("Accept");
            var hasRolesDesired = packet.ReadBit("HasRolesDesired");
            if (hasRolesDesired)
                packet.ReadInt32("RolesDesired");
        }

        [Parser(Opcode.CMSG_PARTY_UNINVITE)]
        public static void HandlePartyUninvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("TargetGuid");

            var len = packet.ReadBits(8);
            packet.ReadWoWString("Reason", len);
        }

        [Parser(Opcode.CMSG_SET_ROLE)]
        public static void HandleSetRole(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("ChangedUnit");
            packet.ReadInt32("Role");
        }

        [Parser(Opcode.CMSG_SET_PARTY_LEADER)]
        public static void HandleSetPartyLeader(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("Target");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadInt32("ProposedRoles");
            packet.ReadPackedGuid128("TargetGuid");
            packet.ReadInt32("TargetCfgRealmID");

            packet.ResetBitReader();

            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);

            packet.ReadWoWString("TargetName", lenTargetName);
            packet.ReadWoWString("TargetRealm", lenTargetRealm);
        }

        [Parser(Opcode.CMSG_CONVERT_RAID)]
        public static void HandleConvertRaid(Packet packet)
        {
            packet.ReadBit("Raid");
        }
    }
}
