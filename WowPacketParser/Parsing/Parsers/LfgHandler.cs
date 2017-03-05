using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.CMSG_LFG_JOIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgJoin(Packet packet)
        {
            packet.Translator.ReadInt32E<LfgRoleFlag>("Roles");
            packet.Translator.ReadBytes(2); // always 0
            var numFields = packet.Translator.ReadByte("Join Dungeon Count");
            for (var i = 0; i < numFields; i++)
                packet.Translator.ReadLfgEntry("Dungeon Entry", i);

            packet.Translator.ReadUInt32(); // always 0 (for 1..3) 0
            packet.Translator.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_LFG_JOIN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgJoin434(Packet packet)
        {
            packet.Translator.ReadInt32E<LfgRoleFlag>("Roles");

            for (var i = 0; i < 3; i++)
                packet.Translator.ReadInt32("Unk Int32", i);

            var length = packet.Translator.ReadBits(9);
            var count = packet.Translator.ReadBits("Join Dungeon Count", 24);

            packet.Translator.ReadWoWString("Comment", length);

            for (var i = 0; i < count; i++)
                packet.Translator.ReadLfgEntry("Dungeon Entry", i);
        }

        [Parser(Opcode.CMSG_LFG_LEAVE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgLeave(Packet packet)
        {
            packet.Translator.ReadInt32E<LfgRoleFlag>("Roles");
            packet.Translator.ReadTime("Time");
            packet.Translator.ReadUInt32("Reason?");
            packet.Translator.ReadUInt32("Instance Id");

            var guid = packet.Translator.StartBitStream(4, 5, 0, 6, 2, 7, 1, 3);
            packet.Translator.ParseBitStream(guid, 7, 4, 3, 2, 6, 0, 1, 5);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_LFG_SET_COMMENT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgComment(Packet packet)
        {
            packet.Translator.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_LFG_SET_COMMENT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgComment434(Packet packet)
        {
            var length = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Comment", length);
        }

        [Parser(Opcode.CMSG_LFG_SET_BOOT_VOTE)]
        public static void HandleLfgSetBootVote(Packet packet)
        {
            packet.Translator.ReadBool("Agree");
        }

        [Parser(Opcode.CMSG_LFG_PROPOSAL_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalResult(Packet packet)
        {
            packet.Translator.ReadInt32("Group ID");
            packet.Translator.ReadBool("Accept");
        }

        [Parser(Opcode.CMSG_LFG_PROPOSAL_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalResult434(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk Uint32");
            packet.Translator.ReadTime("Time");
            packet.Translator.ReadInt32E<LfgRoleFlag>("Roles");
            packet.Translator.ReadUInt32("Unk Uint32");

            var guid2 = packet.Translator.StartBitStream(4, 5, 0, 6, 2, 7, 1, 3);
            packet.Translator.ParseBitStream(guid2, 7, 4, 3, 2, 6, 0, 1, 5);
            packet.Translator.WriteGuid("Player Guid", guid2);

            var guid = new byte[8];
            guid[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Accept");
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 7, 1, 5, 6, 3, 4, 0, 2);
            packet.Translator.WriteGuid("Instance Guid", guid);

        }

        [Parser(Opcode.SMSG_LFG_BOOT_PROPOSAL_UPDATE)]
        public static void HandleLfgBootProposalUpdate(Packet packet)
        {
            packet.Translator.ReadBool("In Progress");
            packet.Translator.ReadBool("Did Vote");
            packet.Translator.ReadBool("Vote");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadByte("Offline/afk");
            packet.Translator.ReadGuid("Victim GUID");
            packet.Translator.ReadInt32("Total Votes");
            packet.Translator.ReadInt32("Agree Count");
            packet.Translator.ReadInt32("Time Left");
            packet.Translator.ReadInt32("Needed Votes");
            packet.Translator.ReadCString("Comment");
        }

        public static void ReadLfgRewardBlock(Packet packet, object index)
        {
            packet.Translator.ReadBool("First Completion", index);

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_LFG_PLAYER_REWARD, Direction.ServerToClient))
                packet.Translator.ReadInt32("Strangers", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                packet.Translator.ReadInt32("currencyQuantity", index);
                packet.Translator.ReadInt32("Unk 2", index);
                packet.Translator.ReadInt32("currencyID", index);
                packet.Translator.ReadInt32("tier1Quantity", index);
                packet.Translator.ReadInt32("tier1Limit", index);
                packet.Translator.ReadInt32("overallQuantity", index);
                packet.Translator.ReadInt32("overallLimit", index);
                packet.Translator.ReadInt32("periodPurseQuantity", index);
                packet.Translator.ReadInt32("periodPurseLimit", index);
                packet.Translator.ReadInt32("purseQuantity", index);
                packet.Translator.ReadInt32("purseLimit", index);
                packet.Translator.ReadInt32("Unk 4", index);
                packet.Translator.ReadInt32("completedEncounters", index);

                packet.Translator.ReadByte("Call to Arms eligible", index);

                // LFG_SLOT_INFO_LOOT related
                for (var i = 0; i < 3; ++i)
                {
                    var unk1 = packet.Translator.ReadInt32E<LfgRoleFlag>("Call to Arms Role", index, i);
                    if (unk1 != 0)
                    {
                        packet.Translator.ReadInt32("Call to Arms Money", index, i);
                        packet.Translator.ReadInt32("Call to Arms XP", index, i);
                        var unk4 = packet.Translator.ReadByte("Call to Arms Item Count", index, i);
                        for (var j = 0; j < unk4; ++j)
                        {
                            packet.Translator.ReadInt32<ItemId>("Call to Arms Item Or Currency Id", index, i, j);
                            packet.Translator.ReadInt32("Call to Arms Item Display ID", index, i, j);
                            packet.Translator.ReadInt32("Call to Arms Item Stack Count", index, i, j);
                            packet.Translator.ReadBool("Call to Arms Is Currency", index, i, j);
                        }
                    }
                }
            }

            packet.Translator.ReadInt32("Base Money", index);
            packet.Translator.ReadInt32("Base XP", index);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
            {
                packet.Translator.ReadInt32("Variable Money", index);
                packet.Translator.ReadInt32("Variable XP", index);
            }

            var numFields = packet.Translator.ReadByte("Reward Item Count", index);
            for (var i = 0; i < numFields; i++)
            {
                packet.Translator.ReadInt32<ItemId>("Reward Item Or Currency Id", index, i);
                packet.Translator.ReadInt32("Reward Item Display ID", index, i);
                packet.Translator.ReadInt32("Reward Item Stack Count", index, i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.Translator.ReadBool("Is Currency", index, i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgCompletionReward(Packet packet)
        {
            packet.Translator.ReadLfgEntry("Random LFG Entry");
            packet.Translator.ReadLfgEntry("Actual LFG Entry");

            ReadLfgRewardBlock(packet, -1);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgCompletionReward434(Packet packet)
        {
            packet.Translator.ReadLfgEntry("Random LFG Entry");
            packet.Translator.ReadLfgEntry("Actual LFG Entry");
            packet.Translator.ReadUInt32("Base Money");
            packet.Translator.ReadUInt32("Base XP");

            var numFields = packet.Translator.ReadByte("Reward Item Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.Translator.ReadInt32<ItemId>("Reward Item Or Currency Id", i);
                packet.Translator.ReadInt32("Reward Item Display ID", i);
                packet.Translator.ReadInt32("Reward Item Stack Count", i);
                packet.Translator.ReadBool("Is Currency", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PLAYER)]
        public static void HandleLfgUpdatePlayer(Packet packet)
        {
            packet.Translator.ReadByteE<LfgUpdateType>("Update Type");
            var extraInfo = packet.Translator.ReadBool("Extra Info");
            if (!extraInfo)
                return;

            packet.Translator.ReadBool("Queued");
            packet.Translator.ReadBytes(2); // always 0
            var numFields2 = packet.Translator.ReadByte("Slot Count");
            for (var i = 0; i < numFields2; i++)
                packet.Translator.ReadLfgEntry("LFG Entry", i);

            packet.Translator.ReadCString("Comment");
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PARTY)]
        public static void HandleLfgUpdateParty(Packet packet)
        {
            packet.Translator.ReadByteE<LfgUpdateType>("Update Type");
            var extraInfo = packet.Translator.ReadBool("Extra Info");
            if (!extraInfo)
                return;

            packet.Translator.ReadBool("LFG Joined");
            packet.Translator.ReadBool("Queued");
            packet.Translator.ReadBytes(5); // always 0, 0, for (1..3) 0
            var numFields2 = packet.Translator.ReadByte("Slot Count");
            for (var i = 0; i < numFields2; i++)
                packet.Translator.ReadLfgEntry("LFG Entry", i);

            packet.Translator.ReadCString("Comment");
        }

        [Parser(Opcode.SMSG_LFG_OFFER_CONTINUE)]
        public static void HandleLfgOfferContinue(Packet packet)
        {
            packet.Translator.ReadLfgEntry("LFG Entry");
        }

        public static void ReadDungeonJoinResults(Packet packet, params int[] values)
        {
            packet.Translator.ReadLfgEntry("LFG Entry", values);
            packet.Translator.ReadUInt32E<LfgEntryCheckResult>("Entry Check Result", values);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
            {
                packet.Translator.ReadInt32("needed ILvL", values);
                packet.Translator.ReadInt32("player ILvL", values);
            }
        }

        public static void ReadPlayerLockedDungeons(Packet packet, int i)
        {
            var numFields = packet.Translator.ReadInt32("Entry Count", i);
            for (var j = 0; j < numFields; j++)
                ReadDungeonJoinResults(packet, i, j);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var numFields = packet.Translator.ReadByte("Random Dungeon Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.Translator.ReadLfgEntry("Random Dungeon Entry", i);
                ReadLfgRewardBlock(packet, i);
            }

            ReadPlayerLockedDungeons(packet, -1);
        }

        [Parser(Opcode.SMSG_LFG_PARTY_INFO)]
        public static void HandleLfgPlayerLockInfoUpdate(Packet packet)
        {
            var numFields = packet.Translator.ReadByte("Player Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.Translator.ReadGuid("GUID", i);
                ReadPlayerLockedDungeons(packet, i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            packet.Translator.ReadLfgEntry("LFG Entry");
            packet.Translator.ReadByteE<LfgProposalState>("State");
            packet.Translator.ReadInt32("Group ID");
            packet.Translator.ReadInt32("Bosses Killed Mask");
            packet.Translator.ReadBool("Silent");

            var numFields = packet.Translator.ReadByte("Response Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.Translator.ReadInt32E<LfgRoleFlag>("Roles", i);
                packet.Translator.ReadBool("Self", i);
                packet.Translator.ReadBool("In Dungeon", i);
                packet.Translator.ReadBool("Same Group", i);
                packet.Translator.ReadBool("Answer", i);
                packet.Translator.ReadBool("Accept", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalUpdate434(Packet packet)
        {
            packet.Translator.ReadTime("Date");
            packet.Translator.ReadInt32("Bosses Killed Mask");
            packet.Translator.ReadInt32("Unk UInt32 1");
            packet.Translator.ReadUInt32("Unk UInt32 2");
            packet.Translator.ReadLfgEntry("LFG Entry");
            packet.Translator.ReadUInt32("Unk UInt32 3");
            packet.Translator.ReadByteE<LfgProposalState>("State");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[4] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Silent");
            guid1[4] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();

            uint count = packet.Translator.ReadBits("Response Count", 23);

            guid2[7] = packet.Translator.ReadBit();

            for (int i = 0; i < count; ++i)
            {
                var bits = new Bit[5];
                for (int j = 0; j < 5; ++j)
                    bits[j] = packet.Translator.ReadBit();
                packet.AddValue("Bits",
                    $"In Dungeon?: {bits[0]}, Same Group?: {bits[1]}, Accept: {bits[2]}, Answer: {bits[3]}, Self: {bits[4]}", i); // 0 and 1 could be swapped
            }

            guid2[5] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 1);

            for (int i = 0; i < count; ++i)
                packet.Translator.ReadInt32E<LfgRoleFlag>("Roles", i);

            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 4);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_STATUS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgUpdateStatus(Packet packet)
        {
            var guid = new byte[8];

            guid[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Unk Bit 65");
            var count = packet.Translator.ReadBits("Dungeon Count", 24);
            guid[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Joined");
            var length = packet.Translator.ReadBits(9);
            guid[4] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("LFGJoined");
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Queued");
            packet.Translator.ReadByte("Unk Byte 64");
            packet.Translator.ReadWoWString("Comment", length);
            packet.Translator.ReadUInt32("Queue Id");
            packet.Translator.ReadTime("Join Date");

            packet.Translator.ReadXORByte(guid, 6);

            for (var i = 0; i < 3; ++i)
                packet.Translator.ReadByte("Unk Byte", i); // always 0

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.ReadInt32("Unk_UInt32_1"); // Same value than "Unk_UInt32_1" in SMSG_LFG_JOIN_RESULT - Only seen 3

            packet.Translator.ReadXORByte(guid, 7);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadLfgEntry("Dungeon Entry", i);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            packet.Translator.ReadLfgEntry("LFG Entry");
            packet.Translator.ReadInt32("Average Wait Time");
            packet.Translator.ReadInt32("Wait Time");
            packet.Translator.ReadInt32("Wait Tanks");
            packet.Translator.ReadInt32("Wait Headers");
            packet.Translator.ReadInt32("Wait DPS");
            packet.Translator.ReadByte("Number of Tanks Needed");
            packet.Translator.ReadByte("Number of Healers Needed");
            packet.Translator.ReadByte("Number of Damage Dealers Needed");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.Translator.ReadInt32("Queued Time");
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgQueueStatusUpdate434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 0, 2, 6, 5, 7, 1, 4);

            packet.Translator.ReadXORByte(guid, 0);

            //for (var i = 0; i < 3; ++i) byte uint32
            packet.Translator.ReadByte("Tank Unk");
            packet.Translator.ReadInt32("Tank Time");
            packet.Translator.ReadByte("Healer Unk");
            packet.Translator.ReadInt32("Healer Time");
            packet.Translator.ReadByte("Damage Unk");
            packet.Translator.ReadInt32("Damage Time");

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.ReadInt32("Average Wait Time");
            packet.Translator.ReadTime("Join Time");
            packet.Translator.ReadLfgEntry("LFG Entry");
            packet.Translator.ReadInt32("Queued Time");

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.ReadInt32("Queue Id");

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.ReadInt32("Wait Time"); // Matches "Role Unk2"
            packet.Translator.ReadInt32("Unk_UInt32_1"); // Same value than "Unk_UInt32_1" in SMSG_LFG_JOIN_RESULT - Only seen 3
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.Translator.ReadInt32E<LfgRoleCheckStatus>("Role Check Status");
            packet.Translator.ReadBool("Is Beginning");

            var numFields = packet.Translator.ReadByte("Entry Count");
            for (var i = 0; i < numFields; i++)
                packet.Translator.ReadLfgEntry("LFG Entry", i);

            var numFields2 = packet.Translator.ReadByte("Player Count");
            for (var i = 0; i < numFields2; i++)
            {
                packet.Translator.ReadGuid("GUID", i);
                packet.Translator.ReadBool("Ready", i);
                packet.Translator.ReadInt32E<LfgRoleFlag>("Roles", i);
                packet.Translator.ReadByte("Level", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            var result = packet.Translator.ReadInt32E<LfgJoinResult>("Join Result");
            packet.Translator.ReadInt32E<LfgRoleCheckStatus>("Reason");

            if (result != LfgJoinResult.PartyNotMeetReqs)
                return;

            var numFields = packet.Translator.ReadByte("Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.Translator.ReadGuid("GUID", i);
                var cnt2 = packet.Translator.ReadInt32("Dungeon Count", i);
                for (var j = 0; j < cnt2; j++)
                    ReadDungeonJoinResults(packet, i, j);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgJoinResult434(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk_UInt32_1"); // used in SMSG_LFG_UPDATE_STATUS and SMSG_LFG_QUEUE_STATUS
            packet.Translator.ReadByteE<LfgJoinResult>("Join Result");
            packet.Translator.ReadUInt32("Queue Id");
            packet.Translator.ReadByteE<LfgRoleCheckStatus>("Status");
            packet.Translator.ReadTime("Join Date");

            var guid = new byte[8];

            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();

            var count = packet.Translator.ReadBits("Count", 24);
            var guids = new byte[count][];
            var counts = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                guids[i] = packet.Translator.StartBitStream(7, 5, 3, 6, 0, 2, 4, 1);
                counts[i] = packet.Translator.ReadBits("Dungeon Count", 22, i);
            }

            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();

            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < counts[i]; j++)
                    ReadDungeonJoinResults(packet, i, j);

                packet.Translator.ParseBitStream(guids[i], 2, 5, 1, 0, 4, 3, 6, 7);
                packet.Translator.WriteGuid("Guid", guids[i], i);
            }

            packet.Translator.ParseBitStream(guid, 1, 4, 3, 5, 0, 7, 2, 6);
            packet.Translator.WriteGuid("Join GUID", guid);
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHOSEN)]
        public static void HandleLfgRoleChosen(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadBool("Accept");
            packet.Translator.ReadInt32E<LfgRoleFlag>("Roles");
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_SEARCH)]
        public static void HandleLfgUpdateSearch(Packet packet)
        {
            packet.Translator.ReadBool("Unk Boolean");
        }

        [Parser(Opcode.CMSG_LFG_SET_ROLES)]
        public static void HandleLfgSetRoles(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.Translator.ReadInt32E<LfgRoleFlag>("Roles");
            else
                packet.Translator.ReadByteE<LfgRoleFlag>("Roles");
        }

        [Parser(Opcode.CMSG_LFG_TELEPORT)]
        public static void HandleLfgTeleport(Packet packet)
        {
            packet.Translator.ReadBool("Teleport Out");
        }

        [Parser(Opcode.SMSG_LFG_TELEPORT_DENIED)]
        public static void HandleLfgError(Packet packet)
        {
            packet.Translator.ReadInt32E<LfgError>("Error");
        }

        [Parser(Opcode.CMSG_LFG_LFR_JOIN)]
        [Parser(Opcode.CMSG_LFG_LFR_LEAVE)]
        [Parser(Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER)]
        public static void HandleLfgSearch(Packet packet)
        {
            packet.Translator.ReadLfgEntry("LFG Entry");
        }

        [Parser(Opcode.SMSG_LFG_LFR_LIST)]
        public static void HandleUpdateLfgList(Packet packet)
        {
            packet.Translator.ReadInt32E<LfgType>("LFG Type");
            packet.Translator.ReadInt32<LFGDungeonId>("Dungeon ID");

            var unkBool = packet.Translator.ReadBool("Unknown bool 1");

            if (unkBool)
            {
                var count = packet.Translator.ReadInt32("Unknown count");
                for (var i = 0; i < count; i++)
                    packet.Translator.ReadGuid("Unk Player GUID", i);
            }

            var count2 = packet.Translator.ReadInt32("Group count");
            packet.Translator.ReadInt32("Unknown group count");

            for (var i = 0; i < count2; i++)
            {
                packet.Translator.ReadGuid("Group GUID", i);

                var flags = packet.Translator.ReadInt32E<LfgUpdateFlag>("Update Flags", i);
                if (flags.HasAnyFlag(LfgUpdateFlag.Comment))
                    packet.Translator.ReadCString("Comment", i);

                if (flags.HasAnyFlag(LfgUpdateFlag.Roles))
                    for (var j = 0; j < 3; j++)
                        packet.Translator.ReadByte("Unk byte 1 ", i , j); // Group have this role ?

                if (!flags.HasAnyFlag(LfgUpdateFlag.Binded))
                    continue;

                packet.Translator.ReadGuid("Instance GUID", i);
                packet.Translator.ReadInt32("Encounters Mask", i);
            }

            var count3 = packet.Translator.ReadInt32("Players count");
            packet.Translator.ReadInt32("Unknown player count");

            for (var i = 0; i < count3; i++)
            {
                packet.Translator.ReadGuid("Player GUID", i);
                var flags2 = packet.Translator.ReadInt32E<LfgUpdateFlag>("Update Flags", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.CharacterInfo))
                {
                    packet.Translator.ReadByte("Level", i);
                    packet.Translator.ReadByte("Class", i);
                    packet.Translator.ReadByte("Race", i);

                    for (var j = 0; j < 3; j++)
                        packet.Translator.ReadByte("Talents spent in row", i, j);

                    packet.Translator.ReadInt32("Armor", i);
                    packet.Translator.ReadInt32("Spell Damage Bonus", i);
                    packet.Translator.ReadInt32("Spell Healing Bonus", i);
                    packet.Translator.ReadInt32("Melee Crit Rating", i);
                    packet.Translator.ReadInt32("Ranged Crit Rating", i);
                    packet.Translator.ReadInt32("Spell Crit Rating", i);
                    packet.Translator.ReadSingle("Mana / 5 (Out of combat)", i);
                    packet.Translator.ReadSingle("Mana / 5 (In combat)", i);
                    packet.Translator.ReadInt32("Attackpower", i);
                    packet.Translator.ReadInt32("Agility", i);
                    packet.Translator.ReadInt32("Max Health", i);
                    packet.Translator.ReadInt32("Max Power", i);
                    packet.Translator.ReadInt32("Free talent points", i);
                    packet.Translator.ReadSingle("Unknown float", i);
                    packet.Translator.ReadInt32("Defence Rating", i);
                    packet.Translator.ReadInt32("Dodge Rating", i);
                    packet.Translator.ReadInt32("Block Rating", i);
                    packet.Translator.ReadInt32("Parry Rating", i);
                    packet.Translator.ReadInt32("Crit Rating", i);
                    packet.Translator.ReadInt32("Expertise Rating", i);
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.Comment))
                    packet.Translator.ReadCString("Comment", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.GroupLeader))
                    packet.Translator.ReadBool("Is Group Leader", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Guid))
                    packet.Translator.ReadGuid("Group GUID", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Roles))
                    packet.Translator.ReadByteE<LfgRoleFlag>("Role", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Area))
                    packet.Translator.ReadInt32<AreaId>("Area ID", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Unknown7))
                    packet.Translator.ReadBool("Unknown byte", i);

                if (!flags2.HasAnyFlag(LfgUpdateFlag.Binded))
                    continue;

                packet.Translator.ReadGuid("Instance GUID", i);
                packet.Translator.ReadInt32("Encounters Mask", i);
            }
        }

        [Parser(Opcode.CMSG_DUNGEON_FINDER_GET_SYSTEM_INFO)]
        public static void HandleDungeonFinderGetSystemInfo(Packet packet)
        {
            packet.Translator.ReadBit("Unk boolean");
        }

        [Parser(Opcode.SMSG_ROLE_POLL_BEGIN)]
        public static void HandleRollPollBegin(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 5, 7, 3, 2, 4, 0, 6);
            packet.Translator.ParseBitStream(guid, 4, 7, 0, 5, 1, 6, 2, 3);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_DF_GET_SYSTEM_INFO)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.Translator.ReadBit("Player (1)/ Party (0)");
        }

        [Parser(Opcode.CMSG_LFG_PLAYER_LOCK_INFO_REQUEST)]
        [Parser(Opcode.CMSG_LFG_PARTY_LOCK_INFO_REQUEST)]
        [Parser(Opcode.CMSG_DF_GET_JOIN_STATUS)]
        [Parser(Opcode.SMSG_LFG_DISABLED)]
        [Parser(Opcode.CMSG_LFG_LEAVE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLFGNull(Packet packet)
        {
        }
    }
}
