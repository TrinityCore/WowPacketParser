using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleGroupInvite(Packet packet)
        {
            var crossRealmGuid = new byte[8];

            packet.ReadInt32("Int114");
            packet.ReadByte("Byte118");
            packet.ReadInt32("Int128");
            crossRealmGuid[7] = packet.ReadBit();
            var realmNameLen = packet.ReadBits(9);
            crossRealmGuid[3] = packet.ReadBit();
            var nameLen = packet.ReadBits(9);
            crossRealmGuid[2] = packet.ReadBit();
            crossRealmGuid[5] = packet.ReadBit();
            crossRealmGuid[4] = packet.ReadBit();
            crossRealmGuid[0] = packet.ReadBit();
            crossRealmGuid[1] = packet.ReadBit();
            crossRealmGuid[6] = packet.ReadBit();

            packet.ReadXORByte(crossRealmGuid, 7);
            packet.ReadXORByte(crossRealmGuid, 6);
            packet.ReadXORByte(crossRealmGuid, 0);
            packet.ReadXORByte(crossRealmGuid, 4);
            packet.ReadWoWString("Name", nameLen);
            packet.ReadXORByte(crossRealmGuid, 1);
            packet.ReadXORByte(crossRealmGuid, 2);
            packet.ReadXORByte(crossRealmGuid, 3);
            packet.ReadWoWString("Realm Name", realmNameLen);
            packet.ReadXORByte(crossRealmGuid, 5);

            packet.WriteGuid("crossRealmGuid", crossRealmGuid);
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var inviterGuid = new byte[8];

            var realmName1 = (int)packet.ReadBits(8);
            var realmName2 = (int)packet.ReadBits(8);
            inviterGuid[2] = packet.ReadBit();
            var bit160 = packet.ReadBit("bit160");
            var inviterName = (int)packet.ReadBits(6);
            inviterGuid[7] = packet.ReadBit();
            inviterGuid[5] = packet.ReadBit();
            var bit150 = packet.ReadBit("bit150");
            var bit148 = packet.ReadBit("bit148");
            inviterGuid[1] = packet.ReadBit();
            var bit149 = packet.ReadBit("bit149");
            var bit74 = packet.ReadBit("bit74");
            var bits164 = (int)packet.ReadBits(22);
            inviterGuid[3] = packet.ReadBit();
            inviterGuid[0] = packet.ReadBit();
            inviterGuid[4] = packet.ReadBit();
            inviterGuid[6] = packet.ReadBit();
            packet.ResetBitReader();

            packet.ReadXORByte(inviterGuid, 6);
            packet.ReadWoWString("realmName1", realmName1);
            packet.ReadXORByte(inviterGuid, 7);
            packet.ReadXORByte(inviterGuid, 2);
            packet.ReadXORByte(inviterGuid, 0);
            packet.ReadInt64("LowGuid?");
            packet.ReadInt32("RealmId?");
            packet.ReadInt32("Int14C");
            packet.ReadXORByte(inviterGuid, 1);
            packet.ReadXORByte(inviterGuid, 5);
            packet.ReadWoWString("realmName2", realmName2);
            packet.ReadXORByte(inviterGuid, 4);
            packet.ReadInt32("Int180");
            packet.ReadWoWString("inviterName", inviterName);
            packet.ReadXORByte(inviterGuid, 3);
            packet.ReadInt32("unk");
            for (var i = 0; i < bits164; ++i)
                packet.ReadInt32("IntEA", i);

            packet.WriteGuid("inviterGuid", inviterGuid);
        }

        [Parser(Opcode.CMSG_GROUP_INVITE_RESPONSE)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.ReadByte("Byte11");
            var bit18 = packet.ReadBit();
            packet.ReadBit("Accept");
            if (bit18)
                packet.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var leaderGUID = new byte[8];
            var looterGUID = new byte[8];
            var groupGUID = new byte[8];

            var bit7C = false;
            var bit87 = false;

            groupGUID[0] = packet.ReadBit();
            leaderGUID[7] = packet.ReadBit();
            leaderGUID[1] = packet.ReadBit();
            var hasInstanceDifficulty = packet.ReadBit();
            groupGUID[7] = packet.ReadBit();
            leaderGUID[6] = packet.ReadBit();
            leaderGUID[5] = packet.ReadBit();
            var memberCounter = (int)packet.ReadBits(21);

            var memberName = new uint[memberCounter];
            var memberGUID = new byte[memberCounter][];
            for (var i = 0; i < memberCounter; ++i)
            {
                memberGUID[i] = new byte[8];

                memberGUID[i][1] = packet.ReadBit();
                memberGUID[i][2] = packet.ReadBit();
                memberGUID[i][5] = packet.ReadBit();
                memberGUID[i][6] = packet.ReadBit();
                memberName[i] = packet.ReadBits(6);
                memberGUID[i][7] = packet.ReadBit();
                memberGUID[i][3] = packet.ReadBit();
                memberGUID[i][0] = packet.ReadBit();
                memberGUID[i][4] = packet.ReadBit();
            }

            leaderGUID[3] = packet.ReadBit();
            leaderGUID[0] = packet.ReadBit();
            var hasLootMode = packet.ReadBit();
            groupGUID[5] = packet.ReadBit();

            if (hasLootMode)
            {
                looterGUID[6] = packet.ReadBit();
                looterGUID[4] = packet.ReadBit();
                looterGUID[5] = packet.ReadBit();
                looterGUID[2] = packet.ReadBit();
                looterGUID[1] = packet.ReadBit();
                looterGUID[0] = packet.ReadBit();
                looterGUID[7] = packet.ReadBit();
                looterGUID[3] = packet.ReadBit();
            }
            groupGUID[2] = packet.ReadBit();
            groupGUID[4] = packet.ReadBit();
            groupGUID[1] = packet.ReadBit();

            var bit88 = packet.ReadBit();
            leaderGUID[2] = packet.ReadBit();
            groupGUID[6] = packet.ReadBit();
            if (bit88)
            {
                bit87 = packet.ReadBit();
                bit7C = packet.ReadBit();
            }

            leaderGUID[4] = packet.ReadBit();
            groupGUID[3] = packet.ReadBit();

            packet.ResetBitReader();

            packet.ReadXORByte(leaderGUID, 0);
            if (hasInstanceDifficulty)
            {
                packet.ReadInt32("raidDifficulty");
                packet.ReadInt32("dungeonDifficulty");
            }

            for (var i = 0; i < memberCounter; ++i)
            {
                packet.ReadXORByte(memberGUID[i], 6);
                packet.ReadXORByte(memberGUID[i], 3);
                packet.ReadByte("LFGrole", i);
                packet.ReadByte("onlineState", i);
                packet.ReadXORByte(memberGUID[i], 7);
                packet.ReadXORByte(memberGUID[i], 4);
                packet.ReadXORByte(memberGUID[i], 1);
                packet.ReadWoWString("MemberName", memberName[i], i);
                packet.ReadXORByte(memberGUID[i], 5);
                packet.ReadXORByte(memberGUID[i], 2);
                packet.ReadByte("groupID", i);
                packet.ReadXORByte(memberGUID[i], 0);
                packet.ReadByte("Flags", i);
                packet.WriteGuid("memberGUID", memberGUID[i], i);
            }
            packet.ReadXORByte(groupGUID, 1);

            if (bit88)
            {
                packet.ReadSingle("Float80");
                packet.ReadByte("Byte85");
                packet.ReadByte("Byte7D");
                packet.ReadInt32("Int74");
                packet.ReadByte("Byte84");
                packet.ReadByte("Byte70");
                packet.ReadByte("Byte86");
                packet.ReadInt32("Int78");
            }

            packet.ReadXORByte(leaderGUID, 4);
            packet.ReadXORByte(leaderGUID, 2);

            if (hasLootMode)
            {
                packet.ReadByte("lootMethod");
                packet.ReadXORByte(looterGUID, 0);
                packet.ReadXORByte(looterGUID, 5);
                packet.ReadXORByte(looterGUID, 4);
                packet.ReadXORByte(looterGUID, 3);
                packet.ReadXORByte(looterGUID, 2);
                packet.ReadByte("lootThreshold");
                packet.ReadXORByte(looterGUID, 7);
                packet.ReadXORByte(looterGUID, 1);
                packet.ReadXORByte(looterGUID, 6);
                packet.WriteGuid("looterGUID", looterGUID);
            }

            packet.ReadXORByte(groupGUID, 6);
            packet.ReadXORByte(groupGUID, 4);

            packet.ReadByte("groupType");
            packet.ReadByte("groupSlot"); // ??
            packet.ReadInt32("groupPosition");

            packet.ReadXORByte(groupGUID, 7);
            packet.ReadXORByte(leaderGUID, 3);
            packet.ReadXORByte(leaderGUID, 1);

            packet.ReadInt32("groupCounter");

            packet.ReadXORByte(groupGUID, 0);
            packet.ReadXORByte(groupGUID, 2);
            packet.ReadXORByte(groupGUID, 5);
            packet.ReadXORByte(groupGUID, 3);
            packet.ReadXORByte(leaderGUID, 7);

            packet.ReadByte("groupRole");

            packet.ReadXORByte(leaderGUID, 5);
            packet.ReadXORByte(leaderGUID, 6);

            packet.WriteGuid("leaderGUID", leaderGUID);
            packet.WriteGuid("groupGUID", groupGUID);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.ReadByte("Flags");
            var guid = new byte[8];
            packet.StartBitStream(guid, 7, 4, 0, 1, 3, 6, 2, 5);
            packet.ReadXORBytes(guid, 3, 6, 5, 2, 1, 4, 0, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PARTY_COMMAND_RESULT)]
        public static void HandlePartyCommandResult(Packet packet)
        {
            packet.ReadUInt32E<PartyCommand>("Command");
            packet.ReadCString("Member");
            packet.ReadUInt32E<PartyResult>("Result");
            packet.ReadUInt32("LFG Boot Cooldown");
            packet.ReadGuid("Player Guid"); // Usually 0
        }

        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            packet.ReadCString("Player");
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 5);
            packet.ReadBit("unk1"); // Add arena opponent ?
            packet.StartBitStream(guid, 1, 4);
            packet.ReadBit("unk2");
            packet.StartBitStream(guid, 6, 2, 7, 3);

            packet.ParseBitStream(guid, 3, 2, 6, 7, 5);
            var updateFlags = packet.ReadUInt32E<GroupUpdateFlag548>("Update Flags");
            packet.ParseBitStream(guid, 1, 4, 0);

            packet.WriteGuid("Guid", guid);

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var updateFlagPacket = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            if (updateFlags.HasFlag(GroupUpdateFlag548.Status)) // 0x1
                updateFlagPacket.ReadInt16E<GroupMemberStatusFlag>("Status");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk2)) // 0x2
            {
                for (var i = 0; i < 2; i++)
                    updateFlagPacket.ReadByte("Unk2", i);
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.CurrentHealth)) // 0x4
                updateFlagPacket.ReadUInt32("Current Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.MaxHealth)) // 0x8
                updateFlagPacket.ReadUInt32("Max Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PowerType)) // 0x10
                updateFlagPacket.ReadByteE<PowerType>("Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag548.CurrentPower)) // 0x20
                updateFlagPacket.ReadUInt16("Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Zone)) // 0x40
                updateFlagPacket.ReadUInt16<ZoneId>("Zone Id");

            if (updateFlags.HasFlag(GroupUpdateFlag548.MaxPower))// 0x80
                updateFlagPacket.ReadUInt16("Max Power");


            if (updateFlags.HasFlag(GroupUpdateFlag548.Level))// 0x100
                updateFlagPacket.ReadUInt16("Level");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk200)) // 0x200
                updateFlagPacket.ReadUInt16("Unk200");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk400)) // 0x400
                updateFlagPacket.ReadUInt16("Unk400");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Position)) // 0x800
            {
                updateFlagPacket.ReadInt16("Position X");
                updateFlagPacket.ReadInt16("Position Y");
                updateFlagPacket.ReadInt16("Position Z");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.Auras)) // 0x1000
            {
                updateFlagPacket.ReadByte("unkByte");
                var mask = updateFlagPacket.ReadUInt64("Aura Mask");
                var count = updateFlagPacket.ReadInt32("AuraCount");

                for (var i = 0; i < count; ++i)
                {
                    if (mask == 0) // bad packet
                        break;

                    if ((mask & (1ul << i)) == 0)
                        continue;

                    updateFlagPacket.ReadUInt32<SpellId>("Spell Id", i);
                    var flags = updateFlagPacket.ReadByteE<AuraFlagMoP>("Aura Flags", i);
                    var unk = updateFlagPacket.ReadUInt32("unk", i);

                    if (flags.HasFlag(AuraFlagMoP.Scalable))
                    {
                        var cnt = updateFlagPacket.ReadByte("Scalings");
                        for (int j = 0; j < cnt; j++)
                            updateFlagPacket.ReadSingle("Scale", i, j);
                    }
                }
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetGuid)) // 0x2000
                updateFlagPacket.ReadGuid("Pet GUID");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetName)) // 0x4000
                updateFlagPacket.ReadCString("Pet Name");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetModelId)) // 0x8000
                updateFlagPacket.ReadInt16("Pet Modelid");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetCurrentHealth)) // 0x10000
                updateFlagPacket.ReadInt32("Pet Current Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetMaxHealth)) // 0x20000
                updateFlagPacket.ReadInt32("Pet Max Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetPowerType)) // 0x40000
                updateFlagPacket.ReadByteE<PowerType>("Pet Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetCurrentPower)) // 0x80000
                updateFlagPacket.ReadInt16("Pet Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetMaxPower)) // 0x100000
                updateFlagPacket.ReadInt16("Pet Max Power");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk200000)) // 0x200000
                updateFlagPacket.ReadInt16("Unk200000");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetAuras)) // 0x400000
            {
                updateFlagPacket.ReadByte("unkByte");
                var mask = updateFlagPacket.ReadUInt64("Aura Mask");
                var count = updateFlagPacket.ReadInt32("AuraCount");

                for (var i = 0; i < count; ++i)
                {
                    if ((mask & (1ul << i)) == 0)
                        continue;

                    updateFlagPacket.ReadUInt32<SpellId>("Spell Id", i);
                    var flags = updateFlagPacket.ReadByteE<AuraFlagMoP>("Aura Flags", i);
                    updateFlagPacket.ReadUInt32("unk", i);

                    if (flags.HasFlag(AuraFlagMoP.Scalable))
                    {
                        var cnt = updateFlagPacket.ReadByte("Scalings", i);
                        for (int j = 0; j < cnt; j++)
                            updateFlagPacket.ReadSingle("Scale", i, j);
                    }
                }
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.VehicleSeat)) // 0x800000
                updateFlagPacket.ReadInt32("Vehicle Seat");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Phase)) // 0x1000000
            {
                updateFlagPacket.ReadInt32("Unk Int32");

                var count = updateFlagPacket.ReadBits("Phase Count", 23);
                for (var i = 0; i < count; ++i)
                    updateFlagPacket.ReadUInt16("Phase Id");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk2000000)) // 0x2000000
                updateFlagPacket.ReadInt16("Unk2000000");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk4000000)) // 0x4000000
                updateFlagPacket.ReadInt32("Unk4000000");

            updateFlagPacket.ClosePacket(false);
        }

        [Parser(Opcode.SMSG_GROUP_SET_ROLE)]
        public static void HandleGroupSetRole(Packet packet)
        {
            var assignerGuid = new byte[8];
            var targetGuid = new byte[8];

            assignerGuid[1] = packet.ReadBit();
            targetGuid[7] = packet.ReadBit();
            targetGuid[6] = packet.ReadBit();
            targetGuid[4] = packet.ReadBit();
            targetGuid[1] = packet.ReadBit();
            targetGuid[0] = packet.ReadBit();
            assignerGuid[0] = packet.ReadBit();
            assignerGuid[7] = packet.ReadBit();
            targetGuid[3] = packet.ReadBit();
            assignerGuid[6] = packet.ReadBit();
            targetGuid[2] = packet.ReadBit();
            assignerGuid[4] = packet.ReadBit();
            assignerGuid[5] = packet.ReadBit();
            assignerGuid[2] = packet.ReadBit();
            targetGuid[5] = packet.ReadBit();
            assignerGuid[3] = packet.ReadBit();
            packet.ReadXORByte(assignerGuid, 1);
            packet.ReadXORByte(assignerGuid, 6);
            packet.ReadXORByte(assignerGuid, 2);
            packet.ReadXORByte(targetGuid, 3);
            packet.ReadInt32E<LfgRoleFlag>("Old Roles");
            packet.ReadXORByte(assignerGuid, 7);
            packet.ReadXORByte(targetGuid, 5);
            packet.ReadXORByte(assignerGuid, 3);
            packet.ReadXORByte(targetGuid, 4);
            packet.ReadXORByte(targetGuid, 7);
            packet.ReadXORByte(assignerGuid, 5);
            packet.ReadXORByte(targetGuid, 6);
            packet.ReadXORByte(targetGuid, 2);
            packet.ReadXORByte(targetGuid, 1);
            packet.ReadXORByte(targetGuid, 0);
            packet.ReadXORByte(assignerGuid, 4);
            packet.ReadByte("Byte28");
            packet.ReadXORByte(assignerGuid, 0);
            packet.ReadInt32E<LfgRoleFlag>("New Roles");

            packet.WriteGuid("Guid2", assignerGuid);
            packet.WriteGuid("Guid3", targetGuid);

        }
    }
}
