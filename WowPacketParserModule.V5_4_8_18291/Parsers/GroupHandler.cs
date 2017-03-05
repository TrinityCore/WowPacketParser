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

            packet.Translator.ReadInt32("Int114");
            packet.Translator.ReadByte("Byte118");
            packet.Translator.ReadInt32("Int128");
            crossRealmGuid[7] = packet.Translator.ReadBit();
            var realmNameLen = packet.Translator.ReadBits(9);
            crossRealmGuid[3] = packet.Translator.ReadBit();
            var nameLen = packet.Translator.ReadBits(9);
            crossRealmGuid[2] = packet.Translator.ReadBit();
            crossRealmGuid[5] = packet.Translator.ReadBit();
            crossRealmGuid[4] = packet.Translator.ReadBit();
            crossRealmGuid[0] = packet.Translator.ReadBit();
            crossRealmGuid[1] = packet.Translator.ReadBit();
            crossRealmGuid[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(crossRealmGuid, 7);
            packet.Translator.ReadXORByte(crossRealmGuid, 6);
            packet.Translator.ReadXORByte(crossRealmGuid, 0);
            packet.Translator.ReadXORByte(crossRealmGuid, 4);
            packet.Translator.ReadWoWString("Name", nameLen);
            packet.Translator.ReadXORByte(crossRealmGuid, 1);
            packet.Translator.ReadXORByte(crossRealmGuid, 2);
            packet.Translator.ReadXORByte(crossRealmGuid, 3);
            packet.Translator.ReadWoWString("Realm Name", realmNameLen);
            packet.Translator.ReadXORByte(crossRealmGuid, 5);

            packet.Translator.WriteGuid("crossRealmGuid", crossRealmGuid);
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var inviterGuid = new byte[8];

            var realmName1 = (int)packet.Translator.ReadBits(8);
            var realmName2 = (int)packet.Translator.ReadBits(8);
            inviterGuid[2] = packet.Translator.ReadBit();
            var bit160 = packet.Translator.ReadBit("bit160");
            var inviterName = (int)packet.Translator.ReadBits(6);
            inviterGuid[7] = packet.Translator.ReadBit();
            inviterGuid[5] = packet.Translator.ReadBit();
            var bit150 = packet.Translator.ReadBit("bit150");
            var bit148 = packet.Translator.ReadBit("bit148");
            inviterGuid[1] = packet.Translator.ReadBit();
            var bit149 = packet.Translator.ReadBit("bit149");
            var bit74 = packet.Translator.ReadBit("bit74");
            var bits164 = (int)packet.Translator.ReadBits(22);
            inviterGuid[3] = packet.Translator.ReadBit();
            inviterGuid[0] = packet.Translator.ReadBit();
            inviterGuid[4] = packet.Translator.ReadBit();
            inviterGuid[6] = packet.Translator.ReadBit();
            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORByte(inviterGuid, 6);
            packet.Translator.ReadWoWString("realmName1", realmName1);
            packet.Translator.ReadXORByte(inviterGuid, 7);
            packet.Translator.ReadXORByte(inviterGuid, 2);
            packet.Translator.ReadXORByte(inviterGuid, 0);
            packet.Translator.ReadInt64("LowGuid?");
            packet.Translator.ReadInt32("RealmId?");
            packet.Translator.ReadInt32("Int14C");
            packet.Translator.ReadXORByte(inviterGuid, 1);
            packet.Translator.ReadXORByte(inviterGuid, 5);
            packet.Translator.ReadWoWString("realmName2", realmName2);
            packet.Translator.ReadXORByte(inviterGuid, 4);
            packet.Translator.ReadInt32("Int180");
            packet.Translator.ReadWoWString("inviterName", inviterName);
            packet.Translator.ReadXORByte(inviterGuid, 3);
            packet.Translator.ReadInt32("unk");
            for (var i = 0; i < bits164; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.WriteGuid("inviterGuid", inviterGuid);
        }

        [Parser(Opcode.CMSG_GROUP_INVITE_RESPONSE)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.Translator.ReadByte("Byte11");
            var bit18 = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Accept");
            if (bit18)
                packet.Translator.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var leaderGUID = new byte[8];
            var looterGUID = new byte[8];
            var groupGUID = new byte[8];

            var bit7C = false;
            var bit87 = false;

            groupGUID[0] = packet.Translator.ReadBit();
            leaderGUID[7] = packet.Translator.ReadBit();
            leaderGUID[1] = packet.Translator.ReadBit();
            var hasInstanceDifficulty = packet.Translator.ReadBit();
            groupGUID[7] = packet.Translator.ReadBit();
            leaderGUID[6] = packet.Translator.ReadBit();
            leaderGUID[5] = packet.Translator.ReadBit();
            var memberCounter = (int)packet.Translator.ReadBits(21);

            var memberName = new uint[memberCounter];
            var memberGUID = new byte[memberCounter][];
            for (var i = 0; i < memberCounter; ++i)
            {
                memberGUID[i] = new byte[8];

                memberGUID[i][1] = packet.Translator.ReadBit();
                memberGUID[i][2] = packet.Translator.ReadBit();
                memberGUID[i][5] = packet.Translator.ReadBit();
                memberGUID[i][6] = packet.Translator.ReadBit();
                memberName[i] = packet.Translator.ReadBits(6);
                memberGUID[i][7] = packet.Translator.ReadBit();
                memberGUID[i][3] = packet.Translator.ReadBit();
                memberGUID[i][0] = packet.Translator.ReadBit();
                memberGUID[i][4] = packet.Translator.ReadBit();
            }

            leaderGUID[3] = packet.Translator.ReadBit();
            leaderGUID[0] = packet.Translator.ReadBit();
            var hasLootMode = packet.Translator.ReadBit();
            groupGUID[5] = packet.Translator.ReadBit();

            if (hasLootMode)
            {
                looterGUID[6] = packet.Translator.ReadBit();
                looterGUID[4] = packet.Translator.ReadBit();
                looterGUID[5] = packet.Translator.ReadBit();
                looterGUID[2] = packet.Translator.ReadBit();
                looterGUID[1] = packet.Translator.ReadBit();
                looterGUID[0] = packet.Translator.ReadBit();
                looterGUID[7] = packet.Translator.ReadBit();
                looterGUID[3] = packet.Translator.ReadBit();
            }
            groupGUID[2] = packet.Translator.ReadBit();
            groupGUID[4] = packet.Translator.ReadBit();
            groupGUID[1] = packet.Translator.ReadBit();

            var bit88 = packet.Translator.ReadBit();
            leaderGUID[2] = packet.Translator.ReadBit();
            groupGUID[6] = packet.Translator.ReadBit();
            if (bit88)
            {
                bit87 = packet.Translator.ReadBit();
                bit7C = packet.Translator.ReadBit();
            }

            leaderGUID[4] = packet.Translator.ReadBit();
            groupGUID[3] = packet.Translator.ReadBit();

            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORByte(leaderGUID, 0);
            if (hasInstanceDifficulty)
            {
                packet.Translator.ReadInt32("raidDifficulty");
                packet.Translator.ReadInt32("dungeonDifficulty");
            }

            for (var i = 0; i < memberCounter; ++i)
            {
                packet.Translator.ReadXORByte(memberGUID[i], 6);
                packet.Translator.ReadXORByte(memberGUID[i], 3);
                packet.Translator.ReadByte("LFGrole", i);
                packet.Translator.ReadByte("onlineState", i);
                packet.Translator.ReadXORByte(memberGUID[i], 7);
                packet.Translator.ReadXORByte(memberGUID[i], 4);
                packet.Translator.ReadXORByte(memberGUID[i], 1);
                packet.Translator.ReadWoWString("MemberName", memberName[i], i);
                packet.Translator.ReadXORByte(memberGUID[i], 5);
                packet.Translator.ReadXORByte(memberGUID[i], 2);
                packet.Translator.ReadByte("groupID", i);
                packet.Translator.ReadXORByte(memberGUID[i], 0);
                packet.Translator.ReadByte("Flags", i);
                packet.Translator.WriteGuid("memberGUID", memberGUID[i], i);
            }
            packet.Translator.ReadXORByte(groupGUID, 1);

            if (bit88)
            {
                packet.Translator.ReadSingle("Float80");
                packet.Translator.ReadByte("Byte85");
                packet.Translator.ReadByte("Byte7D");
                packet.Translator.ReadInt32("Int74");
                packet.Translator.ReadByte("Byte84");
                packet.Translator.ReadByte("Byte70");
                packet.Translator.ReadByte("Byte86");
                packet.Translator.ReadInt32("Int78");
            }

            packet.Translator.ReadXORByte(leaderGUID, 4);
            packet.Translator.ReadXORByte(leaderGUID, 2);

            if (hasLootMode)
            {
                packet.Translator.ReadByte("lootMethod");
                packet.Translator.ReadXORByte(looterGUID, 0);
                packet.Translator.ReadXORByte(looterGUID, 5);
                packet.Translator.ReadXORByte(looterGUID, 4);
                packet.Translator.ReadXORByte(looterGUID, 3);
                packet.Translator.ReadXORByte(looterGUID, 2);
                packet.Translator.ReadByte("lootThreshold");
                packet.Translator.ReadXORByte(looterGUID, 7);
                packet.Translator.ReadXORByte(looterGUID, 1);
                packet.Translator.ReadXORByte(looterGUID, 6);
                packet.Translator.WriteGuid("looterGUID", looterGUID);
            }

            packet.Translator.ReadXORByte(groupGUID, 6);
            packet.Translator.ReadXORByte(groupGUID, 4);

            packet.Translator.ReadByte("groupType");
            packet.Translator.ReadByte("groupSlot"); // ??
            packet.Translator.ReadInt32("groupPosition");

            packet.Translator.ReadXORByte(groupGUID, 7);
            packet.Translator.ReadXORByte(leaderGUID, 3);
            packet.Translator.ReadXORByte(leaderGUID, 1);

            packet.Translator.ReadInt32("groupCounter");

            packet.Translator.ReadXORByte(groupGUID, 0);
            packet.Translator.ReadXORByte(groupGUID, 2);
            packet.Translator.ReadXORByte(groupGUID, 5);
            packet.Translator.ReadXORByte(groupGUID, 3);
            packet.Translator.ReadXORByte(leaderGUID, 7);

            packet.Translator.ReadByte("groupRole");

            packet.Translator.ReadXORByte(leaderGUID, 5);
            packet.Translator.ReadXORByte(leaderGUID, 6);

            packet.Translator.WriteGuid("leaderGUID", leaderGUID);
            packet.Translator.WriteGuid("groupGUID", groupGUID);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.Translator.ReadByte("Flags");
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 7, 4, 0, 1, 3, 6, 2, 5);
            packet.Translator.ReadXORBytes(guid, 3, 6, 5, 2, 1, 4, 0, 7);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PARTY_COMMAND_RESULT)]
        public static void HandlePartyCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32E<PartyCommand>("Command");
            packet.Translator.ReadCString("Member");
            packet.Translator.ReadUInt32E<PartyResult>("Result");
            packet.Translator.ReadUInt32("LFG Boot Cooldown");
            packet.Translator.ReadGuid("Player Guid"); // Usually 0
        }

        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            packet.Translator.ReadCString("Player");
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 5);
            packet.Translator.ReadBit("unk1"); // Add arena opponent ?
            packet.Translator.StartBitStream(guid, 1, 4);
            packet.Translator.ReadBit("unk2");
            packet.Translator.StartBitStream(guid, 6, 2, 7, 3);

            packet.Translator.ParseBitStream(guid, 3, 2, 6, 7, 5);
            var updateFlags = packet.Translator.ReadUInt32E<GroupUpdateFlag548>("Update Flags");
            packet.Translator.ParseBitStream(guid, 1, 4, 0);

            packet.Translator.WriteGuid("Guid", guid);

            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var updateFlagPacket = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);

            if (updateFlags.HasFlag(GroupUpdateFlag548.Status)) // 0x1
                updateFlagPacket.Translator.ReadInt16E<GroupMemberStatusFlag>("Status");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk2)) // 0x2
            {
                for (var i = 0; i < 2; i++)
                    updateFlagPacket.Translator.ReadByte("Unk2", i);
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.CurrentHealth)) // 0x4
                updateFlagPacket.Translator.ReadUInt32("Current Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.MaxHealth)) // 0x8
                updateFlagPacket.Translator.ReadUInt32("Max Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PowerType)) // 0x10
                updateFlagPacket.Translator.ReadByteE<PowerType>("Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag548.CurrentPower)) // 0x20
                updateFlagPacket.Translator.ReadUInt16("Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Zone)) // 0x40
                updateFlagPacket.Translator.ReadUInt16<ZoneId>("Zone Id");

            if (updateFlags.HasFlag(GroupUpdateFlag548.MaxPower))// 0x80
                updateFlagPacket.Translator.ReadUInt16("Max Power");


            if (updateFlags.HasFlag(GroupUpdateFlag548.Level))// 0x100
                updateFlagPacket.Translator.ReadUInt16("Level");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk200)) // 0x200
                updateFlagPacket.Translator.ReadUInt16("Unk200");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk400)) // 0x400
                updateFlagPacket.Translator.ReadUInt16("Unk400");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Position)) // 0x800
            {
                updateFlagPacket.Translator.ReadInt16("Position X");
                updateFlagPacket.Translator.ReadInt16("Position Y");
                updateFlagPacket.Translator.ReadInt16("Position Z");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.Auras)) // 0x1000
            {
                updateFlagPacket.Translator.ReadByte("unkByte");
                var mask = updateFlagPacket.Translator.ReadUInt64("Aura Mask");
                var count = updateFlagPacket.Translator.ReadInt32("AuraCount");

                for (var i = 0; i < count; ++i)
                {
                    if (mask == 0) // bad packet
                        break;

                    if ((mask & (1ul << i)) == 0)
                        continue;

                    updateFlagPacket.Translator.ReadUInt32<SpellId>("Spell Id", i);
                    var flags = updateFlagPacket.Translator.ReadByteE<AuraFlagMoP>("Aura Flags", i);
                    var unk = updateFlagPacket.Translator.ReadUInt32("unk", i);

                    if (flags.HasFlag(AuraFlagMoP.Scalable))
                    {
                        var cnt = updateFlagPacket.Translator.ReadByte("Scalings");
                        for (int j = 0; j < cnt; j++)
                            updateFlagPacket.Translator.ReadSingle("Scale", i, j);
                    }
                }
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetGuid)) // 0x2000
                updateFlagpacket.Translator.ReadGuid("Pet GUID");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetName)) // 0x4000
                updateFlagpacket.Translator.ReadCString("Pet Name");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetModelId)) // 0x8000
                updateFlagPacket.Translator.ReadInt16("Pet Modelid");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetCurrentHealth)) // 0x10000
                updateFlagPacket.Translator.ReadInt32("Pet Current Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetMaxHealth)) // 0x20000
                updateFlagPacket.Translator.ReadInt32("Pet Max Health");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetPowerType)) // 0x40000
                updateFlagPacket.Translator.ReadByteE<PowerType>("Pet Power type");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetCurrentPower)) // 0x80000
                updateFlagPacket.Translator.ReadInt16("Pet Current Power");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetMaxPower)) // 0x100000
                updateFlagPacket.Translator.ReadInt16("Pet Max Power");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk200000)) // 0x200000
                updateFlagPacket.Translator.ReadInt16("Unk200000");

            if (updateFlags.HasFlag(GroupUpdateFlag548.PetAuras)) // 0x400000
            {
                updateFlagPacket.Translator.ReadByte("unkByte");
                var mask = updateFlagPacket.Translator.ReadUInt64("Aura Mask");
                var count = updateFlagPacket.Translator.ReadInt32("AuraCount");

                for (var i = 0; i < count; ++i)
                {
                    if ((mask & (1ul << i)) == 0)
                        continue;

                    updateFlagPacket.Translator.ReadUInt32<SpellId>("Spell Id", i);
                    var flags = updateFlagPacket.Translator.ReadByteE<AuraFlagMoP>("Aura Flags", i);
                    updateFlagPacket.Translator.ReadUInt32("unk", i);

                    if (flags.HasFlag(AuraFlagMoP.Scalable))
                    {
                        var cnt = updateFlagPacket.Translator.ReadByte("Scalings", i);
                        for (int j = 0; j < cnt; j++)
                            updateFlagPacket.Translator.ReadSingle("Scale", i, j);
                    }
                }
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.VehicleSeat)) // 0x800000
                updateFlagPacket.Translator.ReadInt32("Vehicle Seat");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Phase)) // 0x1000000
            {
                updateFlagPacket.Translator.ReadInt32("Unk Int32");

                var count = updateFlagpacket.Translator.ReadBits("Phase Count", 23);
                for (var i = 0; i < count; ++i)
                    updateFlagPacket.Translator.ReadUInt16("Phase Id");
            }

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk2000000)) // 0x2000000
                updateFlagPacket.Translator.ReadInt16("Unk2000000");

            if (updateFlags.HasFlag(GroupUpdateFlag548.Unk4000000)) // 0x4000000
                updateFlagPacket.Translator.ReadInt32("Unk4000000");

            updateFlagPacket.ClosePacket(false);
        }

        [Parser(Opcode.SMSG_GROUP_SET_ROLE)]
        public static void HandleGroupSetRole(Packet packet)
        {
            var assignerGuid = new byte[8];
            var targetGuid = new byte[8];

            assignerGuid[1] = packet.Translator.ReadBit();
            targetGuid[7] = packet.Translator.ReadBit();
            targetGuid[6] = packet.Translator.ReadBit();
            targetGuid[4] = packet.Translator.ReadBit();
            targetGuid[1] = packet.Translator.ReadBit();
            targetGuid[0] = packet.Translator.ReadBit();
            assignerGuid[0] = packet.Translator.ReadBit();
            assignerGuid[7] = packet.Translator.ReadBit();
            targetGuid[3] = packet.Translator.ReadBit();
            assignerGuid[6] = packet.Translator.ReadBit();
            targetGuid[2] = packet.Translator.ReadBit();
            assignerGuid[4] = packet.Translator.ReadBit();
            assignerGuid[5] = packet.Translator.ReadBit();
            assignerGuid[2] = packet.Translator.ReadBit();
            targetGuid[5] = packet.Translator.ReadBit();
            assignerGuid[3] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(assignerGuid, 1);
            packet.Translator.ReadXORByte(assignerGuid, 6);
            packet.Translator.ReadXORByte(assignerGuid, 2);
            packet.Translator.ReadXORByte(targetGuid, 3);
            packet.Translator.ReadInt32E<LfgRoleFlag>("Old Roles");
            packet.Translator.ReadXORByte(assignerGuid, 7);
            packet.Translator.ReadXORByte(targetGuid, 5);
            packet.Translator.ReadXORByte(assignerGuid, 3);
            packet.Translator.ReadXORByte(targetGuid, 4);
            packet.Translator.ReadXORByte(targetGuid, 7);
            packet.Translator.ReadXORByte(assignerGuid, 5);
            packet.Translator.ReadXORByte(targetGuid, 6);
            packet.Translator.ReadXORByte(targetGuid, 2);
            packet.Translator.ReadXORByte(targetGuid, 1);
            packet.Translator.ReadXORByte(targetGuid, 0);
            packet.Translator.ReadXORByte(assignerGuid, 4);
            packet.Translator.ReadByte("Byte28");
            packet.Translator.ReadXORByte(assignerGuid, 0);
            packet.Translator.ReadInt32E<LfgRoleFlag>("New Roles");

            packet.Translator.WriteGuid("Guid2", assignerGuid);
            packet.Translator.WriteGuid("Guid3", targetGuid);

        }
    }
}
