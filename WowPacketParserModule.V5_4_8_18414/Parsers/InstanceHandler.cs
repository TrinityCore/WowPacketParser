using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_SET_RAID_DIFFICULTY)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        public static void HandleSaveCUFProfiles(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var count = packet.ReadBits("count", 19);
                var len = new uint[count];
                for (var i = 0; i < count; i++)
                {
                    packet.ReadBit("unk166", i);
                    packet.ReadBit("unk136", i);
                    packet.ReadBit("unk140", i);
                    packet.ReadBit("unk161", i);
                    packet.ReadBit("unk159", i);
                    packet.ReadBit("unk156", i);
                    packet.ReadBit("unk164", i);
                    packet.ReadBit("unk158", i);
                    packet.ReadBit("unk134", i);
                    packet.ReadBit("unk142", i);
                    packet.ReadBit("unk163", i);
                    packet.ReadBit("unk145", i);
                    len[i] = packet.ReadBits("len", 7, i);
                    packet.ReadBit("unk135", i);
                    packet.ReadBit("unk167", i);
                    packet.ReadBit("unk139", i);
                    packet.ReadBit("unk144", i);
                    packet.ReadBit("unk162", i);
                    packet.ReadBit("unk157", i);
                    packet.ReadBit("unk141", i);
                    packet.ReadBit("unk143", i);
                    packet.ReadBit("unk165", i);
                    packet.ReadBit("unk160", i);
                    packet.ReadBit("unk168", i);
                    packet.ReadBit("unk137", i);
                    packet.ReadBit("unk138", i);
                }
                for (var i = 0; i < count; i++)
                {
                    packet.ReadInt16("unk128", i);
                    packet.ReadByte("unk146", i);
                    packet.ReadByte("unk133", i);
                    packet.ReadInt16("unk130", i);
                    packet.ReadByte("unk148", i);
                    packet.ReadByte("unk132", i);
                    packet.ReadInt16("unk150", i);
                    packet.ReadWoWString("str", len[i], i);
                    packet.ReadByte("unk147", i);
                    packet.ReadInt16("unk152", i);
                    packet.ReadInt16("unk154", i);
                }
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_06E6");
            }
        }

        [Parser(Opcode.SMSG_CORPSE_NOT_IN_INSTANCE)]
        public static void HandleCorpseNotInInstance(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleLoadCUFProfiles(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var len = new uint[count];

            for (var i = 0; i < count; i++)
            {
                packet.ReadBit("unk165", i);
                packet.ReadBit("unk159", i);
                packet.ReadBit("unk157", i);
                packet.ReadBit("unk161", i);
                packet.ReadBit("unk164", i);
                packet.ReadBit("unk141", i);
                packet.ReadBit("unk142", i);
                packet.ReadBit("unk134", i);
                packet.ReadBit("unk140", i);
                len[i] = packet.ReadBits("len", 7, i);
                packet.ReadBit("unk135", i);
                packet.ReadBit("unk138", i);
                packet.ReadBit("unk145", i);
                packet.ReadBit("unk167", i);
                packet.ReadBit("unk156", i);
                packet.ReadBit("unk136", i);
                packet.ReadBit("unk144", i);
                packet.ReadBit("unk143", i);
                packet.ReadBit("unk166", i);
                packet.ReadBit("unk137", i);
                packet.ReadBit("unk139", i);
                packet.ReadBit("unk163", i);
                packet.ReadBit("unk168", i);
                packet.ReadBit("unk160", i);
                packet.ReadBit("unk162", i);
                packet.ReadBit("unk158", i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt16("unk152", i);
                packet.ReadInt16("unk154", i);
                packet.ReadByte("unk133", i);
                packet.ReadWoWString("Profile", len[i], i);
                packet.ReadByte("unk147", i);
                packet.ReadByte("unk146", i);
                packet.ReadInt16("unk128", i);
                packet.ReadByte("unk148", i);
                packet.ReadByte("unk132", i);
                packet.ReadInt16("unk130", i);
                packet.ReadInt16("unk150", i);
            }
        }

        [Parser(Opcode.SMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.ReadInt32("Difficulty");
        }

        [Parser(Opcode.CMSG_RESET_INSTANCES)]
        [Parser(Opcode.SMSG_UPDATE_DUNGEON_ENCOUNTER_FOR_LOOT)]
        public static void HandleInstanceNull(Packet packet)
        {
        }

        [Parser(Opcode.MSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_INSTANCE_RESET)]
        [Parser(Opcode.SMSG_UPDATE_LAST_INSTANCE)]
        public static void HandleServerSetDifficulty(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
