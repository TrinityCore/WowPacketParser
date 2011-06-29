using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.CMSG_LFG_JOIN)]
        public static void HandleLfgJoin(Packet packet)
        {
            var roles = (LfgRoleFlag)packet.ReadInt32();
            Console.WriteLine("Roles: " + roles);

            var boolean1 = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean 1: " + boolean1);

            var boolean2 = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean 2: " + boolean2);

            var numFields = packet.ReadByte();
            Console.WriteLine("Join Dungeon Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var lfgEntry = packet.ReadLfgEntry();
                Console.WriteLine("Dungeon Entry " + i + ": " + lfgEntry);
            }

            var numFields2 = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + numFields2);

            for (var i = 0; i < numFields2; i++)
            {
                var unkByte = packet.ReadByte();
                Console.WriteLine("Unk Byte 2 " + i + ": " + unkByte);
            }

            var comment = packet.ReadCString();
            Console.WriteLine("Comment: " + comment);
        }

        [Parser(Opcode.CMSG_SET_LFG_COMMENT)]
        public static void HandleLfgComment(Packet packet)
        {
            var comment = packet.ReadCString();
            Console.WriteLine("Comment: " + comment);
        }

        [Parser(Opcode.CMSG_LFG_SET_BOOT_VOTE)]
        public static void HandleLfgSetBootVote(Packet packet)
        {
            var boolean = packet.ReadBoolean();
            Console.WriteLine("Agree: " + boolean);
        }

        [Parser(Opcode.CMSG_LFG_PROPOSAL_RESULT)]
        public static void HandleLfgProposalResult(Packet packet)
        {
            var int32 = packet.ReadInt32();
            Console.WriteLine("Group ID: " + int32);

            var boolean = packet.ReadBoolean();
            Console.WriteLine("Accept: " + boolean);
        }

        [Parser(Opcode.SMSG_LFG_BOOT_PLAYER)]
        public static void HandleLfgBootProposalUpdate(Packet packet)
        {
            var byte1 = packet.ReadBoolean();
            Console.WriteLine("In Progress: " + byte1);

            var byte2 = packet.ReadBoolean();
            Console.WriteLine("Did Vote: " + byte2);

            var byte3 = packet.ReadBoolean();
            Console.WriteLine("Vote: " + byte3);

            var playerGuid = packet.ReadGuid();
            Console.WriteLine("Victim GUID: " + playerGuid);

            var int1 = packet.ReadInt32();
            Console.WriteLine("Total Votes: " + int1);

            var int2 = packet.ReadInt32();
            Console.WriteLine("Agree Count: " + int2);

            var int3 = packet.ReadInt32();
            Console.WriteLine("Time Left: " + int3);

            var int4 = packet.ReadInt32();
            Console.WriteLine("Needed Votes: " + int4);

            var comment = packet.ReadCString();
            Console.WriteLine("Comment: " + comment);
        }

        public static void ReadLfgRewardBlock(Packet packet)
        {
            var byte1 = packet.ReadBoolean();
            Console.WriteLine("First Completion: " + byte1);

            if (packet.GetOpcode() == Opcode.SMSG_LFG_PLAYER_REWARD)
            {
                var int3 = packet.ReadInt32();
                Console.WriteLine("Strangers: " + int3);
            }

            var int4 = packet.ReadInt32();
            Console.WriteLine("Base Money: " + int4);

            var int5 = packet.ReadInt32();
            Console.WriteLine("Base XP: " + int5);

            var int6 = packet.ReadInt32();
            Console.WriteLine("Variable Money: " + int6);

            var int7 = packet.ReadInt32();
            Console.WriteLine("Variable XP: " + int7);

            var numFields = packet.ReadByte();
            Console.WriteLine("Reward Item Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var blockint1 = packet.ReadInt32();
                Console.WriteLine("Reward Item ID " + i + ": " + blockint1);

                var blockint2 = packet.ReadInt32();
                Console.WriteLine("Reward Item Display ID " + i + ": " + blockint2);

                var blockint3 = packet.ReadInt32();
                Console.WriteLine("Reward Item Stack Count " + i + ": " + blockint3);
            }
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD)]
        public static void HandleLfgCompletionReward(Packet packet)
        {
            var int1 = packet.ReadLfgEntry();
            Console.WriteLine("Random LFG Entry: " + int1);

            var int2 = packet.ReadInt32();
            Console.WriteLine("Actual LFG Entry: " + int2);

            ReadLfgRewardBlock(packet);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PLAYER)]
        public static void HandleLfgUpdatePlayer(Packet packet)
        {
            var byte1 = (LfgUpdateType)packet.ReadByte();
            Console.WriteLine("Update Type: " + byte1);

            var boolean = packet.ReadBoolean();
            Console.WriteLine("Joined: " + boolean);

            if (!boolean)
                return;

            var byte2 = packet.ReadBoolean();
            Console.WriteLine("Queued: " + byte2);

            var byte3 = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + byte3);

            var byte4 = packet.ReadByte();
            Console.WriteLine("Unk Byte 2: " + byte4);

            var numFields2 = packet.ReadByte();
            Console.WriteLine("Slot Count: " + numFields2);

            for (var i = 0; i < numFields2; i++)
            {
                var blockint32 = packet.ReadLfgEntry();
                Console.WriteLine("LFG Entry " + i + ": " + blockint32);
            }

            var string1 = packet.ReadCString();
            Console.WriteLine("Comment: " + string1);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PARTY)]
        public static void HandleLfgUpdateParty(Packet packet)
        {
            var byte1 = (LfgUpdateType)packet.ReadByte();
            Console.WriteLine("Update Type: " + byte1);

            var boolean = packet.ReadBoolean();
            Console.WriteLine("Joined: " + boolean);

            if (!boolean)
                return;

            var byte2 = packet.ReadBoolean();
            Console.WriteLine("LFG Joined: " + byte2);

            var byte3 = packet.ReadByte();
            Console.WriteLine("Queued: " + byte3);

            var byte4 = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + byte4);

            var byte5 = packet.ReadByte();
            Console.WriteLine("Unk Byte 2: " + byte5);

            for (var i = 0; i < 3; i++)
            {
                var blockbyte = packet.ReadByte();
                Console.WriteLine("Unk Byte 3 " + i + ": " + blockbyte);
            }

            var numFields2 = packet.ReadByte();
            Console.WriteLine("Slot Count: " + numFields2);

            for (var i = 0; i < numFields2; i++)
            {
                var blockint32 = packet.ReadLfgEntry();
                Console.WriteLine("LFG Entry " + i + ": " + blockint32);
            }

            var string1 = packet.ReadCString();
            Console.WriteLine("Comment: " + string1);
        }

        [Parser(Opcode.SMSG_LFG_OFFER_CONTINUE)]
        public static void HandleLfgOfferContinue(Packet packet)
        {
            var entry = packet.ReadLfgEntry();
            Console.WriteLine("LFG Entry: " + entry);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var numFields = packet.ReadByte();
            Console.WriteLine("Random Dungeon Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var int1 = packet.ReadLfgEntry();
                Console.WriteLine("Random Dungeon Entry " + i + ": " + int1);

                ReadLfgRewardBlock(packet);
            }

            var numFields3 = packet.ReadInt32();
            Console.WriteLine("Entry Count: " + numFields3);

            for (var j = 0; j < numFields3; j++)
            {
                var lfgent = packet.ReadLfgEntry();
                Console.WriteLine("LFG Entry " + j + ": " + lfgent);

                var value = (LfgEntryCheckResult)packet.ReadInt32();
                Console.WriteLine("Entry Check Result: " + value);
            }
        }

        [Parser(Opcode.SMSG_LFG_PARTY_INFO)]
        public static void HandleLfgPlayerLockInfoUpdate(Packet packet)
        {
            var numFields = packet.ReadByte();
            Console.WriteLine("Player Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var playerGuid = packet.ReadGuid();
                Console.WriteLine("GUID " + i + ": " + playerGuid);

                var numFields2 = packet.ReadInt32();
                Console.WriteLine("Entry Count: " + numFields2);

                for (var j = 0; j < numFields2; j++)
                    ReadDungeonJoinResults(packet);
            }
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            var int1 = packet.ReadLfgEntry();
            Console.WriteLine("LFG Entry: " + int1);

            var byte1 = (LfgState)packet.ReadByte();
            Console.WriteLine("State: " + byte1);

            var int2 = packet.ReadInt32();
            Console.WriteLine("Group ID: " + int2);

            var int3 = packet.ReadInt32();
            Console.WriteLine("Bosses Killed: " + int3.ToString("X8"));

            var byte2 = packet.ReadByte();
            Console.WriteLine("Silent: " + byte2);

            var numFields = packet.ReadByte();
            Console.WriteLine("Response Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var blockint32 = (LfgRoleFlag)packet.ReadInt32();
                Console.WriteLine("Roles " + i + ": " + blockint32);

                var blockbyte1 = packet.ReadBoolean();
                Console.WriteLine("Self " + i + ": " + blockbyte1);

                var blockbyte2 = packet.ReadBoolean();
                Console.WriteLine("In Dungeon " + i + ": " + blockbyte2);

                var blockbyte3 = packet.ReadBoolean();
                Console.WriteLine("Same Group " + i + ": " + blockbyte3);

                var blockbyte4 = packet.ReadBoolean();
                Console.WriteLine("Answer " + i + ": " + blockbyte4);

                var blockbyte5 = packet.ReadBoolean();
                Console.WriteLine("Accept " + i + ": " + blockbyte5);
            }
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            var int1 = packet.ReadLfgEntry();
            Console.WriteLine("LFG Entry: " + int1);

            var int2 = packet.ReadInt32();
            Console.WriteLine("Average Wait Time: " + int2);

            var int3 = packet.ReadInt32();
            Console.WriteLine("Wait Time: " + int3);

            var int4 = packet.ReadInt32();
            Console.WriteLine("Wait Tanks: " + int4);

            var int5 = packet.ReadInt32();
            Console.WriteLine("Wait Healers: " + int5);

            var int6 = packet.ReadInt32();
            Console.WriteLine("Wait DPS: " + int6);

            var byte1 = packet.ReadBoolean();
            Console.WriteLine("Tanks Needed: " + byte1);

            var byte2 = packet.ReadBoolean();
            Console.WriteLine("Healers Needed: " + byte2);

            var byte3 = packet.ReadByte();
            Console.WriteLine("Damage Dealers Needed: " + byte3);

            var int7 = packet.ReadInt32();
            Console.WriteLine("Queued Time: " + int7);
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            var int1 = (LfgRoleCheckStatus)packet.ReadInt32();
            Console.WriteLine("Check Status: " + int1);

            var init = packet.ReadBoolean();
            Console.WriteLine("Is Beginning: " + init);

            var numFields = packet.ReadByte();
            Console.WriteLine("Entry Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var blockint32 = packet.ReadLfgEntry();
                Console.WriteLine("LFG Entry " + i + ": " + blockint32);
            }

            var numFields2 = packet.ReadByte();
            Console.WriteLine("Player Count: " + numFields2);

            for (var i = 0; i < numFields2; i++)
            {
                var playerGuid = packet.ReadGuid();
                Console.WriteLine("GUID " + i + ": " + playerGuid);

                var blockbyte1 = packet.ReadBoolean();
                Console.WriteLine("Ready " + i + ": " + blockbyte1);

                var blockint32 = (LfgRoleFlag)packet.ReadInt32();
                Console.WriteLine("Roles " + i + ": " + blockint32);

                var blockbyte2 = packet.ReadByte();
                Console.WriteLine("Level " + i + ": " + blockbyte2);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            var int1 = (LfgRoleCheckStatus)packet.ReadInt32();
            Console.WriteLine("Check Status: " + int1);

            var int2 = packet.ReadInt32();
            Console.WriteLine("Reason: " + int2);

            if (int1 != LfgRoleCheckStatus.Unknown)
                return;

            var numFields = packet.ReadByte();
            Console.WriteLine("Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var playerGuid = packet.ReadGuid();
                Console.WriteLine("GUID " + i + ": " + playerGuid);

                var cnt2 = packet.ReadInt32();
                Console.WriteLine("Dungeon Count: " + cnt2);

                for (var j = 0; j < cnt2; j++)
                    ReadDungeonJoinResults(packet);
            }
        }

        public static void ReadDungeonJoinResults(Packet packet)
        {
            var lfgent = packet.ReadLfgEntry();
            Console.WriteLine("LFG Entry: " + lfgent);

            var value = (LfgEntryCheckResult)packet.ReadInt32();
            Console.WriteLine("Entry Check Result: " + value);
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHOSEN)]
        public static void HandleLfgRoleCheckResult(Packet packet)
        {
            var playerGuid = packet.ReadGuid();
            Console.WriteLine("GUID: " + playerGuid);

            var boolean = packet.ReadBoolean();
            Console.WriteLine("Accept: " + boolean);

            var Role = (LfgRoleFlag)packet.ReadInt32();
            Console.WriteLine("Roles: " + Role);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_SEARCH)]
        public static void HandleLfgUpdateSearch(Packet packet)
        {
            var byte1 = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean: " + byte1);
        }

        [Parser(Opcode.CMSG_LFG_SET_ROLES)]
        public static void HandleLfgSetRoles(Packet packet)
        {
            var role = (LfgRoleFlag)packet.ReadByte();
            Console.WriteLine("Roles: " + role);
        }

        [Parser(Opcode.CMSG_LFG_TELEPORT)]
        public static void HandleLfgTeleport(Packet packet)
        {
            var teleout = packet.ReadBoolean();
            Console.WriteLine("Teleport Out: " + teleout);
        }

        [Parser(Opcode.SMSG_LFG_TELEPORT_DENIED)]
        public static void HandleLfgError(Packet packet)
        {
            var err = (LfgError)packet.ReadInt32();
            Console.WriteLine("Error: " + err);
        }

        [Parser(Opcode.CMSG_SEARCH_LFG_JOIN)]
        [Parser(Opcode.CMSG_SEARCH_LFG_LEAVE)]
        public static void HandleLfgSearch(Packet packet)
        {
            var lfgId = packet.ReadLfgEntry();
            Console.WriteLine("LFG Entry: " + lfgId);
        }

        [Parser(Opcode.SMSG_LFG_OPEN_FROM_GOSSIP)]
        public static void HandleLfgGossip(Packet packet)
        {
            var unk = packet.ReadLfgEntry();
            Console.WriteLine("LFG Entry: " + unk);
        }

        [Parser(Opcode.SMSG_UPDATE_LFG_LIST)]
        public static void HandleUpdateLfgList(Packet packet)
        {
            var type = (LfgType)packet.ReadInt32();
            Console.WriteLine("LFG Type: " + type);

            var id = packet.ReadInt32();
            Console.WriteLine("Dungeon ID: " + id);

            var unkBool = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean 1: " + unkBool);

            if (unkBool)
            {
                var cnt = packet.ReadInt32();
                Console.WriteLine("Unk Int32 1: " + cnt);

                for (var i = 0; i < cnt; i++)
                {
                    var guid = packet.ReadGuid();
                    Console.WriteLine("Unk GUID 1: " + guid);
                }
            }

            var cnt2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + cnt2);

            var int2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 3: " + int2);

            for (var i = 0; i < cnt2; i++)
            {
                var guid2 = packet.ReadGuid();
                Console.WriteLine("Unk GUID 2 " + ": " + guid2);

                var flags = (LfgUpdateFlag)packet.ReadInt32();
                Console.WriteLine("Update Flags " + i + ": " + flags);

                if (flags.HasFlag(LfgUpdateFlag.Comment))
                {
                    var str = packet.ReadCString();
                    Console.WriteLine("Comment " + i + ": " + str);
                }

                if (flags.HasFlag(LfgUpdateFlag.Roles))
                {
                    for (var j = 0; j < 3; j++)
                    {
                        var unk8 = packet.ReadByte();
                        Console.WriteLine("Unk Byte 1 " + i + ": " + unk8);
                    }
                }

                if (!flags.HasFlag(LfgUpdateFlag.Unknown4))
                    continue;

                var guid3 = packet.ReadGuid();
                Console.WriteLine("Unk GUID 3 " + i + ": " + guid3);

                var unk80 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 4 " + i + ": " + unk80);
            }

            var cnt3 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 5: " + cnt3);

            var int3 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 6: " + int3);

            for (var i = 0; i < cnt3; i++)
            {
                var guid4 = packet.ReadGuid();
                Console.WriteLine("Unk GUID 4 " + ": " + guid4);

                var flags2 = (LfgUpdateFlag)packet.ReadInt32();
                Console.WriteLine("Update Flags " + i + ": " + flags2);

                if (flags2.HasFlag(LfgUpdateFlag.CharacterInfo))
                {
                    var byte1 = packet.ReadByte();
                    Console.WriteLine("Unk Byte 2 " + i + ": " + byte1);

                    var byte2 = packet.ReadByte();
                    Console.WriteLine("Unk Byte 3 " + i + ": " + byte2);

                    var byte3 = packet.ReadByte();
                    Console.WriteLine("Unk Byte 4 " + i + ": " + byte3);

                    for (var j = 0; j < 3; j++)
                    {
                        var unkByte3 = packet.ReadByte();
                        Console.WriteLine("Unk Byte 5 " + i + ": " + unkByte3);
                    }

                    var integer1 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 6 " + i + ": " + integer1);

                    var integer2 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 7 " + i + ": " + integer2);

                    var integer3 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 8 " + i + ": " + integer3);

                    var integer4 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 9 " + i + ": " + integer4);

                    var integer5 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 10 " + i + ": " + integer5);

                    var integer6 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 11 " + i + ": " + integer6);

                    var float1 = packet.ReadSingle();
                    Console.WriteLine("Unk Single 1 " + i + ": " + float1);

                    var float2 = packet.ReadSingle();
                    Console.WriteLine("Unk Single 2 " + i + ": " + float2);

                    var integer7 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 12 " + i + ": " + integer7);

                    var integer8 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 13 " + i + ": " + integer8);

                    var integer9 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 14 " + i + ": " + integer9);

                    var integer10 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 15 " + i + ": " + integer10);

                    var integer11 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 16 " + i + ": " + integer11);

                    var float3 = packet.ReadSingle();
                    Console.WriteLine("Unk Single 3 " + i + ": " + float3);

                    var integer12 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 17 " + i + ": " + integer12);

                    var integer13 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 18 " + i + ": " + integer13);

                    var integer14 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 19 " + i + ": " + integer14);

                    var integer15 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 20 " + i + ": " + integer15);

                    var integer16 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 21 " + i + ": " + integer16);

                    var integer17 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 22 " + i + ": " + integer17);
                }

                if (flags2.HasFlag(LfgUpdateFlag.Comment))
                {
                    var str = packet.ReadCString();
                    Console.WriteLine("Comment " + i + ": " + str);
                }

                if (flags2.HasFlag(LfgUpdateFlag.Unknown1))
                {
                    var int8 = packet.ReadByte();
                    Console.WriteLine("Unk Byte 6 " + i + ": " + int8);
                }

                if (flags2.HasFlag(LfgUpdateFlag.Guid))
                {
                    var guid5 = packet.ReadGuid();
                    Console.WriteLine("Unk GUID 5 " + i + ": " + guid5);
                }

                if (flags2.HasFlag(LfgUpdateFlag.Roles))
                {
                    var byte10 = packet.ReadByte();
                    Console.WriteLine("Unk Byte 7 " + byte10 + ": " + byte10);
                }

                if (flags2.HasFlag(LfgUpdateFlag.Unknown2))
                {
                    var int20 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 23 " + i + ": " + int20);
                }

                if (flags2.HasFlag(LfgUpdateFlag.Unknown3))
                {
                    var byte40 = packet.ReadByte();
                    Console.WriteLine("Unk Byte 8 " + i + ": " + byte40);
                }

                if (!flags2.HasFlag(LfgUpdateFlag.Unknown4))
                    continue;

                var guid6 = packet.ReadGuid();
                Console.WriteLine("Unk GUID 6 " + i + ": " + guid6);

                var intUnk = packet.ReadInt32();
                Console.WriteLine("Unk Int32 24 " + i + ": " + intUnk);
            }
        }
    }
}
