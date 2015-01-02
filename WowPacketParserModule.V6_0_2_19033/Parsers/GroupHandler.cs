using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_MINIMAP_PING)]
        public static void HandleClientMinimapPing(Packet packet)
        {
            packet.ReadVector2("Position");
            packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_MINIMAP_PING)]
        public static void HandleServerMinimapPing(Packet packet)
        {
            packet.ReadPackedGuid128("Sender");
            packet.ReadVector2("Position");
        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            packet.ReadByte("PartyFlags");
            packet.ReadByte("PartyIndex");
            packet.ReadByte("PartyType");

            packet.ReadInt32("MyIndex");
            packet.ReadPackedGuid128("LeaderGUID");
            packet.ReadInt32("SequenceNum");
            packet.ReadPackedGuid128("PartyGUID");

            var int13 = packet.ReadInt32("PlayerListCount");
            for (int i = 0; i < int13; i++)
            {
                packet.ResetBitReader();
                var bits76 = packet.ReadBits(6);

                packet.ReadPackedGuid128("Guid", i);

                packet.ReadByte("Connected", i);
                packet.ReadByte("Subgroup", i);
                packet.ReadByte("Flags", i);
                packet.ReadByte("RolesAssigned", i);
                packet.ReadByte("Unk Byte", i);

                packet.ReadWoWString("Name", bits76, i);
            }

            packet.ResetBitReader();

            var bit68 = packet.ReadBit("HasLfgInfo");
            var bit144 = packet.ReadBit("HasLootSettings");
            var bit164 = packet.ReadBit("HasDifficultySettings");

            if (bit68)
            {
                packet.ReadByte("MyLfgFlags");
                packet.ReadInt32("LfgSlot");
                packet.ReadInt32("MyLfgRandomSlot");
                packet.ReadByte("MyLfgPartialClear");
                packet.ReadSingle("MyLfgGearDiff");
                packet.ReadByte("MyLfgStrangerCount");
                packet.ReadByte("MyLfgKickVoteCount");
                packet.ReadByte("LfgBootCount");

                packet.ResetBitReader();

                packet.ReadBit("LfgAborted");
                packet.ReadBit("MyLfgFirstReward");
            }

            if (bit144)
            {
                packet.ReadByte("LootMethod");
                packet.ReadPackedGuid128("LootMaster");
                packet.ReadByte("LootThreshold");
            }

            if (bit164)
            {
                packet.ReadInt32("Unk Int4");
                //for (int i = 0; i < 2; i++)
                //{
                    packet.ReadInt32("DungeonDifficultyID");
                    packet.ReadInt32("RaidDifficultyID");
                //}
            }
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATS)]
        public static void HandlePartyMemberStats(Packet packet)
        {
            packet.ReadBit("ForEnemy");
            packet.ReadBit("FullUpdate");
            var bit761 = packet.ReadBit("HasUnk761");
            var bit790 = packet.ReadBit("HasStatus");
            var bit763 = packet.ReadBit("HasPowerType");
            var bit322 = packet.ReadBit("HasUnk322");
            var bit28 = packet.ReadBit("HasCurrentHealth");
            var bit316 = packet.ReadBit("HasMaxHealth");
            var bit748 = packet.ReadBit("HasCurrentPower");
            var bit766 = packet.ReadBit("HasMaxPower");
            var bit752 = packet.ReadBit("HasLevel");
            var bit326 = packet.ReadBit("HasUnk326");
            var bit770 = packet.ReadBit("HasZoneId");
            var bit756 = packet.ReadBit("HasUnk756");
            var bit776 = packet.ReadBit("HasUnk776");
            var bit786 = packet.ReadBit("HasPosition");
            var bit20 = packet.ReadBit("HasVehicleSeat");
            var bit308 = packet.ReadBit("HasAuras");
            var bit736 = packet.ReadBit("HasPet");
            var bit72 = packet.ReadBit("HasPhase");

            packet.ReadPackedGuid128("MemberGuid");

            if (bit761)
            {
                // sub_5FB6A9
                for (int i = 0; i < 2; i++)
                    packet.ReadByte("Unk761", i);
            }

            if (bit790)
                packet.ReadEnum<GroupMemberStatusFlag>("Status", TypeCode.Int16);

            if (bit763)
                packet.ReadByte("PowerType");

            if (bit322)
                packet.ReadInt16("Unk322");

            if (bit28)
                packet.ReadInt32("CurrentHealth");

            if (bit316)
                packet.ReadInt32("MaxHealth");

            if (bit748)
                packet.ReadInt16("CurrentPower");

            if (bit766)
                packet.ReadInt16("MaxPower");

            if (bit752)
                packet.ReadInt16("Level");

            if (bit326)
                packet.ReadInt16("Unk200000");

            if (bit770)
                packet.ReadInt16("ZoneId");

            if (bit756)
                packet.ReadInt16("Unk2000000");

            if (bit776)
                packet.ReadInt32("Unk4000000");

            if (bit786)
            {
                // sub_626D9A
                packet.ReadInt16("PositionX");
                packet.ReadInt16("PositionY");
                packet.ReadInt16("PositionZ");
            }

            if (bit20)
                packet.ReadInt32("VehicleSeat");

            if (bit308)
            {
                // sub_618493
                var count = packet.ReadInt32("AuraCount");

                for (int i = 0; i < count; i++)
                {
                    packet.ReadInt32("SpellId", i);
                    packet.ReadByte("Scalings", i);
                    packet.ReadInt32("EffectMask", i);
                    var byte3 = packet.ReadInt32("EffectCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.ReadSingle("Scale", i, j);
                }
            }

            if (bit736) // Pet
            {
                // sub_618CF4
                packet.ResetBitReader();

                var bit16 = packet.ReadBit("HasPetGUID");
                var bit153 = packet.ReadBit("HasPetName");
                var bit156 = packet.ReadBit("HasPetModelId");
                var bit164 = packet.ReadBit("HasPetCurrentHealth");
                var bit172 = packet.ReadBit("HasPetMaxHealth");
                var bit404 = packet.ReadBit("HasPetAuras");

                if (bit16)
                    packet.ReadPackedGuid128("PetGUID");

                if (bit153)
                {
                    // sub_5EA889
                    packet.ResetBitReader();
                    var len = packet.ReadBits(8);
                    packet.ReadWoWString("PetName", len);
                }

                if (bit156)
                    packet.ReadInt16("PetModelId");

                if (bit164)
                    packet.ReadInt32("PetCurrentHealth");

                if (bit172)
                    packet.ReadInt32("PetMaxHealth");

                if (bit404)
                {
                    var count = packet.ReadInt32("AuraCount");

                    for (int i = 0; i < count; i++)
                    {
                        packet.ReadInt32("SpellId", i);
                        packet.ReadByte("Scalings", i);
                        packet.ReadInt32("EffectMask", i);
                        var byte3 = packet.ReadInt32("EffectCount", i);

                        for (int j = 0; j < byte3; j++)
                            packet.ReadSingle("Scale", i, j);
                    }
                }
            }

            if (bit72) // Phase
            {
                // sub_61E155
                packet.ReadInt32("PhaseShiftFlags");
                var int4 = packet.ReadInt32("PhaseCount");
                packet.ReadPackedGuid128("PersonalGUID");
                for (int i = 0; i < int4; i++)
                {
                    packet.ReadInt16("PhaseFlags", i);
                    packet.ReadInt16("Id", i);
                }
            }
        }

        [Parser(Opcode.SMSG_ROLE_CHANGED_INFORM)]
        public static void HandleGRoleChangedInform(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("From");
            packet.ReadPackedGuid128("ChangedUnit");
            packet.ReadEnum<LfgRoleFlag>("OldRole", TypeCode.Int32);
            packet.ReadEnum<LfgRoleFlag>("NewRole", TypeCode.Int32);
        }
    }
}