using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var grouptype = packet.ReadEnum<GroupTypeFlag>("Group Type", TypeCode.Byte);
            packet.ReadByte("Sub Group");
            packet.ReadEnum<GroupUpdateFlag>("Flags", TypeCode.Byte);
            packet.ReadByte("Player's Role");

            if (grouptype.HasAnyFlag(GroupTypeFlag.LookingForDungeon))
            {
                packet.ReadEnum<InstanceStatus>("Group Type Status", TypeCode.Byte);
                packet.ReadLfgEntry("LFG Entry");
            }

            packet.ReadGuid("Group GUID");
            packet.ReadInt32("Counter");
            var numFields = packet.ReadInt32("Member Count");

            for (var i = 0; i < numFields; i++)
            {
                packet.ReadCString("[" + i + "] Name");
                packet.ReadGuid("[" + i + "] GUID");
                packet.ReadEnum<GroupMemberStatusFlag>("[" + i + "] Status", TypeCode.Byte);
                packet.ReadByte("[" + i + "] Sub Group");
                packet.ReadEnum<GroupUpdateFlag>("[" + i + "] Update Flags", TypeCode.Byte);
                packet.ReadEnum<LfgRoleFlag>("[" + i + "] Role", TypeCode.Byte);
            }

            packet.ReadGuid("Leader GUID");

            if (numFields <= 0)
                return;

            packet.ReadEnum<LootMethod>("Loot Method", TypeCode.Byte);
            packet.ReadGuid("Looter GUID");
            packet.ReadEnum<ItemQuality>("Loot Threshold", TypeCode.Byte);
            packet.ReadEnum<MapDifficulty>("Dungeon Difficulty", TypeCode.Byte);
            packet.ReadEnum<MapDifficulty>("Raid Difficulty", TypeCode.Byte);
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS_FULL)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PARTY_MEMBER_STATS_FULL))
                packet.ReadByte("Unk byte");

            packet.ReadPackedGuid("GUID");
            var updateFlags = packet.ReadEnum<GroupUpdateFlag>("Update Flags", TypeCode.Int32);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Status))
                packet.ReadEnum<GroupMemberStatusFlag>("Status", TypeCode.Int16);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.CurrentHealth))
                packet.ReadInt32("Current Health");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.MaxHealth))
                packet.ReadInt32("Max Health");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PowerType))
                packet.ReadEnum<PowerType>("Power type", TypeCode.Byte);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.CurrentPower))
                packet.ReadInt16("Current Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.MaxPower))
                packet.ReadInt16("Max Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Level))
                packet.ReadInt16("Level");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Zone))
                packet.ReadInt16("Zone ID");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Position))
            {
                packet.ReadInt16("Position X");
                packet.ReadInt16("Position Y");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Auras))
            {
                var auraMask = packet.ReadUInt64("Auramask");
                for (var i = 0; i < 64; ++i)
                {
                    if ((auraMask & ((ulong)1 << i)) != 0)
                    {
                        packet.Writer.WriteLine("Slot: [" + i + "] Spell ID: " + StoreGetters.GetName(StoreNameType.Spell, packet.ReadInt32()));
                        packet.ReadEnum<AuraFlag>("Slot: [" + i + "] Aura flag", TypeCode.Byte);
                    }
                }
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetGuid))
                packet.ReadGuid("Pet GUID");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetName))
                packet.ReadCString("Pet Name");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetModelId))
                packet.ReadInt16("Pet Modelid");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetCurrentHealth))
                packet.ReadInt32("Pet Current Health");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetMaxHealth))
                packet.ReadInt32("Pet Max Health");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetPowerType))
                packet.ReadEnum<PowerType>("Pet Power type", TypeCode.Byte);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetCurrentPower))
                packet.ReadInt16("Pet Current Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetMaxPower))
                packet.ReadInt16("Pet Max Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetAuras))
            {
                var auraMask = packet.ReadUInt64("Pet Auramask");

                for (var i = 0; i < 64; ++i)
                {
                    if ((auraMask & ((ulong)1 << i)) != 0)
                    {
                        packet.Writer.WriteLine("Slot: [" + i + "] Spell ID: " + StoreGetters.GetName(StoreNameType.Spell, packet.ReadInt32()));
                        packet.ReadEnum<AuraFlag>("Slot: [" + i + "] Aura flag", TypeCode.Byte);
                    }
                }
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.VehicleSeat))
                packet.ReadInt32("Vehicle Seat");
        }


        [Parser(Opcode.CMSG_GROUP_SET_LEADER)]
        public static void HandleGroupSetLeader(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_GROUP_SET_LEADER)]
        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleGroupInvite(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.ReadBoolean("invited/already in group flag?");
            packet.ReadCString("Name");
            packet.ReadInt32("Unk Int32 1");
            var count = packet.ReadByte("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadUInt32("Unk Uint32", i);

            packet.ReadInt32("Unk Int32 2");
        }

        [Parser(Opcode.CMSG_GROUP_UNINVITE_GUID)]
        public static void HandleGroupUninviteGuid(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("Reason");
        }

        [Parser(Opcode.CMSG_GROUP_ACCEPT)]
        public static void HandleGroupAccept(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.MSG_RANDOM_ROLL)]
        public static void HandleRandomRollPackets(Packet packet)
        {
            packet.ReadInt32("Minimum");
            packet.ReadInt32("Maximum");

            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.ReadInt32("Roll");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_PARTY_COMMAND_RESULT)]
        public static void HandlePartyCommandResult(Packet packet)
        {
            packet.ReadEnum<PartyCommand>("Command", TypeCode.UInt32);
            packet.ReadCString("Member");
            packet.ReadEnum<PartyResult>("Result", TypeCode.UInt32);
            packet.ReadUInt32("LFG Boot Cooldown");
        }

        [Parser(Opcode.SMSG_RAID_GROUP_ONLY)]
        public static void HandleRaidGroupOnly(Packet packet)
        {
            packet.ReadInt32("Time left");
            packet.ReadEnum<InstanceStatus>("Group Type Status?", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_REAL_GROUP_UPDATE)]
        public static void HandleRealGroupUpdate(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Unk UInt32");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GROUP_CHANGE_SUB_GROUP)]
        public static void HandleGroupChangesubgroup(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadByte("Group");
        }

        [Parser(Opcode.MSG_RAID_READY_CHECK)]
        public static void HandleRaidReadyCheck(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                packet.ReadBoolean("Ready");
            else
                packet.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_RAID_READY_CHECK_CONFIRM)]
        public static void HandleRaidReadyCheckConfirm(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Ready");
        }

        [Parser(Opcode.SMSG_SERVER_MESSAGE)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.ReadUInt32("Unk UInt32");
            packet.ReadCString("Message");
        }
    }
}
