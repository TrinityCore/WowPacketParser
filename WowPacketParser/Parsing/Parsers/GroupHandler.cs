using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

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
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.ReadBoolean("Unk bool");
            }

            packet.ReadGuid("Group GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.ReadInt32("Counter");

            var numFields = packet.ReadInt32("Member Count");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadCString("[" + i + "] Name");
                packet.ReadGuid("[" + i + "] GUID");
                packet.ReadEnum<GroupMemberStatusFlag>("[" + i + "] Status", TypeCode.Byte);
                packet.ReadByte("[" + i + "] Sub Group");
                packet.ReadEnum<GroupUpdateFlag>("[" + i + "] Update Flags", TypeCode.Byte);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.ReadEnum<LfgRoleFlag>("[" + i + "] Role", TypeCode.Byte);
            }

            packet.ReadGuid("Leader GUID");

            if (numFields <= 0)
                return;

            packet.ReadEnum<LootMethod>("Loot Method", TypeCode.Byte);
            packet.ReadGuid("Looter GUID");
            packet.ReadEnum<ItemQuality>("Loot Threshold", TypeCode.Byte);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadEnum<MapDifficulty>("Dungeon Difficulty", TypeCode.Byte);

            packet.ReadEnum<MapDifficulty>("Raid Difficulty", TypeCode.Byte);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958) &&
                ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545)) // Removed somewhere between wotlk and cataclysm
                packet.ReadByte("Unk Byte"); // Has something to do with difficulty too
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS_FULL, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePartyMemberStats422(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PARTY_MEMBER_STATS_FULL))
                packet.ReadBoolean("Add arena opponent");

            packet.ReadPackedGuid("GUID");
            var updateFlags = (GroupUpdateFlag)packet.ReadInt32("Update Flags");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Status))
                packet.ReadEnum<GroupMemberStatusFlag>("Status", TypeCode.Int16);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.CurrentHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Current Health");
                else
                    packet.ReadUInt16("Current Health");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.MaxHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Max Health");
                else
                    packet.ReadUInt16("Max Health");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PowerType))
                packet.ReadEnum<PowerType>("Power type", TypeCode.Byte);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.CurrentPower))
                packet.ReadInt16("Current Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.MaxPower))
                packet.ReadInt16("Max Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Level))
                packet.ReadInt16("Level");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Zone))
                packet.ReadEntryWithName<Int16>(StoreNameType.Zone, "Zone Id");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Position))
                packet.ReadInt16("Unk");

            if (updateFlags.HasAnyFlag(0x200))
            {
                packet.ReadInt16("X");
                packet.ReadInt16("Y");
                packet.ReadInt16("Z");
            }

            if (updateFlags.HasAnyFlag(0x400))
            {
                packet.ReadByte("Unk byte");
                var mask = packet.ReadUInt64("Aura mask");
                var cnt = packet.ReadUInt32("Aura count");
                for (var i = 0; i < cnt; ++i)
                {
                    if ((mask & (1ul << i)) == 0)
                        continue;

                    packet.ReadUInt32("Spell Id", i);
                    var aflags = packet.ReadEnum<AuraFlag>("AuraFlags", TypeCode.UInt16, i);
                    if (aflags.HasAnyFlag(AuraFlag.Scalable))
                        for (var j = 0; j < 3; ++j)
                            packet.ReadInt32("Effect BasePoints", i, j);
                }
            }

            if (updateFlags.HasAnyFlag(0x800))
                packet.ReadUInt64("Pet GUID");

            if (updateFlags.HasAnyFlag(0x1000))
                packet.ReadCString("Pet Name");

            if (updateFlags.HasAnyFlag(0x2000))
                packet.ReadUInt16("Pet Model Id");

            if (updateFlags.HasAnyFlag(0x4000))
                packet.ReadUInt32("Pet Current Health");

            if (updateFlags.HasAnyFlag(0x8000))
                packet.ReadUInt32("Pet Max Health");

            if (updateFlags.HasAnyFlag(0x10000))
                packet.ReadEnum<PowerType>("Pet Power type", TypeCode.Byte);

            if (updateFlags.HasAnyFlag(0x20000))
                packet.ReadInt16("Pet Current Power");

            if (updateFlags.HasAnyFlag(0x40000))
                packet.ReadInt16("Pet Max Power");

            if (updateFlags.HasAnyFlag(0x80000))
            {
                packet.ReadByte("Unk byte");
                var mask = packet.ReadUInt64("Pet Aura mask");
                var cnt = packet.ReadUInt32("Pet Aura count");
                for (var i = 0; i < cnt; ++i)
                {
                    if ((mask & (1ul << i)) == 0)
                        continue;

                    packet.ReadUInt32("Spell Id", i);
                    var aflags = packet.ReadEnum<AuraFlag>("AuraFlags", TypeCode.UInt16, i);
                    if (aflags.HasAnyFlag(AuraFlag.Scalable))
                        for (var j = 0; j < 3; ++j)
                            packet.ReadInt32("Effect BasePoints", i, j);
                }
            }

            if (updateFlags.HasAnyFlag(0x100000))
                packet.ReadInt32("Vehicle Seat?");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Unk200000))
            {
                packet.ReadInt32("Unk int32");
                packet.ReadInt32("Unk int32");
                packet.ReadCString("Unk string");
            }
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS_FULL, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) &&
                packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PARTY_MEMBER_STATS_FULL))
                packet.ReadBoolean("Add arena opponent");

            packet.ReadPackedGuid("GUID");
            var updateFlags = packet.ReadEnum<GroupUpdateFlag>("Update Flags", TypeCode.UInt32);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Status))
                packet.ReadEnum<GroupMemberStatusFlag>("Status", TypeCode.Int16);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.CurrentHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Current Health");
                else
                    packet.ReadUInt16("Current Health");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.MaxHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Max Health");
                else
                    packet.ReadUInt16("Max Health");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PowerType))
                packet.ReadEnum<PowerType>("Power type", TypeCode.Byte);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.CurrentPower))
                packet.ReadInt16("Current Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.MaxPower))
                packet.ReadInt16("Max Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Level))
                packet.ReadInt16("Level");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Zone))
                packet.ReadEntryWithName<Int16>(StoreNameType.Zone, "Zone Id");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Position))
            {
                packet.ReadInt16("Position X");
                packet.ReadInt16("Position Y");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.Auras))
            {
                var auraMask = packet.ReadUInt64("Auramask");

                var maxAura = ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) ? 64 : 56;
                for (var i = 0; i < maxAura; ++i)
                {
                    if ((auraMask & (1ul << i)) == 0)
                        continue;

                    var aura = ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) ? packet.ReadInt32() : packet.ReadUInt16();
                    packet.WriteLine("Slot: [" + i + "] Spell ID: " + StoreGetters.GetName(StoreNameType.Spell, aura));
                    packet.ReadEnum<AuraFlag>("Slot: [" + i + "] Aura flag", TypeCode.Byte);
                }
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetGuid))
                packet.ReadGuid("Pet GUID");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetName))
                packet.ReadCString("Pet Name");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetModelId))
                packet.ReadInt16("Pet Modelid");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetCurrentHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Pet Current Health");
                else
                    packet.ReadUInt16("Pet Current Health");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetMaxHealth))
            {
                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Pet Max Health");
                else
                    packet.ReadUInt16("Pet Max Health");
            }

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetPowerType))
                packet.ReadEnum<PowerType>("Pet Power type", TypeCode.Byte);

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetCurrentPower))
                packet.ReadInt16("Pet Current Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetMaxPower))
                packet.ReadInt16("Pet Max Power");

            if (updateFlags.HasAnyFlag(GroupUpdateFlag.PetAuras))
            {
                var auraMask = packet.ReadUInt64("Pet Auramask");

                var maxAura = ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) ? 64 : 56;
                for (var i = 0; i < maxAura; ++i)
                {
                    if ((auraMask & (1ul << i)) == 0)
                        continue;

                    var aura = ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) ? packet.ReadInt32() : packet.ReadUInt16();
                    packet.WriteLine("Slot: [" + i + "] Spell ID: " + StoreGetters.GetName(StoreNameType.Spell, aura));
                    packet.ReadEnum<AuraFlag>("Slot: [" + i + "] Aura flag", TypeCode.Byte);
                }
            }

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) && // no idea when this was added exactly, doesn't exist in 2.4.1
                updateFlags.HasAnyFlag(GroupUpdateFlag.VehicleSeat))
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

        [Parser(Opcode.CMSG_GROUP_INVITE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGroupInvite422(Packet packet)
        {
            // note: this handler is different in 4.3.0, it got a bit fancy.
            var guidBytes = new byte[8];

            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadInt32("Unk0"); // Always 0
            packet.ReadInt32("Unk1"); // Non-zero in cross realm parties (1383)
            packet.ReadCString("Name");

            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();
            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();
            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();
            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();
            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();
            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();
            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();

            packet.ReadCString("Realm Name"); // Non-empty in cross realm parties

            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();

            // Non-zero in cross realm parties
            packet.WriteLine("GUID: {0}", new Guid(BitConverter.ToUInt64(guidBytes, 0)));
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

        [Parser(Opcode.CMSG_GROUP_ACCEPT_DECLINE)]
        public static void HandleGroupAcceptDecline(Packet packet)
        {
            packet.ReadBit("Accepted");
            // 7 0 bits here
            packet.ReadUInt32("Unknown");
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.ReadUInt32("LFG Boot Cooldown");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadGuid("Player Guid"); // Usually 0
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
            {
                // Packet is sent in two different methods. One sends a byte and one doesn't
                if (packet.CanRead())
                    packet.ReadBoolean("Ready");
            }
            else
                packet.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_RAID_READY_CHECK_CONFIRM)]
        public static void HandleRaidReadyCheckConfirm(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Ready");
        }

        [Parser(Opcode.MSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
                packet.ReadGuid("GUID");

            var position = packet.ReadVector2();
            packet.WriteLine("Position: " + position);
        }

        [Parser(Opcode.CMSG_GROUP_RAID_CONVERT)]
        public static void HandleGroupRaidConvert(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.ReadBoolean("ToRaid");
        }
    }
}
