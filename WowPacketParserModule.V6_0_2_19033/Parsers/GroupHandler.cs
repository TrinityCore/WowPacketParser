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

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            packet.ReadBit("ForEnemy");
            packet.ReadBit("FullUpdate");
            var bit761 = packet.ReadBit("HasUnk761");
            var bit790 = packet.ReadBit("HasStatus");
            var bit763 = packet.ReadBit("HasPowerType");
            var bit322 = packet.ReadBit("HasUnk322");
            var bit28 = packet.ReadBit("HasCurrentHealth");
            var bit316 = packet.ReadBit("HasMaxHealth");
            var bit748 = packet.ReadBit("HasCurrentPower");
            var bit766 = packet.ReadBit("HasMaxPower");
            var bit752 = packet.ReadBit("HasLevel");
            var bit326 = packet.ReadBit("HasUnk326");
            var bit770 = packet.ReadBit("HasZoneId");
            var bit756 = packet.ReadBit("HasUnk756");
            var bit776 = packet.ReadBit("HasUnk776");
            var bit786 = packet.ReadBit("HasPosition");
            var bit20 = packet.ReadBit("HasVehicleSeat");
            var bit308 = packet.ReadBit("HasAuras");
            var bit736 = packet.ReadBit("HasPet");
            var bit72 = packet.ReadBit("HasPhase");

            packet.ReadPackedGuid128("MemberGuid");

            if (bit761)
            {
                // sub_5FB6A9
                for (int i = 0; i < 2; i++)
                    packet.ReadByte("Unk761", i);
            }

            if (bit790)
                packet.ReadInt16E<GroupMemberStatusFlag>("Status");

            if (bit763)
                packet.ReadByte("PowerType");

            if (bit322)
                packet.ReadInt16("Unk322");

            if (bit28)
                packet.ReadInt32("CurrentHealth");

            if (bit316)
                packet.ReadInt32("MaxHealth");

            if (bit748)
                packet.ReadInt16("CurrentPower");

            if (bit766)
                packet.ReadInt16("MaxPower");

            if (bit752)
                packet.ReadInt16("Level");

            if (bit326)
                packet.ReadInt16("Unk200000");

            if (bit770)
                packet.ReadInt16("ZoneId");

            if (bit756)
                packet.ReadInt16("Unk2000000");

            if (bit776)
                packet.ReadInt32("Unk4000000");

            if (bit786)
            {
                // sub_626D9A
                packet.ReadInt16("PositionX");
                packet.ReadInt16("PositionY");
                packet.ReadInt16("PositionZ");
            }

            if (bit20)
                packet.ReadInt32("VehicleSeat");

            if (bit308)
            {
                // sub_618493
                var count = packet.ReadInt32("AuraCount");

                for (int i = 0; i < count; i++)
                {
                    packet.ReadInt32("SpellId", i);
                    packet.ReadByte("Scalings", i);
                    packet.ReadInt32("EffectMask", i);
                    var scaleCount = packet.ReadInt32("ScaleCount", i);

                    for (int j = 0; j < scaleCount; j++)
                        packet.ReadSingle("Scale", i, j);
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
                    packet.ReadInt16("PetModelId");

                if (bit164)
                    packet.ReadInt32("PetCurrentHealth");

                if (bit172)
                    packet.ReadInt32("PetMaxHealth");

                if (bit404)
                {
                    var count = packet.ReadInt32("AuraCount");

                    for (int i = 0; i < count; i++)
                    {
                        packet.ReadInt32("SpellId", i);
                        packet.ReadByte("Scalings", i);
                        packet.ReadInt32("EffectMask", i);
                        var byte3 = packet.ReadInt32("EffectCount", i);

                        for (int j = 0; j < byte3; j++)
                            packet.ReadSingle("Scale", i, j);
                    }
                }
            }

            if (bit72) // Phase
            {
                // sub_61E155
                packet.ReadInt32("PhaseShiftFlags");
                var int4 = packet.ReadInt32("PhaseCount");
                packet.ReadPackedGuid128("PersonalGUID");
                for (int i = 0; i < int4; i++)
                {
                    packet.ReadInt16("PhaseFlags", i);
                    packet.ReadInt16("Id", i);
                }
            }
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATE)]
        public static void HandlePartyMemberState(Packet packet)
        {
            packet.ReadBit("ForEnemy");
            packet.ReadPackedGuid128("MemberGuid");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("Unk704", i);

            packet.ReadInt16E<GroupMemberStatusFlag>("Status");

            packet.ReadByte("PowerType");
            packet.ReadInt16("Unk322");
            packet.ReadInt32("CurrentHealth");
            packet.ReadInt32("MaxHealth");
            packet.ReadInt16("CurrentPower");
            packet.ReadInt16("MaxPower");
            packet.ReadInt16("Level");
            packet.ReadInt16("Unk200000");
            packet.ReadInt16("ZoneId");

            packet.ReadInt16("Unk2000000");
            packet.ReadInt32("Unk4000000");

            packet.ReadInt16("PositionX");
            packet.ReadInt16("PositionY");
            packet.ReadInt16("PositionZ");

            packet.ReadInt32("VehicleSeat");
            var auraCount = packet.ReadInt32("AuraCount");

            packet.ReadInt32("PhaseShiftFlags");
            var int4 = packet.ReadInt32("PhaseCount");
            packet.ReadPackedGuid128("PersonalGUID");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadInt16("PhaseFlags", i);
                packet.ReadInt16("Id", i);
            }

            for (int i = 0; i < auraCount; i++)
            {
                packet.ReadInt32<SpellId>("SpellId", i);
                packet.ReadByte("Scalings", i);
                packet.ReadInt32("EffectMask", i);
                var byte3 = packet.ReadInt32("EffectCount", i);

                for (int j = 0; j < byte3; j++)
                    packet.ReadSingle("Scale", i, j);
            }

            packet.ResetBitReader();

            var hasPet = packet.ReadBit("HasPet");
            if (hasPet) // Pet
            {
                packet.ReadPackedGuid128("PetGUID");
                packet.ReadInt16("PetModelId");
                packet.ReadInt32("PetCurrentHealth");
                packet.ReadInt32("PetMaxHealth");

                var petAuraCount = packet.ReadInt32("AuraCount");
                for (int i = 0; i < petAuraCount; i++)
                {
                    packet.ReadInt32<SpellId>("SpellId", i);
                    packet.ReadByte("Scalings", i);
                    packet.ReadInt32("EffectMask", i);
                    var byte3 = packet.ReadInt32("EffectCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.ReadSingle("Scale", i, j);
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
