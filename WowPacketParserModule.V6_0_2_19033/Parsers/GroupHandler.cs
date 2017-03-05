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
            packet.Translator.ReadVector2("Position");
            packet.Translator.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Sender");
            packet.Translator.ReadVector2("Position");
        }

        [Parser(Opcode.SMSG_PARTY_UPDATE)]
        public static void HandlePartyUpdate(Packet packet)
        {
            packet.Translator.ReadByte("PartyFlags");
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadByte("PartyType");

            packet.Translator.ReadInt32("MyIndex");
            packet.Translator.ReadPackedGuid128("LeaderGUID");
            packet.Translator.ReadInt32("SequenceNum");
            packet.Translator.ReadPackedGuid128("PartyGUID");

            var int13 = packet.Translator.ReadInt32("PlayerListCount");
            for (int i = 0; i < int13; i++)
            {
                packet.Translator.ResetBitReader();
                var bits76 = packet.Translator.ReadBits(6);

                packet.Translator.ReadPackedGuid128("Guid", i);

                packet.Translator.ReadByte("Connected", i);
                packet.Translator.ReadByte("Subgroup", i);
                packet.Translator.ReadByte("Flags", i);
                packet.Translator.ReadByte("RolesAssigned", i);
                packet.Translator.ReadByteE<Class>("PlayerClass", i);

                packet.Translator.ReadWoWString("Name", bits76, i);
            }

            packet.Translator.ResetBitReader();

            var bit68 = packet.Translator.ReadBit("HasLfgInfo");
            var bit144 = packet.Translator.ReadBit("HasLootSettings");
            var bit164 = packet.Translator.ReadBit("HasDifficultySettings");

            if (bit68)
            {
                packet.Translator.ReadByte("MyLfgFlags");
                packet.Translator.ReadInt32("LfgSlot");
                packet.Translator.ReadInt32("MyLfgRandomSlot");
                packet.Translator.ReadByte("MyLfgPartialClear");
                packet.Translator.ReadSingle("MyLfgGearDiff");
                packet.Translator.ReadByte("MyLfgStrangerCount");
                packet.Translator.ReadByte("MyLfgKickVoteCount");
                packet.Translator.ReadByte("LfgBootCount");

                packet.Translator.ResetBitReader();

                packet.Translator.ReadBit("LfgAborted");
                packet.Translator.ReadBit("MyLfgFirstReward");
            }

            if (bit144)
            {
                packet.Translator.ReadByte("LootMethod");
                packet.Translator.ReadPackedGuid128("LootMaster");
                packet.Translator.ReadByte("LootThreshold");
            }

            if (bit164)
            {
                packet.Translator.ReadInt32("Unk Int4");
                //for (int i = 0; i < 2; i++)
                //{
                    packet.Translator.ReadInt32("DungeonDifficultyID");
                    packet.Translator.ReadInt32("RaidDifficultyID");
                //}
            }
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            packet.Translator.ReadBit("ForEnemy");
            packet.Translator.ReadBit("FullUpdate");
            var bit761 = packet.Translator.ReadBit("HasPartyType");
            var bit790 = packet.Translator.ReadBit("HasStatus");
            var bit763 = packet.Translator.ReadBit("HasPowerType");
            var bit322 = packet.Translator.ReadBit("HasSpec");
            var bit28 = packet.Translator.ReadBit("HasHealth");
            var bit316 = packet.Translator.ReadBit("HasMaxHealth");
            var bit748 = packet.Translator.ReadBit("HasPower");
            var bit766 = packet.Translator.ReadBit("HasMaxPower");
            var bit752 = packet.Translator.ReadBit("HasLevel");
            var bit326 = packet.Translator.ReadBit("HasSpec");
            var bit770 = packet.Translator.ReadBit("HasAreaId");
            var bit756 = packet.Translator.ReadBit("HasWmoGroupID");
            var bit776 = packet.Translator.ReadBit("HasWmoDoodadPlacementID");
            var bit786 = packet.Translator.ReadBit("HasPosition");
            var bit20 = packet.Translator.ReadBit("HasVehicleSeatRecID");
            var bit308 = packet.Translator.ReadBit("HasAuras");
            var bit736 = packet.Translator.ReadBit("HasPet");
            var bit72 = packet.Translator.ReadBit("HasPhase");

            packet.Translator.ReadPackedGuid128("MemberGuid");

            if (bit761)
            {
                // sub_5FB6A9
                for (int i = 0; i < 2; i++)
                    packet.Translator.ReadByte("PartyType", i);
            }

            if (bit790)
                packet.Translator.ReadInt16E<GroupMemberStatusFlag>("Flags");

            if (bit763)
                packet.Translator.ReadByte("DisplayPower");

            if (bit322)
                packet.Translator.ReadInt16("OverrideDisplayPower");

            if (bit28)
                packet.Translator.ReadInt32("Health");

            if (bit316)
                packet.Translator.ReadInt32("MaxHealth");

            if (bit748)
                packet.Translator.ReadInt16("Power");

            if (bit766)
                packet.Translator.ReadInt16("MaxPower");

            if (bit752)
                packet.Translator.ReadInt16("Level");

            if (bit326)
                packet.Translator.ReadInt16("Spec");

            if (bit770)
                packet.Translator.ReadInt16("AreaID");

            if (bit756)
                packet.Translator.ReadInt16("WmoGroupID");

            if (bit776)
                packet.Translator.ReadInt32("WmoDoodadPlacementID");

            if (bit786)
            {
                // sub_626D9A
                packet.Translator.ReadInt16("PositionX");
                packet.Translator.ReadInt16("PositionY");
                packet.Translator.ReadInt16("PositionZ");
            }

            if (bit20)
                packet.Translator.ReadInt32("VehicleSeatRecID");

            if (bit308)
            {
                // sub_618493
                var count = packet.Translator.ReadInt32("AuraCount");

                for (int i = 0; i < count; i++)
                {
                    packet.Translator.ReadInt32<SpellId>("Aura", i);
                    packet.Translator.ReadByte("Flags", i);
                    packet.Translator.ReadInt32("ActiveFlags", i);
                    var byte3 = packet.Translator.ReadInt32("PointsCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.Translator.ReadSingle("Points", i, j);
                }
            }

            if (bit736) // Pet
            {
                // sub_618CF4
                packet.Translator.ResetBitReader();

                var bit16 = packet.Translator.ReadBit("HasPetGUID");
                var bit153 = packet.Translator.ReadBit("HasPetName");
                var bit156 = packet.Translator.ReadBit("HasPetModelId");
                var bit164 = packet.Translator.ReadBit("HasPetCurrentHealth");
                var bit172 = packet.Translator.ReadBit("HasPetMaxHealth");
                var bit404 = packet.Translator.ReadBit("HasPetAuras");

                if (bit16)
                    packet.Translator.ReadPackedGuid128("PetGUID");

                if (bit153)
                {
                    // sub_5EA889
                    packet.Translator.ResetBitReader();
                    var len = packet.Translator.ReadBits(8);
                    packet.Translator.ReadWoWString("PetName", len);
                }

                if (bit156)
                    packet.Translator.ReadInt16("PetDisplayID");

                if (bit164)
                    packet.Translator.ReadInt32("PetHealth");

                if (bit172)
                    packet.Translator.ReadInt32("PetMaxHealth");

                if (bit404)
                {
                    var count = packet.Translator.ReadInt32("PetAuraCount");

                    for (int i = 0; i < count; i++)
                    {
                        packet.Translator.ReadInt32<SpellId>("PetAura", i);
                        packet.Translator.ReadByte("PetFlags", i);
                        packet.Translator.ReadInt32("PetActiveFlags", i);
                        var byte3 = packet.Translator.ReadInt32("PetPointsCount", i);

                        for (int j = 0; j < byte3; j++)
                            packet.Translator.ReadSingle("PetPoints", i, j);
                    }
                }
            }

            if (bit72) // Phase
            {
                // sub_61E155
                packet.Translator.ReadInt32("PhaseShiftFlags");
                var int4 = packet.Translator.ReadInt32("PhaseCount");
                packet.Translator.ReadPackedGuid128("PersonalGUID");
                for (int i = 0; i < int4; i++)
                {
                    packet.Translator.ReadInt16("PhaseFlags", i);
                    packet.Translator.ReadInt16("Id", i);
                }
            }
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATE)]
        public static void HandlePartyMemberState(Packet packet)
        {
            packet.Translator.ReadBit("ForEnemy");
            packet.Translator.ReadPackedGuid128("MemberGuid");

            for (var i = 0; i < 2; i++)
                packet.Translator.ReadByte("PartyType", i);

            packet.Translator.ReadInt16E<GroupMemberStatusFlag>("Flags");

            packet.Translator.ReadByte("PowerType");
            packet.Translator.ReadInt16("OverrideDisplayPower");
            packet.Translator.ReadInt32("Health");
            packet.Translator.ReadInt32("MaxHealth");
            packet.Translator.ReadInt16("Power");
            packet.Translator.ReadInt16("MaxPower");
            packet.Translator.ReadInt16("Level");
            packet.Translator.ReadInt16("Spec");
            packet.Translator.ReadInt16("AreaID");

            packet.Translator.ReadInt16("WmoGroupID");
            packet.Translator.ReadInt32("WmoDoodadPlacementID");

            packet.Translator.ReadInt16("PositionX");
            packet.Translator.ReadInt16("PositionY");
            packet.Translator.ReadInt16("PositionZ");

            packet.Translator.ReadInt32("VehicleSeatRecID");
            var auraCount = packet.Translator.ReadInt32("AuraCount");

            packet.Translator.ReadInt32("PhaseShiftFlags");
            var int4 = packet.Translator.ReadInt32("PhaseCount");
            packet.Translator.ReadPackedGuid128("PersonalGUID");
            for (int i = 0; i < int4; i++)
            {
                packet.Translator.ReadInt16("PhaseFlags", i);
                packet.Translator.ReadInt16("Id", i);
            }

            for (int i = 0; i < auraCount; i++)
            {
                packet.Translator.ReadInt32<SpellId>("Aura", i);
                packet.Translator.ReadByte("Flags", i);
                packet.Translator.ReadInt32("ActiveFlags", i);
                var byte3 = packet.Translator.ReadInt32("PointsCount", i);

                for (int j = 0; j < byte3; j++)
                    packet.Translator.ReadSingle("Points", i, j);
            }

            packet.Translator.ResetBitReader();

            var hasPet = packet.Translator.ReadBit("HasPet");
            if (hasPet) // Pet
            {
                packet.Translator.ReadPackedGuid128("PetGuid");
                packet.Translator.ReadInt16("PetDisplayID");
                packet.Translator.ReadInt32("PetMaxHealth");
                packet.Translator.ReadInt32("PetHealth");

                var petAuraCount = packet.Translator.ReadInt32("PetAuraCount");
                for (int i = 0; i < petAuraCount; i++)
                {
                    packet.Translator.ReadInt32<SpellId>("PetAura", i);
                    packet.Translator.ReadByte("PetFlags", i);
                    packet.Translator.ReadInt32("PetActiveFlags", i);
                    var byte3 = packet.Translator.ReadInt32("PetPointsCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.Translator.ReadSingle("PetPoints", i, j);
                }

                packet.Translator.ResetBitReader();

                var len = packet.Translator.ReadBits(8);
                packet.Translator.ReadWoWString("PetName", len);
            }
        }

        [Parser(Opcode.SMSG_ROLE_CHANGED_INFORM)]
        public static void HandleRoleChangedInform(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("From");
            packet.Translator.ReadPackedGuid128("ChangedUnit");
            packet.Translator.ReadInt32E<LfgRoleFlag>("OldRole");
            packet.Translator.ReadInt32E<LfgRoleFlag>("NewRole");
        }

        [Parser(Opcode.SMSG_ROLE_POLL_INFORM)]
        public static void HandleRolePollInform(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("From");
        }

        [Parser(Opcode.SMSG_GROUP_NEW_LEADER)]
        public static void HandleGroupNewLeader(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            var len = packet.Translator.ReadBits(6);
            packet.Translator.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_PARTY_INVITE)]
        public static void HandlePartyInvite(Packet packet)
        {
            // Order guessed
            packet.Translator.ReadBit("CanAccept");
            packet.Translator.ReadBit("MightCRZYou");
            packet.Translator.ReadBit("MustBeBNetFriend");
            packet.Translator.ReadBit("AllowMultipleRoles");
            packet.Translator.ReadBit("IsXRealm");

            var len = packet.Translator.ReadBits(6);

            packet.Translator.ReadPackedGuid128("InviterGuid");
            packet.Translator.ReadPackedGuid128("InviterBNetAccountID");

            packet.Translator.ReadInt32("InviterCfgRealmID");
            packet.Translator.ReadInt16("Unk1");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("IsLocal");
            packet.Translator.ReadBit("Unk2");

            var bits2 = packet.Translator.ReadBits(8);
            var bits258 = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("InviterRealmNameActual", bits2);
            packet.Translator.ReadWoWString("InviterRealmNameNormalized", bits258);

            packet.Translator.ReadInt32("ProposedRoles");
            var int32 = packet.Translator.ReadInt32("LfgSlotsCount");
            packet.Translator.ReadInt32("LfgCompletedMask");

            packet.Translator.ReadWoWString("InviterName", len);

            for (int i = 0; i < int32; i++)
                packet.Translator.ReadInt32("LfgSlots", i);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("Target");
        }

        [Parser(Opcode.CMSG_UPDATE_RAID_TARGET)]
        public static void HandleUpdateRaidTarget(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("Target");
            packet.Translator.ReadByte("Symbol");
        }

        [Parser(Opcode.SMSG_SEND_RAID_TARGET_UPDATE_SINGLE)]
        public static void HandleSendRaidTargetUpdateSingle(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadByte("Symbol");
            packet.Translator.ReadPackedGuid128("Target");
            packet.Translator.ReadPackedGuid128("ChangedBy");
        }

        [Parser(Opcode.SMSG_SEND_RAID_TARGET_UPDATE_ALL)]
        public static void HandleSendRaidTargetUpdateAll(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            var raidTargetSymbolCount = packet.Translator.ReadInt32("RaidTargetSymbolCount");
            for (int i = 0; i < raidTargetSymbolCount; i++)
            {
                packet.Translator.ReadPackedGuid128("Target", i);
                packet.Translator.ReadByte("Symbol", i);
            }
        }

        [Parser(Opcode.SMSG_READY_CHECK_RESPONSE)]
        public static void HandleReadyCheckResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PartyGUID");
            packet.Translator.ReadPackedGuid128("Player");
            packet.Translator.ReadBit("IsReady");
        }

        [Parser(Opcode.SMSG_READY_CHECK_STARTED)]
        public static void HandleReadyCheckStarted(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("PartyGUID");
            packet.Translator.ReadPackedGuid128("InitiatorGUID");
            packet.Translator.ReadInt32("Duration");
        }

        [Parser(Opcode.CMSG_READY_CHECK_RESPONSE)]
        public static void HandleClientReadyCheckResponse(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("PartyGUID");
            packet.Translator.ReadBit("IsReady");
        }

        [Parser(Opcode.SMSG_READY_CHECK_COMPLETED)]
        public static void HandleReadyCheckCompleted(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("PartyGUID");
        }

        [Parser(Opcode.SMSG_RAID_MARKERS_CHANGED)]
        public static void HandleRaidMarkersChanged(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadInt32("ActiveMarkers");

            var count = packet.Translator.ReadBits(4);
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadPackedGuid128("TransportGUID");
                packet.Translator.ReadInt32("MapID");
                packet.Translator.ReadVector3("Position");
            }
        }

        [Parser(Opcode.SMSG_PARTY_COMMAND_RESULT)]
        public static void HandlePartyCommandResult(Packet packet)
        {
            var nameLength = packet.Translator.ReadBits(9);
            packet.Translator.ReadBitsE<PartyCommand>("Command", 4);
            packet.Translator.ReadBitsE<PartyResult>("Result", 6);
            packet.Translator.ReadUInt32("ResultData");
            packet.Translator.ReadPackedGuid128("ResultGUID");
            packet.Translator.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.CMSG_LEAVE_GROUP)]
        [Parser(Opcode.CMSG_REQUEST_PARTY_JOIN_UPDATES)]
        public static void HandleLeaveGroup(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE_RESPONSE)]
        public static void HandlePartyInviteResponse(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Accept");
            var hasRolesDesired = packet.Translator.ReadBit("HasRolesDesired");
            if (hasRolesDesired)
                packet.Translator.ReadInt32("RolesDesired");
        }

        [Parser(Opcode.CMSG_PARTY_UNINVITE)]
        public static void HandlePartyUninvite(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("TargetGuid");

            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Reason", len);
        }

        [Parser(Opcode.CMSG_SET_ROLE)]
        public static void HandleSetRole(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("ChangedUnit");
            packet.Translator.ReadInt32("Role");
        }

        [Parser(Opcode.CMSG_SET_PARTY_LEADER)]
        public static void HandleSetPartyLeader(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadPackedGuid128("Target");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadInt32("ProposedRoles");
            packet.Translator.ReadPackedGuid128("TargetGuid");
            packet.Translator.ReadInt32("TargetCfgRealmID");

            packet.Translator.ResetBitReader();

            var lenTargetName = packet.Translator.ReadBits(9);
            var lenTargetRealm = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("TargetName", lenTargetName);
            packet.Translator.ReadWoWString("TargetRealm", lenTargetRealm);
        }

        [Parser(Opcode.CMSG_CONVERT_RAID)]
        public static void HandleConvertRaid(Packet packet)
        {
            packet.Translator.ReadBit("Raid");
        }
    }
}
