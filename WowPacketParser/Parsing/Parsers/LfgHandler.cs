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
            packet.ReadInt32E<LfgRoleFlag>("Roles");
            packet.ReadBytes(2); // always 0
            var numFields = packet.ReadByte("Join Dungeon Count");
            for (var i = 0; i < numFields; i++)
                packet.ReadLfgEntry("Dungeon Entry", i);

            packet.ReadUInt32(); // always 0 (for 1..3) 0
            packet.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_LFG_JOIN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgJoin434(Packet packet)
        {
            packet.ReadInt32E<LfgRoleFlag>("Roles");

            for (var i = 0; i < 3; i++)
                packet.ReadInt32("Unk Int32", i);

            var length = packet.ReadBits(9);
            var count = packet.ReadBits("Join Dungeon Count", 24);

            packet.ReadWoWString("Comment", length);

            for (var i = 0; i < count; i++)
                packet.ReadLfgEntry("Dungeon Entry", i);
        }

        [Parser(Opcode.CMSG_LFG_LEAVE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgLeave(Packet packet)
        {
            packet.ReadInt32E<LfgRoleFlag>("Roles");
            packet.ReadTime("Time");
            packet.ReadUInt32("Reason?");
            packet.ReadUInt32("Instance Id");

            var guid = packet.StartBitStream(4, 5, 0, 6, 2, 7, 1, 3);
            packet.ParseBitStream(guid, 7, 4, 3, 2, 6, 0, 1, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_LFG_SET_COMMENT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgComment(Packet packet)
        {
            packet.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_LFG_SET_COMMENT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgComment434(Packet packet)
        {
            var length = packet.ReadBits(9);
            packet.ReadWoWString("Comment", length);
        }

        [Parser(Opcode.CMSG_LFG_SET_BOOT_VOTE)]
        public static void HandleLfgSetBootVote(Packet packet)
        {
            packet.ReadBool("Agree");
        }

        [Parser(Opcode.CMSG_LFG_PROPOSAL_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalResult(Packet packet)
        {
            packet.ReadInt32("Group ID");
            packet.ReadBool("Accept");
        }

        [Parser(Opcode.CMSG_LFG_PROPOSAL_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalResult434(Packet packet)
        {
            packet.ReadUInt32("Unk Uint32");
            packet.ReadTime("Time");
            packet.ReadInt32E<LfgRoleFlag>("Roles");
            packet.ReadUInt32("Unk Uint32");

            var guid2 = packet.StartBitStream(4, 5, 0, 6, 2, 7, 1, 3);
            packet.ParseBitStream(guid2, 7, 4, 3, 2, 6, 0, 1, 5);
            packet.WriteGuid("Player Guid", guid2);

            var guid = new byte[8];
            guid[7] = packet.ReadBit();
            packet.ReadBit("Accept");
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ParseBitStream(guid, 7, 1, 5, 6, 3, 4, 0, 2);
            packet.WriteGuid("Instance Guid", guid);

        }

        [Parser(Opcode.SMSG_LFG_BOOT_PROPOSAL_UPDATE)]
        public static void HandleLfgBootProposalUpdate(Packet packet)
        {
            packet.ReadBool("In Progress");
            packet.ReadBool("Did Vote");
            packet.ReadBool("Vote");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadByte("Offline/afk");
            packet.ReadGuid("Victim GUID");
            packet.ReadInt32("Total Votes");
            packet.ReadInt32("Agree Count");
            packet.ReadInt32("Time Left");
            packet.ReadInt32("Needed Votes");
            packet.ReadCString("Comment");
        }

        public static void ReadLfgRewardBlock(Packet packet, object index)
        {
            packet.ReadBool("First Completion", index);

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_LFG_PLAYER_REWARD, Direction.ServerToClient))
                packet.ReadInt32("Strangers", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                packet.ReadInt32("currencyQuantity", index);
                packet.ReadInt32("Unk 2", index);
                packet.ReadInt32("currencyID", index);
                packet.ReadInt32("tier1Quantity", index);
                packet.ReadInt32("tier1Limit", index);
                packet.ReadInt32("overallQuantity", index);
                packet.ReadInt32("overallLimit", index);
                packet.ReadInt32("periodPurseQuantity", index);
                packet.ReadInt32("periodPurseLimit", index);
                packet.ReadInt32("purseQuantity", index);
                packet.ReadInt32("purseLimit", index);
                packet.ReadInt32("Unk 4", index);
                packet.ReadInt32("completedEncounters", index);

                packet.ReadByte("Call to Arms eligible", index);

                // LFG_SLOT_INFO_LOOT related
                for (var i = 0; i < 3; ++i)
                {
                    var unk1 = packet.ReadInt32E<LfgRoleFlag>("Call to Arms Role", index, i);
                    if (unk1 != 0)
                    {
                        packet.ReadInt32("Call to Arms Money", index, i);
                        packet.ReadInt32("Call to Arms XP", index, i);
                        var unk4 = packet.ReadByte("Call to Arms Item Count", index, i);
                        for (var j = 0; j < unk4; ++j)
                        {
                            packet.ReadInt32<ItemId>("Call to Arms Item Or Currency Id", index, i, j);
                            packet.ReadInt32("Call to Arms Item Display ID", index, i, j);
                            packet.ReadInt32("Call to Arms Item Stack Count", index, i, j);
                            packet.ReadBool("Call to Arms Is Currency", index, i, j);
                        }
                    }
                }
            }

            packet.ReadInt32("Base Money", index);
            packet.ReadInt32("Base XP", index);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
            {
                packet.ReadInt32("Variable Money", index);
                packet.ReadInt32("Variable XP", index);
            }

            var numFields = packet.ReadByte("Reward Item Count", index);
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadInt32<ItemId>("Reward Item Or Currency Id", index, i);
                packet.ReadInt32("Reward Item Display ID", index, i);
                packet.ReadInt32("Reward Item Stack Count", index, i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadBool("Is Currency", index, i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgCompletionReward(Packet packet)
        {
            packet.ReadLfgEntry("Random LFG Entry");
            packet.ReadLfgEntry("Actual LFG Entry");

            ReadLfgRewardBlock(packet, -1);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgCompletionReward434(Packet packet)
        {
            packet.ReadLfgEntry("Random LFG Entry");
            packet.ReadLfgEntry("Actual LFG Entry");
            packet.ReadUInt32("Base Money");
            packet.ReadUInt32("Base XP");

            var numFields = packet.ReadByte("Reward Item Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadInt32<ItemId>("Reward Item Or Currency Id", i);
                packet.ReadInt32("Reward Item Display ID", i);
                packet.ReadInt32("Reward Item Stack Count", i);
                packet.ReadBool("Is Currency", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PLAYER)]
        public static void HandleLfgUpdatePlayer(Packet packet)
        {
            packet.ReadByteE<LfgUpdateType>("Update Type");
            var extraInfo = packet.ReadBool("Extra Info");
            if (!extraInfo)
                return;

            packet.ReadBool("Queued");
            packet.ReadBytes(2); // always 0
            var numFields2 = packet.ReadByte("Slot Count");
            for (var i = 0; i < numFields2; i++)
                packet.ReadLfgEntry("LFG Entry", i);

            packet.ReadCString("Comment");
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PARTY)]
        public static void HandleLfgUpdateParty(Packet packet)
        {
            packet.ReadByteE<LfgUpdateType>("Update Type");
            var extraInfo = packet.ReadBool("Extra Info");
            if (!extraInfo)
                return;

            packet.ReadBool("LFG Joined");
            packet.ReadBool("Queued");
            packet.ReadBytes(5); // always 0, 0, for (1..3) 0
            var numFields2 = packet.ReadByte("Slot Count");
            for (var i = 0; i < numFields2; i++)
                packet.ReadLfgEntry("LFG Entry", i);

            packet.ReadCString("Comment");
        }

        [Parser(Opcode.SMSG_LFG_OFFER_CONTINUE)]
        public static void HandleLfgOfferContinue(Packet packet)
        {
            packet.ReadLfgEntry("LFG Entry");
        }

        public static void ReadDungeonJoinResults(Packet packet, params int[] values)
        {
            packet.ReadLfgEntry("LFG Entry", values);
            packet.ReadUInt32E<LfgEntryCheckResult>("Entry Check Result", values);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
            {
                packet.ReadInt32("needed ILvL", values);
                packet.ReadInt32("player ILvL", values);
            }
        }

        public static void ReadPlayerLockedDungeons(Packet packet, int i)
        {
            var numFields = packet.ReadInt32("Entry Count", i);
            for (var j = 0; j < numFields; j++)
                ReadDungeonJoinResults(packet, i, j);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var numFields = packet.ReadByte("Random Dungeon Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadLfgEntry("Random Dungeon Entry", i);
                ReadLfgRewardBlock(packet, i);
            }

            ReadPlayerLockedDungeons(packet, -1);
        }

        [Parser(Opcode.SMSG_LFG_PARTY_INFO)]
        public static void HandleLfgPlayerLockInfoUpdate(Packet packet)
        {
            var numFields = packet.ReadByte("Player Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadGuid("GUID", i);
                ReadPlayerLockedDungeons(packet, i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            packet.ReadLfgEntry("LFG Entry");
            packet.ReadByteE<LfgProposalState>("State");
            packet.ReadInt32("Group ID");
            packet.ReadInt32("Bosses Killed Mask");
            packet.ReadBool("Silent");

            var numFields = packet.ReadByte("Response Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadInt32E<LfgRoleFlag>("Roles", i);
                packet.ReadBool("Self", i);
                packet.ReadBool("In Dungeon", i);
                packet.ReadBool("Same Group", i);
                packet.ReadBool("Answer", i);
                packet.ReadBool("Accept", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalUpdate434(Packet packet)
        {
            packet.ReadTime("Date");
            packet.ReadInt32("Bosses Killed Mask");
            packet.ReadInt32("Unk UInt32 1");
            packet.ReadUInt32("Unk UInt32 2");
            packet.ReadLfgEntry("LFG Entry");
            packet.ReadUInt32("Unk UInt32 3");
            packet.ReadByteE<LfgProposalState>("State");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[4] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            packet.ReadBit("Silent");
            guid1[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();

            var count = packet.ReadBits("Response Count", 23);

            guid2[7] = packet.ReadBit();

            for (var i = 0; i < count; ++i)
            {
                var bits = new Bit[5];
                for (var j = 0; j < 5; ++j)
                    bits[j] = packet.ReadBit();
                packet.AddValue("Bits", string.Format("In Dungeon?: {0}, Same Group?: {1}, Accept: {2}, Answer: {3}, Self: {4}",
                    bits[0], bits[1], bits[2], bits[3], bits[4]), i); // 0 and 1 could be swapped
            }

            guid2[5] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 1);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32E<LfgRoleFlag>("Roles", i);

            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 4);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_STATUS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgUpdateStatus(Packet packet)
        {
            var guid = new byte[8];

            guid[1] = packet.ReadBit();
            packet.ReadBit("Unk Bit 65");
            var count = packet.ReadBits("Dungeon Count", 24);
            guid[6] = packet.ReadBit();
            packet.ReadBit("Joined");
            var length = packet.ReadBits(9);
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadBit("LFGJoined");
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("Queued");
            packet.ReadByte("Unk Byte 64");
            packet.ReadWoWString("Comment", length);
            packet.ReadUInt32("Queue Id");
            packet.ReadTime("Join Date");

            packet.ReadXORByte(guid, 6);

            for (var i = 0; i < 3; ++i)
                packet.ReadByte("Unk Byte", i); // always 0

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);

            packet.ReadInt32("Unk_UInt32_1"); // Same value than "Unk_UInt32_1" in SMSG_LFG_JOIN_RESULT - Only seen 3

            packet.ReadXORByte(guid, 7);

            for (var i = 0; i < count; ++i)
                packet.ReadLfgEntry("Dungeon Entry", i);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            packet.ReadLfgEntry("LFG Entry");
            packet.ReadInt32("Average Wait Time");
            packet.ReadInt32("Wait Time");
            packet.ReadInt32("Wait Tanks");
            packet.ReadInt32("Wait Headers");
            packet.ReadInt32("Wait DPS");
            packet.ReadByte("Number of Tanks Needed");
            packet.ReadByte("Number of Healers Needed");
            packet.ReadByte("Number of Damage Dealers Needed");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.ReadInt32("Queued Time");
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgQueueStatusUpdate434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 0, 2, 6, 5, 7, 1, 4);

            packet.ReadXORByte(guid, 0);

            //for (var i = 0; i < 3; ++i) byte uint32
            packet.ReadByte("Tank Unk");
            packet.ReadInt32("Tank Time");
            packet.ReadByte("Healer Unk");
            packet.ReadInt32("Healer Time");
            packet.ReadByte("Damage Unk");
            packet.ReadInt32("Damage Time");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);

            packet.ReadInt32("Average Wait Time");
            packet.ReadTime("Join Time");
            packet.ReadLfgEntry("LFG Entry");
            packet.ReadInt32("Queued Time");

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);

            packet.ReadInt32("Queue Id");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);

            packet.ReadInt32("Wait Time"); // Matches "Role Unk2"
            packet.ReadInt32("Unk_UInt32_1"); // Same value than "Unk_UInt32_1" in SMSG_LFG_JOIN_RESULT - Only seen 3
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.ReadInt32E<LfgRoleCheckStatus>("Role Check Status");
            packet.ReadBool("Is Beginning");

            var numFields = packet.ReadByte("Entry Count");
            for (var i = 0; i < numFields; i++)
                packet.ReadLfgEntry("LFG Entry", i);

            var numFields2 = packet.ReadByte("Player Count");
            for (var i = 0; i < numFields2; i++)
            {
                packet.ReadGuid("GUID", i);
                packet.ReadBool("Ready", i);
                packet.ReadInt32E<LfgRoleFlag>("Roles", i);
                packet.ReadByte("Level", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            var result = packet.ReadInt32E<LfgJoinResult>("Join Result");
            packet.ReadInt32E<LfgRoleCheckStatus>("Reason");

            if (result != LfgJoinResult.PartyNotMeetReqs)
                return;

            var numFields = packet.ReadByte("Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadGuid("GUID", i);
                var cnt2 = packet.ReadInt32("Dungeon Count", i);
                for (var j = 0; j < cnt2; j++)
                    ReadDungeonJoinResults(packet, i, j);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgJoinResult434(Packet packet)
        {
            packet.ReadUInt32("Unk_UInt32_1"); // used in SMSG_LFG_UPDATE_STATUS and SMSG_LFG_QUEUE_STATUS
            packet.ReadByteE<LfgJoinResult>("Join Result");
            packet.ReadUInt32("Queue Id");
            packet.ReadByteE<LfgRoleCheckStatus>("Status");
            packet.ReadTime("Join Date");

            var guid = new byte[8];

            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            var count = packet.ReadBits("Count", 24);
            var guids = new byte[count][];
            var counts = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                guids[i] = packet.StartBitStream(7, 5, 3, 6, 0, 2, 4, 1);
                counts[i] = packet.ReadBits("Dungeon Count", 22, i);
            }

            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < counts[i]; j++)
                    ReadDungeonJoinResults(packet, i, j);

                packet.ParseBitStream(guids[i], 2, 5, 1, 0, 4, 3, 6, 7);
                packet.WriteGuid("Guid", guids[i], i);
            }

            packet.ParseBitStream(guid, 1, 4, 3, 5, 0, 7, 2, 6);
            packet.WriteGuid("Join GUID", guid);
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHOSEN)]
        public static void HandleLfgRoleChosen(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBool("Accept");
            packet.ReadInt32E<LfgRoleFlag>("Roles");
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_SEARCH)]
        public static void HandleLfgUpdateSearch(Packet packet)
        {
            packet.ReadBool("Unk Boolean");
        }

        [Parser(Opcode.CMSG_LFG_SET_ROLES)]
        public static void HandleLfgSetRoles(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadInt32E<LfgRoleFlag>("Roles");
            else
                packet.ReadByteE<LfgRoleFlag>("Roles");
        }

        [Parser(Opcode.CMSG_LFG_TELEPORT)]
        public static void HandleLfgTeleport(Packet packet)
        {
            packet.ReadBool("Teleport Out");
        }

        [Parser(Opcode.SMSG_LFG_TELEPORT_DENIED)]
        public static void HandleLfgError(Packet packet)
        {
            packet.ReadInt32E<LfgError>("Error");
        }

        [Parser(Opcode.CMSG_LFG_LFR_JOIN)]
        [Parser(Opcode.CMSG_LFG_LFR_LEAVE)]
        [Parser(Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER)]
        public static void HandleLfgSearch(Packet packet)
        {
            packet.ReadLfgEntry("LFG Entry");
        }

        [Parser(Opcode.SMSG_LFG_LFR_LIST)]
        public static void HandleUpdateLfgList(Packet packet)
        {
            packet.ReadInt32E<LfgType>("LFG Type");
            packet.ReadInt32<LFGDungeonId>("Dungeon ID");

            var unkBool = packet.ReadBool("Unknown bool 1");

            if (unkBool)
            {
                var count = packet.ReadInt32("Unknown count");
                for (var i = 0; i < count; i++)
                    packet.ReadGuid("Unk Player GUID", i);
            }

            var count2 = packet.ReadInt32("Group count");
            packet.ReadInt32("Unknown group count");

            for (var i = 0; i < count2; i++)
            {
                packet.ReadGuid("Group GUID", i);

                var flags = packet.ReadInt32E<LfgUpdateFlag>("Update Flags", i);
                if (flags.HasAnyFlag(LfgUpdateFlag.Comment))
                    packet.ReadCString("Comment", i);

                if (flags.HasAnyFlag(LfgUpdateFlag.Roles))
                    for (var j = 0; j < 3; j++)
                        packet.ReadByte("Unk byte 1 ", i , j); // Group have this role ?

                if (!flags.HasAnyFlag(LfgUpdateFlag.Binded))
                    continue;

                packet.ReadGuid("Instance GUID", i);
                packet.ReadInt32("Encounters Mask", i);
            }

            var count3 = packet.ReadInt32("Players count");
            packet.ReadInt32("Unknown player count");

            for (var i = 0; i < count3; i++)
            {
                packet.ReadGuid("Player GUID", i);
                var flags2 = packet.ReadInt32E<LfgUpdateFlag>("Update Flags", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.CharacterInfo))
                {
                    packet.ReadByte("Level", i);
                    packet.ReadByte("Class", i);
                    packet.ReadByte("Race", i);

                    for (var j = 0; j < 3; j++)
                        packet.ReadByte("Talents spent in row", i, j);

                    packet.ReadInt32("Armor", i);
                    packet.ReadInt32("Spell Damage Bonus", i);
                    packet.ReadInt32("Spell Healing Bonus", i);
                    packet.ReadInt32("Melee Crit Rating", i);
                    packet.ReadInt32("Ranged Crit Rating", i);
                    packet.ReadInt32("Spell Crit Rating", i);
                    packet.ReadSingle("Mana / 5 (Out of combat)", i);
                    packet.ReadSingle("Mana / 5 (In combat)", i);
                    packet.ReadInt32("Attackpower", i);
                    packet.ReadInt32("Agility", i);
                    packet.ReadInt32("Max Health", i);
                    packet.ReadInt32("Max Power", i);
                    packet.ReadInt32("Free talent points", i);
                    packet.ReadSingle("Unknown float", i);
                    packet.ReadInt32("Defence Rating", i);
                    packet.ReadInt32("Dodge Rating", i);
                    packet.ReadInt32("Block Rating", i);
                    packet.ReadInt32("Parry Rating", i);
                    packet.ReadInt32("Crit Rating", i);
                    packet.ReadInt32("Expertise Rating", i);
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.Comment))
                    packet.ReadCString("Comment", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.GroupLeader))
                    packet.ReadBool("Is Group Leader", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Guid))
                    packet.ReadGuid("Group GUID", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Roles))
                    packet.ReadByteE<LfgRoleFlag>("Role", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Area))
                    packet.ReadInt32<AreaId>("Area ID", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Unknown7))
                    packet.ReadBool("Unknown byte", i);

                if (!flags2.HasAnyFlag(LfgUpdateFlag.Binded))
                    continue;

                packet.ReadGuid("Instance GUID", i);
                packet.ReadInt32("Encounters Mask", i);
            }
        }

        [Parser(Opcode.CMSG_DUNGEON_FINDER_GET_SYSTEM_INFO)]
        public static void HandleDungeonFinderGetSystemInfo(Packet packet)
        {
            packet.ReadBit("Unk boolean");
        }

        [Parser(Opcode.SMSG_ROLE_POLL_BEGIN)]
        public static void HandleRollPollBegin(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 7, 3, 2, 4, 0, 6);
            packet.ParseBitStream(guid, 4, 7, 0, 5, 1, 6, 2, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_DF_GET_SYSTEM_INFO)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.ReadBit("Player (1)/ Party (0)");
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
