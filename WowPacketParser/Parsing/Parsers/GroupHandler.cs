using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var grouptype = (GroupTypeFlag)packet.ReadByte();
            Console.WriteLine("Group Type: " + grouptype);

            var subgroup = packet.ReadByte();
            Console.WriteLine("Sub Group: " + subgroup);

            var flags = (GroupUpdateFlag)packet.ReadByte();
            Console.WriteLine("Flags: " + flags);

            var myroles = packet.ReadByte();
            Console.WriteLine("own roles: " + myroles);

            if (grouptype.HasFlag(GroupTypeFlag.LookingForDungeon))
            {
                var dungeonStatus = (InstanceStatus)packet.ReadByte();
                Console.WriteLine("Dungeon Status: " + dungeonStatus);

                var lfgentry = packet.ReadLfgEntry();
                Console.WriteLine("LFG Entry: " + lfgentry);
            }

            var groupGuid = packet.ReadGuid();
            Console.WriteLine("Group GUID: " + groupGuid);

            var counter = packet.ReadInt32();
            Console.WriteLine("Counter: " + counter);

            var numFields = packet.ReadInt32();
            Console.WriteLine("Member Count: " + numFields);

            for (var i = 0; i < numFields; i++)
            {
                var name = packet.ReadCString();
                Console.WriteLine("Name " + i + ": " + name);

                var guid = packet.ReadGuid();
                Console.WriteLine("GUID " + i + ": " + guid);

                var status = (GroupMemberStatusFlag)packet.ReadByte();
                Console.WriteLine("Status " + i + ": " + status);

                var subgroup1 = packet.ReadByte();
                Console.WriteLine("Sub Group" + i + ": " + subgroup1);

                var flags1 = (GroupUpdateFlag)packet.ReadByte();
                Console.WriteLine("Update Flags " + i + ": " + flags1);

                var role = (LfgRoleFlag)packet.ReadByte();
                Console.WriteLine("Role " + i + ": " + role);
            }

            var leaderGuid = packet.ReadGuid();
            Console.WriteLine("Leader GUID: " + leaderGuid);

            if (numFields <= 0)
                return;

            var loot = (LootMethod)packet.ReadByte();
            Console.WriteLine("Loot Method: " + loot);

            var looterGuid = packet.ReadGuid();
            Console.WriteLine("Looter GUID: " + looterGuid);

            var item = (ItemQuality)packet.ReadByte();
            Console.WriteLine("Loot Threshold: " + item);

            var dungeonDifficulty = (MapDifficulty)packet.ReadByte();
            Console.WriteLine("Dungeon Difficulty: " + dungeonDifficulty);

            var raidDifficulty = (MapDifficulty)packet.ReadByte();
            Console.WriteLine("Raid Difficulty: " + raidDifficulty);

            var unkbyte3 = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unkbyte3);
        }

        [Parser(Opcode.SMSG_GROUP_SET_LEADER)]
        public static void HandleGroupSetLeader(Packet packet)
        {
            var name = packet.ReadCString();
            Console.WriteLine("Name: " + name);
        }

        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleGroupInvite(Packet packet)
        {
            var name = packet.ReadCString();
            Console.WriteLine("Name: " + name);

            var unkint = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unkint);
        }

        [Parser(Opcode.CMSG_GROUP_ACCEPT)]
        public static void HandleGroupAccept(Packet packet)
        {
            var unkint = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unkint);
        }

        [Parser(Opcode.MSG_RANDOM_ROLL)]
        public static void HandleRandomRollPackets(Packet packet)
        {
            var min = packet.ReadInt32();
            Console.WriteLine("Minimum: " + min);

            var max = packet.ReadInt32();
            Console.WriteLine("Maximum: " + max);

            if (packet.GetDirection() == Direction.ClientToServer)
                return;

            var roll = packet.ReadInt32();
            Console.WriteLine("Roll: " + roll);

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }
    }
}
