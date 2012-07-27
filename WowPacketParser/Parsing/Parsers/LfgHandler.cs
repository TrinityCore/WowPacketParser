using System;
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
            packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32);
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
            packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32);

            for (var i = 0; i < 3; i++)
                packet.ReadInt32("Unk Int32", i);

            var length = packet.ReadBits("Comment Length", 9);
            var count = packet.ReadBits("Join Dungeon Count", 24);

            packet.ReadWoWString("Comment", length);

            for (var i = 0; i < count; i++)
                packet.ReadLfgEntry("Dungeon Entry", i);
        }

        [Parser(Opcode.CMSG_LFG_LEAVE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgLeave(Packet packet)
        {
            packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32);
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
            var length = packet.ReadBits("String Length", 9);
            packet.ReadWoWString("Comment", length);
        }

        [Parser(Opcode.CMSG_LFG_SET_BOOT_VOTE)]
        public static void HandleLfgSetBootVote(Packet packet)
        {
            packet.ReadBoolean("Agree");
        }

        [Parser(Opcode.CMSG_LFG_PROPOSAL_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalResult(Packet packet)
        {
            packet.ReadInt32("Group ID");
            packet.ReadBoolean("Accept");
        }

        [Parser(Opcode.CMSG_LFG_PROPOSAL_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleLfgProposalResult434(Packet packet)
        {
            packet.ReadUInt32("Unk Uint32");
            packet.ReadTime("Time");
            packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32);
            packet.ReadUInt32("Unk Uint32");

            var guid2 = packet.StartBitStream(4, 5, 0, 6, 2, 7, 1, 3);
            packet.ParseBitStream(guid2, 7, 4, 3, 2, 6, 0, 1, 5);
            packet.WriteGuid("Player Guid", guid2);

            var guid = new byte[8];
            guid[7] = packet.ReadBit().ToByte();
            packet.ReadBit("Accept");
            guid[1] = packet.ReadBit().ToByte();
            guid[3] = packet.ReadBit().ToByte();
            guid[0] = packet.ReadBit().ToByte();
            guid[5] = packet.ReadBit().ToByte();
            guid[4] = packet.ReadBit().ToByte();
            guid[6] = packet.ReadBit().ToByte();
            guid[2] = packet.ReadBit().ToByte();

            packet.ParseBitStream(guid, 7, 1, 5, 6, 3, 4, 0, 2);
            packet.WriteGuid("Instance Guid", guid);

        }

        [Parser(Opcode.SMSG_LFG_BOOT_PROPOSAL_UPDATE)]
        public static void HandleLfgBootProposalUpdate(Packet packet)
        {
            packet.ReadBoolean("In Progress");
            packet.ReadBoolean("Did Vote");
            packet.ReadBoolean("Vote");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadByte("Unk");
            packet.ReadGuid("Victim GUID");
            packet.ReadInt32("Total Votes");
            packet.ReadInt32("Agree Count");
            packet.ReadInt32("Time Left");
            packet.ReadInt32("Needed Votes");
            packet.ReadCString("Comment");
        }

        public static void ReadLfgRewardBlock(ref Packet packet)
        {
            packet.ReadBoolean("First Completion");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_LFG_PLAYER_REWARD))
                packet.ReadInt32("Strangers");

            packet.ReadInt32("Base Money");
            packet.ReadInt32("Base XP");
            packet.ReadInt32("Variable Money");
            packet.ReadInt32("Variable XP");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                packet.ReadInt32("Unk 1");
                packet.ReadInt32("Unk 2");
                packet.ReadInt32("Unk 3");
                packet.ReadInt32("Unk 4");
                packet.ReadInt32("Unk 5");
                packet.ReadInt32("Unk 6");
                packet.ReadInt32("Unk 7");
                packet.ReadInt32("Unk 8");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595)) // perhaps earlier, confirmed for 434
                    packet.ReadInt32("Unk 8.1");

                packet.ReadByte("Unk 9");

                // LFG_SLOT_INFO_LOOT related
                for (var i = 0; i < 3; ++i)
                {
                    var unk1 = packet.ReadInt32("Unk 1", i);
                    if (unk1 != 0)
                    {
                        packet.ReadInt32("Unk 2", i);
                        packet.ReadInt32("Unk 3", i);
                        var unk4 = packet.ReadByte("Unk 4", i);
                        for (var j = 0; j < unk4; ++j)
                        {
                            packet.ReadInt32("Unk 5", j);
                            packet.ReadInt32("Unk 6", j);
                            packet.ReadInt32("Unk 7", j);
                            packet.ReadByte("Unk 8", j);
                        }
                    }
                }

                packet.ReadInt32("Unk 10");
                packet.ReadInt32("Unk 11");
            }

            var numFields = packet.ReadByte("Reward Item Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Reward Item Or Currency Id", i);
                packet.ReadInt32("Reward Item Display ID", i);
                packet.ReadInt32("Reward Item Stack Count", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadBoolean("Is Currency", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD)]
        public static void HandleLfgCompletionReward(Packet packet)
        {
            packet.ReadLfgEntry("Random LFG Entry");
            packet.ReadLfgEntry("Actual LFG Entry");

            ReadLfgRewardBlock(ref packet);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PLAYER)]
        public static void HandleLfgUpdatePlayer(Packet packet)
        {
            packet.ReadEnum<LfgUpdateType>("Update Type", TypeCode.Byte);
            var extraInfo = packet.ReadBoolean("Extra Info");
            if (!extraInfo)
                return;

            packet.ReadBoolean("Queued");
            packet.ReadBytes(2); // always 0
            var numFields2 = packet.ReadByte("Slot Count");
            for (var i = 0; i < numFields2; i++)
                packet.ReadLfgEntry("LFG Entry", i);

            packet.ReadCString("Comment");
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PARTY)]
        public static void HandleLfgUpdateParty(Packet packet)
        {
            packet.ReadEnum<LfgUpdateType>("Update Type", TypeCode.Byte);
            var extraInfo = packet.ReadBoolean("Extra Info");
            if (!extraInfo)
                return;

            packet.ReadBoolean("LFG Joined");
            packet.ReadBoolean("Queued");
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

        public static void ReadDungeonJoinResults(ref Packet packet, params int[] values)
        {
            packet.ReadLfgEntry("LFG Entry", values);
            packet.ReadEnum<LfgEntryCheckResult>("Entry Check Result", TypeCode.UInt32, values);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
            {
                packet.ReadInt32("needed ILvL", values);
                packet.ReadInt32("player ILvL", values);
            }
        }

        public static void ReadPlayerLockedDungeons(ref Packet packet, int i)
        {
            var numFields = packet.ReadInt32("Entry Count", i);
            for (var j = 0; j < numFields; j++)
                ReadDungeonJoinResults(ref packet, i, j);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var numFields = packet.ReadByte("Random Dungeon Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadLfgEntry("Random Dungeon Entry", i);
                ReadLfgRewardBlock(ref packet);
            }

            ReadPlayerLockedDungeons(ref packet, -1);
        }

        [Parser(Opcode.SMSG_LFG_PARTY_INFO)]
        public static void HandleLfgPlayerLockInfoUpdate(Packet packet)
        {
            var numFields = packet.ReadByte("Player Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadGuid("GUID", i);
                ReadPlayerLockedDungeons(ref packet, i);
            }
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            packet.ReadLfgEntry("LFG Entry");
            packet.ReadEnum<LfgProposalState>("State", TypeCode.Byte);
            packet.ReadInt32("Group ID");
            packet.ReadInt32("Bosses Killed Mask");
            packet.ReadBoolean("Silent");

            var numFields = packet.ReadByte("Response Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32, i);
                packet.ReadBoolean("Self", i);
                packet.ReadBoolean("In Dungeon", i);
                packet.ReadBoolean("Same Group", i);
                packet.ReadBoolean("Answer", i);
                packet.ReadBoolean("Accept", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS)]
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

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.ReadEnum<LfgRoleCheckStatus>("Role Check Status", TypeCode.Int32);
            packet.ReadBoolean("Is Beginning");

            var numFields = packet.ReadByte("Entry Count");
            for (var i = 0; i < numFields; i++)
                packet.ReadLfgEntry("LFG Entry", i);

            var numFields2 = packet.ReadByte("Player Count");
            for (var i = 0; i < numFields2; i++)
            {
                packet.ReadGuid("GUID", i);
                packet.ReadBoolean("Ready", i);
                packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32, i);
                packet.ReadByte("Level", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            var result = packet.ReadEnum<LfgJoinResult>("Join Result", TypeCode.Int32);
            packet.ReadEnum<LfgRoleCheckStatus>("Reason", TypeCode.Int32);

            if (result != LfgJoinResult.PartyNotMeetReqs)
                return;

            var numFields = packet.ReadByte("Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadGuid("GUID", i);
                var cnt2 = packet.ReadInt32("Dungeon Count", i);
                for (var j = 0; j < cnt2; j++)
                    ReadDungeonJoinResults(ref packet, i, j);
            }
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHOSEN)]
        public static void HandleLfgRoleChosen(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Accept");
            packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_SEARCH)]
        public static void HandleLfgUpdateSearch(Packet packet)
        {
            packet.ReadBoolean("Unk Boolean");
        }

        [Parser(Opcode.CMSG_LFG_SET_ROLES)]
        public static void HandleLfgSetRoles(Packet packet)
        {
            packet.ReadEnum<LfgRoleFlag>("Roles",
                                         ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595)
                                             ? TypeCode.Int32
                                             : TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_LFG_TELEPORT)]
        public static void HandleLfgTeleport(Packet packet)
        {
            packet.ReadBoolean("Teleport Out");
        }

        [Parser(Opcode.SMSG_LFG_TELEPORT_DENIED)]
        public static void HandleLfgError(Packet packet)
        {
            packet.ReadEnum<LfgError>("Error", TypeCode.Int32);
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
            packet.ReadEnum<LfgType>("LFG Type", TypeCode.Int32);
            packet.ReadEntryWithName<Int32>(StoreNameType.LFGDungeon, "Dungeon ID");

            var unkBool = packet.ReadBoolean("Unknown bool 1");

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

                var flags = packet.ReadEnum<LfgUpdateFlag>("Update Flags", TypeCode.Int32, i);
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
                var flags2 = packet.ReadEnum<LfgUpdateFlag>("Update Flags", TypeCode.Int32, i);

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
                    packet.ReadBoolean("Is Group Leader", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Guid))
                    packet.ReadGuid("Group GUID", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Roles))
                    packet.ReadEnum<LfgRoleFlag>("Role", TypeCode.Byte, i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Area))
                    packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area ID", i);

                if (flags2.HasAnyFlag(LfgUpdateFlag.Unknown7))
                    packet.ReadBoolean("Unknown byte", i);

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
    }
}
