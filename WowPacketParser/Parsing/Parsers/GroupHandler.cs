using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_SET_EVERYONE_IS_ASSISTANT)]
        public static void HandleEveryoneIsAssistant(Packet packet)
        {
            packet.Translator.ReadBit("Active");
        }

        [Parser(Opcode.CMSG_GROUP_SET_ROLES, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGroupSetRoles(Packet packet)
        {
            packet.Translator.ReadUInt32("Role");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GROUP_SET_ROLES, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGroupSetRoles434(Packet packet)
        {
            packet.Translator.ReadInt32E<LfgRoleFlag>("Role");
            var guid = packet.Translator.StartBitStream(2, 6, 3, 7, 5, 1, 0, 4);
            packet.Translator.ParseBitStream(guid, 6, 4, 1, 3, 0, 5, 2, 7);
            packet.Translator.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var grouptype = packet.Translator.ReadByteE<GroupTypeFlag>("Group Type");
            packet.Translator.ReadByte("Sub Group");
            packet.Translator.ReadByteE<GroupUpdateFlag>("Flags");
            packet.Translator.ReadByte("Player Roles Assigned");

            if (grouptype.HasFlag(GroupTypeFlag.LookingForDungeon))
            {
                packet.Translator.ReadByteE<InstanceStatus>("Group Type Status");
                packet.Translator.ReadLfgEntry("LFG Entry");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.Translator.ReadBool("Unk bool");
            }

            packet.Translator.ReadGuid("Group GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.Translator.ReadInt32("Counter");

            var numFields = packet.Translator.ReadInt32("Member Count");
            for (var i = 0; i < numFields; i++)
            {
                var name = packet.Translator.ReadCString("Name", i);
                var guid = packet.Translator.ReadGuid("GUID", i);
                StoreGetters.AddName(guid, name);
                packet.Translator.ReadByteE<GroupMemberStatusFlag>("Status", i);
                packet.Translator.ReadByte("Sub Group", i);
                packet.Translator.ReadByteE<GroupUpdateFlag>("Update Flags", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.Translator.ReadByteE<LfgRoleFlag>("Role", i);
            }

            packet.Translator.ReadGuid("Leader GUID");

            if (numFields <= 0)
                return;

            packet.Translator.ReadByteE<LootMethod>("Loot Method");
            packet.Translator.ReadGuid("Looter GUID");
            packet.Translator.ReadByteE<ItemQuality>("Loot Threshold");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.Translator.ReadByteE<MapDifficulty>("Dungeon Difficulty");

            packet.Translator.ReadByteE<MapDifficulty>("Raid Difficulty");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958) &&
                ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadByte("Unk Byte"); // Has something to do with difficulty too
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS_FULL, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePartyMemberStats422(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PARTY_MEMBER_STATS_FULL, Direction.ServerToClient))
                packet.Translator.ReadBool("Add arena opponent");

            packet.Translator.ReadPackedGuid("GUID");
            var updateFlags = packet.Translator.ReadInt32E<GroupUpdateFlag422>("Update Flags");

            if (updateFlags.HasFlag(GroupUpdateFlag422.Status))
                packet.Translator.ReadInt16E<GroupMemberStatusFlag>("Status");

            if (updateFlags.HasFlag(GroupUpdateFlag422.CurrentHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.Translator.ReadInt32("Current Health");
                else
                    packet.Translator.ReadUInt16("Current Health");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag422.MaxHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.Translator.ReadInt32("Max Health");
                else
                    packet.Translator.ReadUInt16("Max Health");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag422.PowerType))
                packet.Translator.ReadByteE<PowerType>("Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag422.CurrentPower))
                packet.Translator.ReadInt16("Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag422.MaxPower))
                packet.Translator.ReadInt16("Max Power");

            if (updateFlags.HasFlag(GroupUpdateFlag422.Level))
                packet.Translator.ReadInt16("Level");

            if (updateFlags.HasFlag(GroupUpdateFlag422.Zone))
                packet.Translator.ReadInt16<ZoneId>("Zone Id");

            if (updateFlags.HasFlag(GroupUpdateFlag422.Unk100))
                packet.Translator.ReadInt16("Unk");

            if (updateFlags.HasFlag(GroupUpdateFlag422.Position))
            {
                packet.Translator.ReadInt16("X");
                packet.Translator.ReadInt16("Y");
                packet.Translator.ReadInt16("Z");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag422.Auras))
            {
                packet.Translator.ReadByte("Unk byte");
                var mask = packet.Translator.ReadUInt64("Aura mask");
                var cnt = packet.Translator.ReadUInt32("Aura count");
                for (var i = 0; i < cnt; ++i)
                {
                    if (mask == 0) // bad packet
                        break;

                    if ((mask & (1ul << i)) == 0)
                        continue;

                    packet.Translator.ReadUInt32<SpellId>("Spell Id", i);

                    var aflags = packet.Translator.ReadUInt16E<AuraFlag>("Aura Flags", i);
                    if (aflags.HasFlag(AuraFlag.Scalable))
                        for (var j = 0; j < 3; ++j)
                            packet.Translator.ReadInt32("Effect BasePoints", i, j);
                }
            }

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetGuid))
                packet.Translator.ReadUInt64("Pet GUID");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetName))
                packet.Translator.ReadCString("Pet Name");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetModelId))
                packet.Translator.ReadUInt16("Pet Model Id");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetCurrentHealth))
                packet.Translator.ReadUInt32("Pet Current Health");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetMaxHealth))
                packet.Translator.ReadUInt32("Pet Max Health");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetPowerType))
                packet.Translator.ReadByteE<PowerType>("Pet Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetCurrentPower))
                packet.Translator.ReadInt16("Pet Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetMaxPower))
                packet.Translator.ReadInt16("Pet Max Power");

            if (updateFlags.HasFlag(GroupUpdateFlag422.PetAuras))
            {
                packet.Translator.ReadByte("Unk byte");
                var mask = packet.Translator.ReadUInt64("Pet Aura mask");
                var cnt = packet.Translator.ReadUInt32("Pet Aura count");
                for (var i = 0; i < cnt; ++i)
                {
                    if ((mask & (1ul << i)) == 0)
                        continue;

                    packet.Translator.ReadUInt32<SpellId>("Spell Id", i);

                    var aflags = packet.Translator.ReadUInt16E<AuraFlag>("Aura Flags", i);
                    if (aflags.HasFlag(AuraFlag.Scalable))
                        for (var j = 0; j < 3; ++j)
                            packet.Translator.ReadInt32("Effect BasePoints", i, j);
                }
            }

            if (updateFlags.HasFlag(GroupUpdateFlag422.VehicleSeat))
                packet.Translator.ReadInt32("Vehicle Seat?");

            if (updateFlags.HasFlag(GroupUpdateFlag422.Phase))
            {
                packet.Translator.ReadInt32("Unk Int32");

                var count = packet.Translator.ReadInt32("Phase Count");
                for (var i = 0; i < count; ++i)
                    packet.Translator.ReadInt16("Phase Id");
            }
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS_FULL, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) &&
                packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PARTY_MEMBER_STATS_FULL, Direction.ServerToClient))
                packet.Translator.ReadBool("Add arena opponent");

            packet.Translator.ReadPackedGuid("GUID");
            var updateFlags = packet.Translator.ReadUInt32E<GroupUpdateFlag>("Update Flags");

            if (updateFlags.HasFlag(GroupUpdateFlag.Status))
                packet.Translator.ReadInt16E<GroupMemberStatusFlag>("Status");

            if (updateFlags.HasFlag(GroupUpdateFlag.CurrentHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.Translator.ReadInt32("Current Health");
                else
                    packet.Translator.ReadUInt16("Current Health");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag.MaxHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.Translator.ReadInt32("Max Health");
                else
                    packet.Translator.ReadUInt16("Max Health");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag.PowerType))
                packet.Translator.ReadByteE<PowerType>("Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag.CurrentPower))
                packet.Translator.ReadInt16("Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag.MaxPower))
                packet.Translator.ReadInt16("Max Power");

            if (updateFlags.HasFlag(GroupUpdateFlag.Level))
                packet.Translator.ReadInt16("Level");

            if (updateFlags.HasFlag(GroupUpdateFlag.Zone))
                packet.Translator.ReadInt16<ZoneId>("Zone Id");

            if (updateFlags.HasFlag(GroupUpdateFlag.Position))
            {
                packet.Translator.ReadInt16("Position X");
                packet.Translator.ReadInt16("Position Y");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag.Auras))
            {
                var auraMask = packet.Translator.ReadUInt64("Aura Mask");

                var maxAura = ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) ? 64 : 56;
                for (var i = 0; i < maxAura; ++i)
                {
                    if ((auraMask & (1ul << i)) == 0)
                        continue;

                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        packet.Translator.ReadUInt32<SpellId>("Spell Id", i);
                    else
                        packet.Translator.ReadUInt16<SpellId>("Spell Id", i);

                    packet.Translator.ReadByteE<AuraFlag>("Aura Flags", i);
                }
            }

            if (updateFlags.HasFlag(GroupUpdateFlag.PetGuid))
                packet.Translator.ReadGuid("Pet GUID");

            if (updateFlags.HasFlag(GroupUpdateFlag.PetName))
                packet.Translator.ReadCString("Pet Name");

            if (updateFlags.HasFlag(GroupUpdateFlag.PetModelId))
                packet.Translator.ReadInt16("Pet Modelid");

            if (updateFlags.HasFlag(GroupUpdateFlag.PetCurrentHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.Translator.ReadInt32("Pet Current Health");
                else
                    packet.Translator.ReadUInt16("Pet Current Health");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag.PetMaxHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.Translator.ReadInt32("Pet Max Health");
                else
                    packet.Translator.ReadUInt16("Pet Max Health");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag.PetPowerType))
                packet.Translator.ReadByteE<PowerType>("Pet Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag.PetCurrentPower))
                packet.Translator.ReadInt16("Pet Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag.PetMaxPower))
                packet.Translator.ReadInt16("Pet Max Power");

            if (updateFlags.HasFlag(GroupUpdateFlag.PetAuras))
            {
                var auraMask = packet.Translator.ReadUInt64("Pet Auramask");

                var maxAura = ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) ? 64 : 56;
                for (var i = 0; i < maxAura; ++i)
                {
                    if ((auraMask & (1ul << i)) == 0)
                        continue;

                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        packet.Translator.ReadUInt32<SpellId>("Spell Id", i);
                    else
                        packet.Translator.ReadUInt16<SpellId>("Spell Id", i);

                    packet.Translator.ReadByteE<AuraFlag>("Aura Flags", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) && // no idea when this was added exactly, doesn't exist in 2.4.1
                updateFlags.HasFlag(GroupUpdateFlag.VehicleSeat))
                packet.Translator.ReadInt32("Vehicle Seat");
        }

        [Parser(Opcode.CMSG_GROUP_SET_LEADER)]
        public static void HandleGroupSetLeader(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_GROUP_SET_LEADER)]
        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_GROUP_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGroupInvite(Packet packet)
        {
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_GROUP_INVITE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGroupInvite422(Packet packet)
        {
            // note: this handler is different in 4.3.0, it got a bit fancy.
            var guidBytes = packet.Translator.StartBitStream(6, 5, 0, 3, 4, 7, 1, 2);

            packet.Translator.ReadInt32("Unk0"); // Always 0
            packet.Translator.ReadInt32("Unk1"); // Non-zero in cross realm parties (1383)
            packet.Translator.ReadCString("Name");

            packet.Translator.ReadXORByte(guidBytes, 0);
            packet.Translator.ReadXORByte(guidBytes, 7);
            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadXORByte(guidBytes, 1);
            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 6);
            packet.Translator.ReadXORByte(guidBytes, 5);

            packet.Translator.ReadCString("Realm Name"); // Non-empty in cross realm parties

            packet.Translator.ReadXORByte(guidBytes, 3);

            packet.Translator.WriteGuid("Guid", guidBytes); // Non-zero in cross realm parties
        }

        [Parser(Opcode.CMSG_GROUP_INVITE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGroupInvite434(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32"); // Non-zero in cross realm parties (1383)
            packet.Translator.ReadInt32("Unk Int32"); // Always 0
            var guid = new byte[8];
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var strLen = packet.Translator.ReadBits(9);
            guid[3] = packet.Translator.ReadBit();
            var nameLen = packet.Translator.ReadBits(10);
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.ReadWoWString("Name", nameLen);
            packet.Translator.ReadWoWString("Realm Name", strLen); // Non-empty in cross realm parties

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.WriteGuid("Guid", guid); // Non-zero in cross realm parties
        }

        [Parser(Opcode.SMSG_GROUP_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.Translator.ReadBool("invited/already in group flag?");
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadInt32("Unk Int32 1");
            var count = packet.Translator.ReadByte("Count");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt32("Unk Uint32", i);

            packet.Translator.ReadInt32("Unk Int32 2");
        }

        [Parser(Opcode.SMSG_GROUP_INVITE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGroupInviteSmsg434(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.ReadBit("Replied");
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Not Already In Group");
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var count = packet.Translator.ReadBits(9);
            guid[4] = packet.Translator.ReadBit();
            var count2 = packet.Translator.ReadBits(7);
            var count3 = packet.Translator.ReadBits("int32 count", 24);
            packet.Translator.ReadBit("Print Something?");
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.ReadInt32("Timestamp?");
            packet.Translator.ReadInt32("Unk Int 32");
            packet.Translator.ReadInt32("Unk Int 32");

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);

            for (var i = 0; i < count3; i++)
                packet.Translator.ReadInt32("Unk Int 32", i);

            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.ReadWoWString("Realm Name", count);

            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.ReadWoWString("Invited", count2);

            packet.Translator.ReadInt32("Unk Int 32");

            packet.Translator.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.CMSG_GROUP_UNINVITE_GUID)]
        public static void HandleGroupUninviteGuid(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadCString("Reason");
        }

        [Parser(Opcode.CMSG_GROUP_ACCEPT)]
        public static void HandleGroupAccept(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_GROUP_ACCEPT_DECLINE)]
        public static void HandleGroupAcceptDecline(Packet packet)
        {
            packet.Translator.ReadBit("Accepted");
            // 7 0 bits here
            packet.Translator.ReadUInt32("Unknown");
        }

        [Parser(Opcode.MSG_RANDOM_ROLL)]
        public static void HandleRandomRollPackets(Packet packet)
        {
            packet.Translator.ReadInt32("Minimum");
            packet.Translator.ReadInt32("Maximum");

            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.Translator.ReadInt32("Roll");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_PARTY_COMMAND_RESULT)]
        public static void HandlePartyCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32E<PartyCommand>("Command");
            packet.Translator.ReadCString("Member");
            packet.Translator.ReadUInt32E<PartyResult>("Result");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.Translator.ReadUInt32("LFG Boot Cooldown");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadGuid("Player Guid"); // Usually 0
        }

        [Parser(Opcode.SMSG_RAID_GROUP_ONLY)]
        public static void HandleRaidGroupOnly(Packet packet)
        {
            packet.Translator.ReadInt32("Time left");
            packet.Translator.ReadInt32E<InstanceStatus>("Group Type Status?");
        }

        [Parser(Opcode.SMSG_REAL_GROUP_UPDATE)]
        public static void HandleRealGroupUpdate(Packet packet)
        {
            packet.Translator.ReadByteE<GroupTypeFlag>("Group Type");
            packet.Translator.ReadUInt32("Member Count");
            packet.Translator.ReadGuid("Leader GUID");
        }

        [Parser(Opcode.CMSG_GROUP_CHANGE_SUB_GROUP)]
        public static void HandleGroupChangesubgroup(Packet packet)
        {
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadByte("Group");
        }

        [Parser(Opcode.MSG_RAID_READY_CHECK)]
        public static void HandleRaidReadyCheck(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                // Packet is sent in two different methods. One sends a byte and one doesn't
                if (packet.CanRead())
                    packet.Translator.ReadBool("Ready");
            }
            else
                packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_RAID_READY_CHECK_CONFIRM)]
        public static void HandleRaidReadyCheckConfirm(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadBool("Ready");
        }

        [Parser(Opcode.MSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
                packet.Translator.ReadGuid("GUID");

            packet.Translator.ReadVector2("Position");
        }

        [Parser(Opcode.CMSG_GROUP_RAID_CONVERT)]
        public static void HandleGroupRaidConvert(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.Translator.ReadBool("ToRaid");
        }

        [Parser(Opcode.CMSG_GROUP_SWAP_SUB_GROUP)]
        public static void HandleGroupSwapSubGroup(Packet packet)
        {
            packet.Translator.ReadCString("Player 1 name");
            packet.Translator.ReadCString("Player 2 name");
        }

        [Parser(Opcode.CMSG_SET_ASSISTANT_LEADER)]
        public static void HandleGroupAssistantLeader(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadBool("Promote"); // False = demote
        }

        [Parser(Opcode.MSG_PARTY_ASSIGNMENT)]
        public static void HandlePartyAssigment(Packet packet)
        {
            //if (packet.Direction == Direction.ClientToServer)
            packet.Translator.ReadByte("Assigment");
            packet.Translator.ReadBool("Apply");
            packet.Translator.ReadGuid("Guid");
        }

        [Parser(Opcode.SMSG_GROUP_SET_ROLE)] // 4.3.4
        public static void HandleGroupSetRole(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid1[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();

            guid1[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadInt32E<LfgRoleFlag>("New Roles");
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 0);

            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 1);

            packet.Translator.ReadInt32E<LfgRoleFlag>("Old Roles");
            packet.Translator.WriteGuid("Assigner Guid", guid1);
            packet.Translator.WriteGuid("Target Guid", guid2);
        }

        [Parser(Opcode.SMSG_RAID_MARKERS_CHANGED)]
        public static void HandleRaidMarkersChanged(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk Uint32");
        }

        [Parser(Opcode.CMSG_GROUP_INVITE_RESPONSE)]
        public static void HandleGroupInviteResponse434(Packet packet)
        {
            if (packet.Translator.ReadBit("Accepted"))
                packet.Translator.ReadUInt32("Unk Uint32");
        }

        [Parser(Opcode.SMSG_RAID_SUMMON_FAILED)] // 4.3.4
        public static void HandleRaidSummonFailed(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 23);

            var guids = new byte[count][];

            for (int i = 0; i < count; ++i)
                guids[i] = packet.Translator.StartBitStream(5, 3, 1, 7, 2, 0, 6, 4);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.ReadInt32E<RaidSummonFail>("Error", i);

                packet.Translator.ReadXORByte(guids[i], 7);
                packet.Translator.ReadXORByte(guids[i], 3);
                packet.Translator.ReadXORByte(guids[i], 1);

                packet.Translator.WriteGuid("Guid", guids[i], i);
            }

        }

        [Parser(Opcode.CMSG_GROUP_DISBAND)]
        [Parser(Opcode.SMSG_GROUP_DESTROYED)]
        [Parser(Opcode.SMSG_GROUP_UNINVITE)]
        [Parser(Opcode.CMSG_GROUP_DECLINE)]
        [Parser(Opcode.MSG_RAID_READY_CHECK_FINISHED)]
        [Parser(Opcode.CMSG_REQUEST_RAID_INFO)]
        [Parser(Opcode.CMSG_GROUP_REQUEST_JOIN_UPDATES)]
        public static void HandleGroupNull(Packet packet)
        {
        }
    }
}
