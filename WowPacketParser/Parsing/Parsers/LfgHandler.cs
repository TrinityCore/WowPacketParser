using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
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

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_LFG_PLAYER_REWARD))
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
            packet.ReadLfgEntry("Random LFG Entry");
            packet.ReadLfgEntry("Actual LFG Entry");

            ReadLfgRewardBlock(packet);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_PLAYER)]
        public static void HandleLfgUpdatePlayer(Packet packet)
        {
            var byte1 = (LfgUpdateType)packet.ReadByte();
            Console.WriteLine("Update Type: " + byte1);

            var extraInfo = packet.ReadBoolean();
            Console.WriteLine("ExtraInfo: " + extraInfo);

            if (!extraInfo)
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

            var extraInfo = packet.ReadBoolean();
            Console.WriteLine("ExtraInfo: " + extraInfo);

            if (!extraInfo)
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

        [Parser(Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER)]
        public static void HandleLfgGossip(Packet packet)
        {
            var unk = packet.ReadLfgEntry();
            Console.WriteLine("LFG Entry: " + unk);
        }

        [Parser(Opcode.SMSG_UPDATE_LFG_LIST)]
        public static void HandleUpdateLfgList(Packet packet)
        {
            packet.ReadEnum<LfgType>("LFG Type", TypeCode.Int32);

            Console.WriteLine("Dungeon ID: " + StoreGetters.GetName(StoreNameType.LFGDungeon, packet.ReadInt32()));

            var unkBool = packet.ReadBoolean("Unknown bool 1");

            if (unkBool)
            {
                var count = packet.ReadInt32("Unknown count");

                for (var i = 0; i < count; i++)
                {
                    packet.ReadGuid("[" + i + "] Unk Player GUID");
                }
            }

            var count2 = packet.ReadInt32("Group count");
            packet.ReadInt32("Unknown group count");

            for (var i = 0; i < count2; i++)
            {
                packet.ReadGuid("[" + i + "] Group GUID");

                var flags = packet.ReadEnum<LfgUpdateFlag>("[" + i + "] Update Flags", TypeCode.Int32);

                if (flags.HasAnyFlag(LfgUpdateFlag.Comment))
                {
                    packet.ReadCString("[" + i + "] Comment");
                }

                if (flags.HasAnyFlag(LfgUpdateFlag.Roles))
                {
                    for (var j = 0; j < 3; j++)
                    {
                        packet.ReadByte("[" + i + "] Unk byte 1 " + i); // Group have this role ?
                    }
                }

                if (!flags.HasAnyFlag(LfgUpdateFlag.Binded))
                    continue;

                packet.ReadGuid("[" + i + "] Instance GUID");
                packet.ReadInt32("[" + i + "] Encounters");
            }

            var count3 = packet.ReadInt32("Players count");
            packet.ReadInt32("Unknown player count");

            for (var i = 0; i < count3; i++)
            {
                packet.ReadGuid("[" + i + "] Player GUID");
                var flags2 = packet.ReadEnum<LfgUpdateFlag>("[" + i + "] Update Flags", TypeCode.Int32);

                if (flags2.HasAnyFlag(LfgUpdateFlag.CharacterInfo))
                {
                    packet.ReadByte("[" + i + "] Level");
                    packet.ReadByte("[" + i + "] Class");
                    packet.ReadByte("[" + i + "] Race");

                    for (var j = 0; j < 3; j++)
                    {
                        packet.ReadByte("[" + i + "] Talents spent in row [" + (j + 1) + "]");
                    }

                    packet.ReadInt32("[" + i + "] Armor");
                    packet.ReadInt32("[" + i + "] Spell Damage Bonus");
                    packet.ReadInt32("[" + i + "] Spell Healing Bonus");
                    packet.ReadInt32("[" + i + "] Melee Crit Rating");
                    packet.ReadInt32("[" + i + "] Ranged Crit Rating");
                    packet.ReadInt32("[" + i + "] Spell Crit Rating");
                    packet.ReadSingle("[" + i + "] Mana / 5 (Out of combat)");
                    packet.ReadSingle("[" + i + "] Mana / 5 (In combat)");
                    packet.ReadInt32("[" + i + "] Attackpower");
                    packet.ReadInt32("[" + i + "] Agility");
                    packet.ReadInt32("[" + i + "] Max Health");
                    packet.ReadInt32("[" + i + "] Max Power");
                    packet.ReadInt32("[" + i + "] Free talent points");
                    packet.ReadSingle("[" + i + "] Unknown float");
                    packet.ReadInt32("[" + i + "] Defence Rating");
                    packet.ReadInt32("[" + i + "] Dodge Rating");
                    packet.ReadInt32("[" + i + "] Block Rating");
                    packet.ReadInt32("[" + i + "] Parry Rating");
                    packet.ReadInt32("[" + i + "] Crit Rating");
                    packet.ReadInt32("[" + i + "] Expertise Rating");
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.Comment))
                {
                    packet.ReadCString("[" + i + "] Comment");
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.GroupLeader))
                {
                    packet.ReadBoolean("[" + i + "] Is Group Leader");
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.Guid))
                {
                    packet.ReadGuid("[" + i + "] Group GUID");
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.Roles))
                {
                    packet.ReadEnum<LfgRoleFlag>("[" + i + "] Role", TypeCode.Byte);
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.Area))
                {
                    packet.ReadInt32("[" + i + "] Area ID");
                }

                if (flags2.HasAnyFlag(LfgUpdateFlag.Unknown7))
                {
                    packet.ReadBoolean("[" + i + "] Unknown byte");
                }

                if (!flags2.HasAnyFlag(LfgUpdateFlag.Binded))
                    continue;

                packet.ReadGuid("[" + i + "] Instance GUID");
                packet.ReadInt32("[" + i + "] Encounters");
            }
        }
    }
}
